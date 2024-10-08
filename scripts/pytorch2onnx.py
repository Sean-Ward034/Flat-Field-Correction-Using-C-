import torch
import torch.onnx
from basicsr.archs.rrdbnet_arch import RRDBNet

# An instance of your model
model = RRDBNet(num_in_ch=3, num_out_ch=3, num_feat=64, num_block=23, num_grow_ch=32, scale=4)
model.load_state_dict(torch.load('experiments/pretrained_models/RealESRGAN_x4plus.pth')['params_ema'])
# set the train mode to false since we will only run the forward pass.
model.train(False)
model.cpu().eval()

# An example input you would normally provide to your model's forward() method
x = torch.rand(1, 3, 64, 64)
input_names=['data']
output_names=['output']
dynamic_axes= {'data':{0:'batch_size' , 2:'width', 3:'height'}, 'output':{0:'batch_size' , 2:'width', 3:'height'}} #adding names for better debugging
# Export the model
with torch.no_grad():
    torch_out = torch.onnx._export(model, x, 'realesrgan-x4.onnx', input_names=input_names, output_names=output_names, dynamic_axes=dynamic_axes, opset_version=11, export_params=True)