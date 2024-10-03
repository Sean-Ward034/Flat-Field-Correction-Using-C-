import onnx

# Load the ONNX model
model = onnx.load("C:\\Users\\shupa\\FlatFieldCorrection\\FlatFieldCorrection\\Models\\Real-ESRGAN_GUI\\Real-ESRGAN_GUI\\models\\realesrgan-x4plus.onnx")

# Print model's input layers
for input in model.graph.input:
    print(f"Input Name: {input.name}")
