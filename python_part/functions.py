# -*- coding: utf-8 -*-

import os
from howlongtobeatpy import HowLongToBeat
from sqlalchemy import false, true
from Game import Game
from Film import Film
from TVSeries import TVSeries
import json
import requests
from bs4 import BeautifulSoup
import time

missed_games = list()

path_to_data = "F:\my_programs\c#\desk\TitleCounter\\bin\Debug\data\\"

headers = {
    'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}

"""
path_to_data = '/'.join(os.path.abspath(os.curdir).split('/')
                        [:-2])
"""

def get_missed_games():
    return missed_games

def get_game_year(url):
    y = -1
    try:
        r = requests.get(url, headers=headers)
        with open('hltb.html', 'wb') as f:
            f.write(r.text.encode('utf-8'))
        f = open('hltb.html')
        soup = BeautifulSoup(f, features="html.parser")
        # get text from html
        text = soup.get_text()
        a = text.find("NA:")
        t = text[a+4:a+22]
        t = t.split(' ')
        #m = t[0]  month
        #d = t[1][:-1]  day
        y = int(t[2][:4])  # year
    except:
        y = -1
    return y

# Print status of current operation
def print_status(status, process):
    string = '='*30+'\n'
    if status == 's':
        string += 'STARTING: '
    elif status == 'f' or status == 'l':
        string += 'COMPLETED: '
    string += process.upper() + '\n'
    if status == 'l':
        string += "MISSED GAMES: "
        string += missed_games.__str__() + '\n'
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

    
# Choose the most similar game by name
def choose_game(name):
    results = find_games(name)

    if results is not None and len(results) > 0:
        r = results[-1]
        y = get_game_year(r.game_web_link)
        res = Game(r.game_name, r.game_image_url,
               r.game_web_link, r.gameplay_main, 
               r.similarity, year=y)
        #print(name)
        return res
    else: 
        missed_games.append(name)
        #print("ERROR", name)
        return Game()


# Filter games list to remove None objects
def remove_none_games(games):
    result = list()
    for g in games:
        if not (g.name == "None"):
            result.append(g)
    return result

# Get list of all games from textfile from HLTB
def load_games_info():
    print_status('s', 'load_info')
    base = open(games_file_name, 'r')
    lines = base.readlines()
    base.close
    games = list()
    for g in lines:
        title = g[:-1]
        gg = choose_game(title)
        games.append(gg)
    print_status('l', 'load_info')
    save_missed_games()
    return games

def save_missed_games():
    file_name = 'MissedGames.txt'
    file = open(file_name, 'w')
    for g in missed_games:
        file.write(g+'\n')

# Create json file of titles
def create_json(titles, tp, f=True):
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
"""
# Add titles to json file
def append_json(title, tp):
    print_status('s', 'append_json')
    filename = 'images/' + tp + '/' + tp + '_sheet.json'
    file = open(filename, 'a', encoding='utf-8')
    data = list()
    data.append(title.to_dict())
    
    json.dump(data, file, indent=4)
    file.close
    print_status('f', 'append_json')
"""
# Get titles from json file
def read_json(tp, f = True):
    if f:
        print_status('s', 'read_json')
    filename = path_to_data + tp + '_sheet.json'
    file = open(filename, 'r', encoding='utf-8')
    data = json.load(file)
    result = list()
    for d in data:
        if f:
            print(d)
        if tp == 'games':
            result.append(Game(name=d['Name'], image_url=d['Image_Url'], link=d['Link'], 
                time=d['Time'], similarity=d['Similarity'], 
                score=d['Score'], year=d['Year'], status=d['Status']))
        elif tp == 'films':
            result.append(Film(name=d['Name'], rus_name=d['Rus_Name'], 
                                image_url=d['Image_Url'], link=d['Link'],
                                time=d['Time'], score=d['Score'],
                                year=d['Year'], status=d['Status'],
                                genres=d["Genres"]))
        elif tp =='tvseries':
            result.append(TVSeries(name=d['Name'], rus_name=d['Rus_Name'],
                                   image_url=d['Image_Url'], link=d['Link'],
                                   time=d['Time'], score=d['Score'],
                                   year=d['Year'], status=d['Status'],
                                   genres=d["Genres"], seasons=d['Seasons']))
    if f:
        print_status('f', 'read_json')
    return result

# Return list of prohibited symbols in the name of the title
def has_proh_symb(string):
    result = ''
    if ':' in string:
        result += ':'
    if '/' in string:
        result += '/'
    if chr(92) in string:  #  '\'
        result += chr(92)
    if '"' in string:
        result += '"'
    if '*' in string:
        result += '*'
    if '?' in string:
        result += '?'
    if '|' in string:
        result += '|'
    if '>' in string:
        result += '>'
    if '<' in string:
        result += '<'
    return result   
    
# Remove prohibited symbols from the String
def replace_symbols(string, symbols):
    for s in symbols:
        result = ''
        parts = string.split(s)
        for p in parts:
            result += p
    return result

# download images for list of titles
def download_images(titles, tp, f=True):
    if f:
        print_status('s', 'download_images')
    
    for t in titles:
        
        download_image(t, tp)
        
    if f:
        print_status('f', 'download_images')

# Download image for 1 title
def download_image(title, tp):
    try:
        image = requests.get(title.image_url, headers=headers)

        temp_name = title.name
        symb = has_proh_symb(temp_name)
        if symb != '':
            temp_name = replace_symbols(temp_name, symb)
            #print(temp_name + '  |||  CHANGED FROM "'+game.name+'"')
        else:
            pass
            #print(temp_name)
        path = path_to_data + 'images\\' + tp + '/' + temp_name + '.jpg'
        title.has_image = true
        title.image_name = temp_name + '.jpg'

        with open(path, 'wb') as f:
            f.write(image.content)
    except:
        title.has_image = false
        title.image_name = "noimage.png"


def print_json(tp):
    titles = read_json(tp, False)
    for t in titles:
        t.print()




