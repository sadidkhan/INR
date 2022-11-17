import numpy as np
import json
import os
import scipy.io
import requests
rootdir = '/home/inr-lab/arat_segmentation/videoFiles'
folder_dir=[]
fileNames1=[]
for file in os.listdir(rootdir):
    fileNames1.append(file)
        
print(json.dumps(fileNames1))
headers = {"charset": "utf-8", "Content-Type": "application/json"}

ht_url= "http://localhost:5016/api/Home/SubmitFileNames"
response=requests.post(ht_url, data=json.dumps(fileNames1), headers=headers, verify=False)
print(response)
