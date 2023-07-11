# -*- coding: utf-8 -*-

from howlongtobeatpy import HowLongToBeat
from Game import Game
from Film import Film
from TVSeries import TVSeries
import os
import json
import requests
from bs4 import BeautifulSoup
import re

missed_games = list()

path_to_data = "F:\\my_programs\\c#\desk\\TitleCounter\\bin\\Debug\\net6.0-windows\\data\\"

headers = {
    'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}

"""
path_to_data = '/'.join(os.path.abspath(os.curdir).split('/')
                        [:-2])
"""

def get_missed_games():
    return missed_games

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
        res = Game(title=r.game_name, 
                   image_url=r.game_image_url,
                   link_url=r.game_web_link, 
                   time=r.main_story*60,
                   similarity=r.similarity, 
                   date_release=str(r.release_world)+"-01-01")
        res.date_release = find_date_release(res.link_url)
        return res
    else: 
        missed_games.append(name)
        return Game()
    

def month_to_int(string_month):
    return {
        'January': 1,
        'February': 2,
        'March': 3,
        'April': 4,
        'May': 5,
        'June': 6,
        'July': 7,
        'August': 8,
        'September': 9,
        'October': 10,
        'November': 11,
        'December': 12
    }[string_month]

def string_into_date(string):
    r = re.match(r"([A-Za-z]+) (\d{2}), (\d{4})", string)
    month = r.group(1)
    day = r.group(2)
    year = r.group(3)
    return year+'-'+str(month_to_int(month))+'-'+day


def find_date_release(web_link):
    date_release = "1900-01-01"
    try:
        r = requests.get(web_link, headers=headers)
        with open('hltb.html', 'wb') as f:
            f.write(r.text.encode('utf-8'))
        f = open('hltb.html')
        soup = BeautifulSoup(f, features="html.parser")
        # get text from html
        text = soup.get_text()

        na = text.find("NA:")
        eu = text.find("EU:")
        jp = text.find("JP:")
        up = text.find("Updated:")

        if jp == -1:
            jp = up
        if eu == -1:
            eu = up
        na_date = eu_date = jp_date = date_release

        if na != -1:
            na_date = text[na+4:eu]
        if eu != up:
            eu_date = text[eu+4:jp]
        if jp != up:
            jp_date = text[jp+4:up]

        dates = [na_date, eu_date, jp_date]
        dates.sort()
        date_release = dates[0]
        os.remove('hltb.html')
    except:
        pass
    return string_into_date(date_release)

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


restricted_symbols = {':', '/', chr(92), '"', '*', '?', '|', '>', '<'}
    
# Remove prohibited symbols from the String
def set_fixed_name(content):
    fixed_title = ""
    for s in content.title:
        if not s in restricted_symbols:
            fixed_title += s
    content.fixed_title = fixed_title

# download images for list of titles
def download_images(titles, tp, f=True):
    if f:
        print_status('s', 'download_images')
    
    for t in titles:
        
        download_image(t, tp)
        
    if f:
        print_status('f', 'download_images')

# Download image for 1 title
def download_image(content, tp):
    try:
        image = requests.get(content.image_url, headers=headers)

        path = path_to_data + 'images\\' + tp + '/' + content.fixed_title + '.jpg'

        with open(path, 'wb') as f:
            f.write(image.content)
        return image.content
    except:
        print("ERROR downloading image")
        return None


def print_json(tp):
    titles = read_json(tp, False)
    for t in titles:
        t.print()




