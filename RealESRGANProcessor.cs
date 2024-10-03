using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management;
using Microsoft.ML.OnnxRuntime;  // Corrected namespace for InferenceSession
using Microsoft.ML.OnnxRuntime.Tensors;  // Corrected namespace for Tensor

public static class RealESRGANProcessor
{
    public static Bitmap UpscaleImage(Bitmap inputImage, string modelPath)
    {
        // Step 1: Initialize the ONNX Inference session with dynamic execution provider
        var sessionOptions = new SessionOptions();

        // Check if a GPU is available, and set execution provider accordingly
        if (IsGpuAvailable())
        {
            try
            {
                sessionOptions.AppendExecutionProvider_DML();  // Use DirectML if GPU is present
                Console.WriteLine("GPU detected and using DirectML provider.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize GPU provider: {ex.Message}. Falling back to CPU.");
                sessionOptions.AppendExecutionProvider("CPU");
            }
        }
        else
        {
            Console.WriteLine("No GPU detected. Using CPU provider.");
            sessionOptions.AppendExecutionProvider("CPU");
        }

        // Step 2: Create the InferenceSession with the appropriate session options
        using (var session = new InferenceSession(modelPath, sessionOptions))
        {
            // Step 3: Convert Bitmap to Tensor
            var inputTensor = ConvertImageToTensor(inputImage);

            // Step 4: Run the model on the input image
            var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("data", inputTensor)
        };

            using (var results = session.Run(inputs))
            {
                // Step 5: Extract the result tensor and convert back to Bitmap
                var outputTensor = results.First().AsTensor<float>();
                return ConvertTensorToBitmap(outputTensor, inputImage.Width, inputImage.Height);
            }
        }
    }

    // Function to check if a GPU is available using System.Management (Windows-specific)
    private static bool IsGpuAvailable()
    {
        try
        {
            // Create a query to look for GPU devices
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController"))
            {
                var gpuDevices = searcher.Get();

                // If there are any results, a GPU is present
                if (gpuDevices.Cast<ManagementObject>().Any())
                {
                    return true;  // GPU is available
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking for GPU availability: {ex.Message}");
        }

        return false;  // No GPU found or an error occurred
    }

    private static Tensor<float> ConvertImageToTensor(Bitmap image)
    {
        var tensorData = new float[1, 3, image.Height, image.Width]; // Multi-dimensional array for RGB image

        // Populate the tensorData array with pixel values
        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                var pixel = image.GetPixel(x, y);
                tensorData[0, 0, y, x] = pixel.R / 255.0f;
                tensorData[0, 1, y, x] = pixel.G / 255.0f;
                tensorData[0, 2, y, x] = pixel.B / 255.0f;
            }
        }

        // Use ArrayTensorExtensions.ToTensor to create a DenseTensor from the 4D array
        return tensorData.ToTensor();
    }

    private static Bitmap ConvertTensorToBitmap(Tensor<float> tensor, int width, int height)
    {
        var outputImage = new Bitmap(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var r = (int)(tensor[0, 0, y, x] * 255);
                var g = (int)(tensor[0, 1, y, x] * 255);
                var b = (int)(tensor[0, 2, y, x] * 255);
                outputImage.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }
        return outputImage;
    }
}
