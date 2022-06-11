import json
import requests
from bs4 import BeautifulSoup
from functions import *

headers = {
    'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}

def test(url):
    r = requests.get(url, headers=headers)
    with open('hltb33.html', 'wb') as f:
        f.write(r.text.encode("utf-8"))
    f = open('hltb33.html')
    soup = BeautifulSoup(f, features="html.parser")
    # get text from html
    text = soup.get_text().split(' ')
    #a = text.find("NA:")
    #t = text[a+4:a+22]
    return text

def test1(name):
    results = HowLongToBeat().search(game_name=name, similarity_case_sensitive=False)
    print(results)

def test2(url):
    r = requests.get(url, headers=headers)
    with open('hltb.html', 'wb') as f:
        f.write(r.text.encode('utf-8'))
    f = open('hltb.html')
    soup = BeautifulSoup(f, features="html.parser")
    # get text from html
    text = soup.get_text()
    a = text.find("NA:")
    t = text[a+4:a+22]
    print(t)
    t = t.split(' ')
    #m = t[0]  month
    #d = t[1][:-1]  day
    y = int(t[2][:4])  # year
    return y

g = test2('https://howlongtobeat.com/game?id=57454')
print(g)
