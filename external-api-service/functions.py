# -*- coding: utf-8 -*-
import requests


headers = {
    'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:128.0) Gecko/20100101 Firefox/128.0'}

"""
path_to_data = '/'.join(os.path.abspath(os.curdir).split('/')
                        [:-2])
"""

restricted_symbols = {':', '/', chr(92), '"', '*', '?', '|', '>', '<'}


# Remove prohibited symbols from the String
def get_fixed_name(string):
    fixed_title = ""
    for s in string:
        if s not in restricted_symbols:
            fixed_title += s
    return fixed_title


# Download image for 1 title
def download_image(image_url):
    try:
        image = requests.get(image_url, headers=headers)
        return image.content
    except:
        print("ERROR downloading image")
        return None




