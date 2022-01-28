import json
import requests
from bs4 import BeautifulSoup

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

g = test('https://howlongtobeat.com/game?id=57454')
print(g)
