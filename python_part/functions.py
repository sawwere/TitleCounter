# -*- coding: utf-8 -*-

from howlongtobeatpy import HowLongToBeat
from models.Game import Game
from models.Film import Film
from models.TVSeries import TVSeries
import json
import requests


headers = {
    'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}

"""
path_to_data = '/'.join(os.path.abspath(os.curdir).split('/')
                        [:-2])
"""

# Print status of current operation
def print_status(status, process):
    string = '='*30+'\n'
    if status == 's':
        string += 'STARTING: '
    elif status == 'f' or status == 'l':
        string += 'COMPLETED: '
    string += process.upper() + '\n'
    if status == 'l':
        string += "MISSED GAMES: " + '\n'
    string += '='*30
    print(string)


# Sort founded games by similarity
def sortBySim(game):
    return game.similarity

# Get list of the potential games fron HLTB
def find_games(name):
    results = HowLongToBeat().search(game_name=name, similarity_case_sensitive=False)
    results.sort(key=sortBySim)
    return results
    
async def choose_game(name):
    results = find_games(name)
    if results is not None and len(results) > 0:
        r = results[-1]
        res = Game(Title=r.game_name, 
                   ImageUrl=r.game_image_url,
                   LinkUrl=r.game_web_link, 
                   Time=r.main_story*60,
                   Similarity=r.similarity, 
                   DateRelease=str(r.release_world)+"-01-01")
        #res.date_release = find_date_release(res.link_url)
        return res
    else: 
        return None

restricted_symbols = {':', '/', chr(92), '"', '*', '?', '|', '>', '<'}
    
# Remove prohibited symbols from the String
def get_fixed_name(string):
    fixed_title = ""
    for s in string:
        if not s in restricted_symbols:
            fixed_title += s
    return fixed_title


# Download image for 1 title
async def download_image(image_url):
    try:
        image = requests.get(image_url, headers=headers)
        return image.content
    except:
        print("ERROR downloading image")
        return None
    


# Create json file of titles
def create_json(path_to_data, titles, tp, f=True):
    if f:
        print_status('s', 'create_json')    
    filename = path_to_data + tp + '_sheet.json'
    file = open(filename, 'w', encoding='utf-8')
    data = list()
    for t in titles:
        #g.print()
        data.append(t.to_dict())

    json.dump(data, file, indent=4)
    file.close
    if f:
        print_status('f', 'create_json')




