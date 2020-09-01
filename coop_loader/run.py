# -*- coding: utf-8 -*-
import os
import sys

sys.path.append(os.path.join(os.getcwd(), "NormalizedDB"))
sys.path.append("Lib\site-packages")
sys.path.append(os.getcwd())
import requests
import pypyodbc
import subprocess

if __name__ == "__main__":
    first_cmd = ['python', 'COOP_Location_ID_Cache.py']
    second_cmd = ['python', 'loader.py']
    third_cmd = ['python', 'Loader_Normalized_Json.py']
    subprocess.Popen(first_cmd).wait()
    subprocess.Popen(second_cmd).wait()
    subprocess.Popen(third_cmd).wait()