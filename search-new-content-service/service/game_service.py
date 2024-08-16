import json
import re
import requests

from bs4 import BeautifulSoup
from howlongtobeatpy import HowLongToBeat

from models.Game import Game

LIMIT = 5

class GameService:
    def __init__(self) -> None:
        with open("token.json", 'r') as f:
            api_keys = json.loads(f.read())
        self.headers = {
    'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}

    
    def choose_game(self, name):
        searh_results = HowLongToBeat().search(game_name=name, similarity_case_sensitive=False)
        if searh_results is not None and len(searh_results) > 0:
            searh_results.sort(key= lambda x: x.similarity, reverse=True)
            r = searh_results[0]
            res = Game(title=r.game_name, 
                    image_url=r.game_image_url,
                    link_url=r.game_web_link, 
                    time=r.main_story*60,
                    Similarity=r.similarity, 
                    date_release=str(r.release_world)+"-01-01",
                    Score=r.review_score)
            #res.date_release = find_date_release(res.link_url)
            return res
        else: 
            return None

    def search(self, title: str) -> list:
        searh_results = HowLongToBeat().search(game_name=title, similarity_case_sensitive=False)
        if searh_results is None:
            return None
        searh_results.sort(key= lambda x: x.similarity, reverse=True)    
        
        count = min(LIMIT, len(searh_results))
        res = []
        for i in range(0, count):
            r = searh_results[i]

            game = Game(title=r.game_name, 
                    image_url=r.game_image_url,
                    link_url=r.game_web_link, 
                    time=r.main_story*60,
                    similarity=r.similarity, 
                    date_release=self.get_hltb_release_date(r.game_web_link),
                    score=r.review_score)
            res.append(game)
        return res
    
    def get_hltb_release_date(self, web_link):
        DateRelease = "1900-01-01"
        try:
            r = requests.get(web_link, headers=self.headers)
            
            soup = BeautifulSoup(r.text.encode('utf-8'), features="html.parser")
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
            dates = []
            if na != -1:
                dates.append(text[na+4:eu])
            if eu != up:
                dates.append(text[eu+4:jp])
            if jp != up:
                dates.append(text[jp+4:up])

            #dates = [na_date, eu_date, jp_date]
            dates.sort()
            DateRelease = dates[0]
            return self.string_into_date(DateRelease)
        except:
            return DateRelease


    def month_to_num(self, string_month):
        return {
            'January': '01',
            'February': '02',
            'March': '03',
            'April': '04',
            'May': '05',
            'June': '06',
            'July': '07',
            'August': '08',
            'September': '09',
            'October': '10',
            'November': '11',
            'December': '12'
        }[string_month]
    
    def string_into_date(self, string):
        r = re.match(r"([A-Za-z]+) (\d{2}), (\d{4})", string)
        if r is None:
            return "1900-01-01"
        month = r.group(1)
        day = r.group(2)
        year = r.group(3)
        return year+'-'+self.month_to_num(month)+'-'+day