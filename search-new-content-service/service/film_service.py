import json
import re
import requests
from models.Film import Film

LIMIT = 5

class FilmService:
    def __init__(self) -> None:
        with open("token.json", 'r') as f:
            api_keys = json.loads(f.read())
        self.kinopoisk_token = api_keys["kinopoisk"]

    def search(self, title, page) -> list:
        url = "https://api.kinopoisk.dev/v1.4/movie/search?page="+str(page)+"&limit="+str(LIMIT)+"&query=" + title

        headers = {
            "accept": "application/json",
            "X-API-KEY": self.kinopoisk_token
        }

        response = requests.get(url, headers=headers)
        if (response.status_code != 200):
            return None
        response = response.json()
        print(response["total"])
        contents = response["docs"]      
        
        count = min(LIMIT, len(contents))
        res = []
        for i in range(0, count):
            url = "https://api.kinopoisk.dev/v1.4/movie/" + str(contents[i]["id"])
            response = requests.get(url, headers=headers)
            if (response.status_code != 200):
                break
            m = response.json()
            if (m["isSeries"] == True):
                continue
            rus_title = m["name"]
            orig_title = m["alternativeName"]
            global_score = m["rating"]["kp"]
            time = m["movieLength"]
            link_url = "https://www.kinopoisk.ru/film/"+str(m["id"])
            image_url = m["poster"]["url"]


            film = Film(movie = m, 
                    title = orig_title,
                    rus_title = rus_title,
                    time = time,
                    global_score= global_score,
                    link_url = link_url,
                    image_url = image_url,
                    date_release = self.get_kinopoisk_release_date(m)
                    )
            res.append(film)
            
        return res
    def get_kinopoisk_release_date(self, film):
        codes = ["world", "russia", "digital", "dvd", "bluray", "country"]
        for code in codes:
            if code in film["premiere"].keys() and film["premiere"][code] != None:
                return film["premiere"][code][0:10]
        return "1900-01-01"


    def month_to_num(self, string_month):
        return {
            'Jan': '01',
            'Feb': '02',
            'Mar': '03',
            'Apr': '04',
            'May': '05',
            'Jun': '06',
            'Jul': '07',
            'Aug': '08',
            'Sep': '09',
            'Oct': '10',
            'Nov': '11',
            'Dec': '12'
        }[string_month]
    
    def string_into_date(self, string):
        r = re.match(r"(\d{2}) ([A-Za-z]+) (\d{4})", string)
        if r is None:
            return "1900-01-01"
        month = r.group(2)
        day = r.group(1)
        year = r.group(3)
        return year+'-'+self.month_to_num(month)+'-'+day