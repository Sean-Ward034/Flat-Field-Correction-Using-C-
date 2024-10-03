﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Real_ESRGAN_GUI
{
    public class Model : IDisposable
    {
        private string modelName = "";
        private InferenceSession session;
        private Logger logger = Logger.Instance;

        public async Task<bool> LoadModel(string modelPath, string modelName, int deviceId, CancellationToken token)
        {
            if (session == null || this.modelName != modelName)
            {
                this.modelName = modelName;
                try
                {
                    var sessionOptions = new SessionOptions();
                    if (deviceId == -1)
                    {
                        logger.Log("Creating inference session on CPU.");
                        sessionOptions.AppendExecutionProvider_CPU();
                    }
                    else
                    {
                        logger.Log($"Creating inference session on GPU{deviceId}.");
                        sessionOptions.AppendExecutionProvider_DML(deviceId);
                        sessionOptions.EnableMemoryPattern = false;
                    }
                    session = await Task.Run(() => { return new InferenceSession(Path.Combine(modelPath, $"{modelName}.onnx"), sessionOptions); }).WaitOrCancel(token);
                }
                catch (Exception ex)
                {
                    logger.Log($"{ex}: Unable to initial inference session on GPU{deviceId}! Falling back to CPU.");
                    session = await Task.Run(() => { return new InferenceSession(Path.Combine(modelPath, $"{modelName}.onnx")); }).WaitOrCancel(token);
                }
            }

            if (session != null)
            {
                return true;
            }
            return false;
        }

        public async Task Scale(string baseInputPath, List<string> inputPaths, string outputPath, string outputFormat, bool preserveAlpha)
        {
            int count = inputPaths.Count();
            foreach(var inputPath in inputPaths)
            {
                Bitmap image = new Bitmap(inputPath);
                var originalPixelFormat = image.PixelFormat;

                Bitmap alpha=null;
                if (preserveAlpha && originalPixelFormat!=PixelFormat.Format24bppRgb)
                {
                    if (image.PixelFormat != PixelFormat.Format32bppArgb)
                    {
                        image = ImageProcess.ConvertBitmapToFormat(image, PixelFormat.Format32bppArgb);
                    }
                    ImageProcess.SplitChannel(image, out image, out alpha);
                }

                // Ensure that we got RGB channels in Format24bppRgb bitmap.
                if (image.PixelFormat != PixelFormat.Format24bppRgb)
                {
                    image = ImageProcess.ConvertBitmapToFormat(image, PixelFormat.Format24bppRgb);
                }

                logger.Log("Creating input image...");
                var inMat = ConvertImageToFloatTensorUnsafe(image);
                logger.Progress += 10/count;

                logger.Log("Inferencing...");
                var outMat = await Inference(inMat);
                logger.Progress += 10/count;

                if (outMat == null)
                {
                    logger.Log("A null image is returned! Aborting...");
                    logger.Progress += 10/count;
                    image.Dispose();
                    return;
                }

                logger.Log("Converting output tensor to image...");
                image = ConvertFloatTensorToImageUnsafe(outMat);

                if (preserveAlpha && originalPixelFormat != PixelFormat.Format24bppRgb && alpha!=null)
                {
                    alpha = ImageProcess.ResizeAlphaChannel(alpha, image.Width, image.Height);    // Using BICUBIC to resize alpha channel.
                    image = ImageProcess.CombineChannel(image, alpha);
                }

                var saveName = $"\\{Path.GetFileName(inputPath).Split(".")[0]}_{modelName}.{outputFormat}";
                var saveStructure = Path.GetRelativePath(baseInputPath, Path.GetDirectoryName(inputPath));
                var savePath = Path.GetFullPath(saveStructure,outputPath)+"\\";
                
                logger.Log($"Writing image to {savePath}...");
                Directory.CreateDirectory(savePath);
                image.Save(savePath+saveName);
                logger.Progress += 10/count;
                image.Dispose();
            }
        }

        public async Task<Tensor<float>> Inference(Tensor<float> input)
        {
            try
            {
                var inputName = session.InputMetadata.First().Key;
                var inputTensor = new List<NamedOnnxValue>() { NamedOnnxValue.CreateFromTensor<float>(inputName, input) };
                var output = await Task.Run(() => { return session.Run(inputTensor).First().Value; });
                return (Tensor<float>)output;
            }
            catch (Exception ex)
            {
                logger.Log("An exception has occurred during inference session!");
                logger.Log(ex.ToString());
                logger.Log(ex.Message);
            }
            return null;
        }

        public static Tensor<float> ConvertImageToFloatTensorUnsafe(Bitmap image)
        {
            // Create the Tensor with the appropiate dimensions for the NN
            Tensor<float> data = new DenseTensor<float>(new[] { 1, 3, image.Width, image.Height });

            BitmapData bmd = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            int PixelSize = 3;

            unsafe
            {
                for (int y = 0; y < bmd.Height; y++)
                {
                    // row is a pointer to a full row of data with each of its colors
                    byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                    for (int x = 0; x < bmd.Width; x++)
                    {
                        // note the order of colors is BGR, convert to RGB
                        data[0, 0, x, y] = row[(x * PixelSize) + 2] / (float)255.0;
                        data[0, 1, x, y] = row[(x * PixelSize) + 1] / (float)255.0;
                        data[0, 2, x, y] = row[(x * PixelSize) + 0] / (float)255.0;
                    }
                }

                image.UnlockBits(bmd);
            }
            return data;
        }

        public static Bitmap ConvertFloatTensorToImageUnsafe(Tensor<float> tensor)
        {
            Bitmap bmp = new Bitmap(tensor.Dimensions[2], tensor.Dimensions[3], PixelFormat.Format24bppRgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
            int PixelSize = 3;
            unsafe
            {
                for (int y = 0; y < bmd.Height; y++)
                {
                    // row is a pointer to a full row of data with each of its colors
                    byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                    for (int x = 0; x < bmd.Width; x++)
                    {
                        // note the order of colors is RGB, convert to BGR
                        // remember clamp to [0, 1]
                        row[x * PixelSize + 2] = (byte)(Math.Clamp(tensor[0, 0, x, y], 0, 1) * 255.0);
                        row[x * PixelSize + 1] = (byte)(Math.Clamp(tensor[0, 1, x, y], 0, 1) * 255.0);
                        row[x * PixelSize + 0] = (byte)(Math.Clamp(tensor[0, 2, x, y], 0, 1) * 255.0);
                    }
                }

                bmp.UnlockBits(bmd);
            }
            return bmp;
        }

        public void Dispose()
        {
            if (session!=null)
            {
                session.Dispose();
            }
        }
    }
}
