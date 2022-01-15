# -*- coding: utf-8 -*-

from howlongtobeatpy import HowLongToBeat
from Game import Game
from Film import Film
from TVSeries import TVSeries
import json
import requests
from bs4 import BeautifulSoup
import time

missed_games = list()
games_file_name = "F:/my_programs/python/HowLongToBeat/games_sheet1.txt"
films_file_name = "F:/my_programs/python/HowLongToBeat/films_sheet1.txt"
json_games_games_file_name = "F:/my_programs/python/HowLongToBeat/games_sheet2.json"
json_films_games_file_name = "F:/my_programs/python/HowLongToBeat/films_sheet2.json"

def get_missed_games():
    return missed_games

def get_game_year(url):
    r = requests.get(url)
    with open('hltb.html', 'wb') as f:
        f.write(r.text.encode('cp1251'))
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
    filename = 'F:\my_programs\python\HowLongToBeat/' + tp + '_sheet.json'
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
    filename = 'F:/my_programs/python/HowLongToBeat/' + tp + '_sheet.json'
    file = open(filename, 'r', encoding='utf-8')
    data = json.load(file)
    result = list()
    for d in data:
        if f:
            print(d['name'])
        if tp == 'game':
            result.append(Game(name=d['name'], image_url=d['image_url'], link=d['link'], 
                time=d['time'], similarity=d['similarity'], 
                score=d['score'], year=d['year'], status=d['status']))
        elif tp == 'film':
            result.append(Film(name=d['name'], rus_name=d['rus_name'], 
                                image_url=d['image_url'], link=d['link'],
                                time=d['time'], score=d['score'],
                                year=d['year'], status=d['status'],
                                genres=d["genres"]))
        elif tp =="tvseries":
            result.append(TVSeries(name=d['name'], rus_name=d['rus_name'],
                                   image_url=d['image_url'], link=d['link'],
                                   time=d['time'], score=d['score'],
                                   year=d['year'], status=d['status'],
                                   genres=d["genres"], seasons=d['seasons']))
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
    image = requests.get(title.image_url)

    temp_name = title.name
    symb = has_proh_symb(temp_name)
    if symb != '':
        temp_name = replace_symbols(temp_name, symb)
        #print(temp_name + '  |||  CHANGED FROM "'+game.name+'"')
    else:
        pass
        #print(temp_name)
    path = 'F:\my_programs\python\HowLongToBeat\images/' + tp + '/' + temp_name + '.jpg'
    with open(path, 'wb') as f:
        f.write(image.content)

def print_json(tp):
    titles = read_json(tp, False)
    for t in titles:
        t.print()




