import numpy as np
import json
import os
import scipy.io
import requests
rootdir = 'E:/project/INR-Tamim/Videos/'
folder_dir=[]
fileNames1=[]
for file in os.listdir(rootdir):
    # d = os.path.join(rootdir, file)
    # if os.path.isdir(d):
        #folder_dir.append(d)
    fileNames1.append(file)
        
        
print(json.dumps(fileNames1))
headers = {"charset": "utf-8", "Content-Type": "application/json"}

ht_url= "https://localhost:44305/api/Home/SubmitFileNames"
response=requests.post(ht_url, data=json.dumps(fileNames1), headers=headers, verify=False)
print(response)