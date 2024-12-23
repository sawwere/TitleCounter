import re
import requests
from models.Film import Film

LIMIT = 5


class FilmService:
    def __init__(self, api_keys) -> None:
        self.api_keys = api_keys
        self.kinopoisk_token = api_keys["KP_API_KEY"]

    def search(self, title, page) -> list:
        url = "https://api.kinopoisk.dev/v1.4/movie/search?page=" + str(page) + "&limit=" + str(LIMIT) + "&query=" + title

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
        arr = []
        for i in range(0, count):
            url = "https://api.kinopoisk.dev/v1.4/movie/" + str(contents[i]["id"])
            response = requests.get(url, headers=headers)
            if (response.status_code != 200):
                break
            m = response.json()
            if (m["isSeries"] is True):
                continue
            alternative_title = m["name"]
            orig_title = m["alternativeName"]
            if orig_title is None:
                orig_title = alternative_title
            global_score = m["rating"]["kp"]
            time = m["movieLength"]
            kp_id = str(m["id"])
            imdb_id = str(m["externalId"]["imdb"])
            image_url = m["poster"]["url"]
            print("===================================")
            print('\033[92m' + kp_id + " " + orig_title+'\033[0m')
            print("===================================")
            film = Film(movie=m,
                        title=orig_title,
                        ru_title=alternative_title,
                        time=time,
                        global_score=global_score,
                        kp_id=kp_id,
                        imdb_id=imdb_id,
                        image_url=image_url,
                        date_release=self.get_kinopoisk_release_date(m)
                        )
            arr.append(film)
        res = {}
        res["total"] = len(arr)
        res["contents"] = [x.to_dict() for x in arr]    
        return res
    
    def search_by_page(self, page) -> list:
        url = "https://api.kinopoisk.dev/v1.4/movie?page=" + str(page) + "&limit=40&selectFields=externalId&selectFields=id&selectFields=name&selectFields=alternativeName&selectFields=enName&selectFields=premiere&selectFields=year&selectFields=poster&selectFields=isSeries&selectFields=rating&selectFields=movieLength&selectFields=description&selectFields=names&notNullFields=poster.url"

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
        
        count = len(contents)
        arr = []
        for i in range(0, count):
            m = contents[i]
            if (m["isSeries"] is True):
                continue
            ru_title = m["name"]
            orig_title = m["alternativeName"]
            if orig_title is None:
                orig_title = ru_title
            en_title = None
            # for name in m["names"]:
            #     if ("language" in name.keys()
            #             and "type" in name.keys()
            #             and name["language"] == "US"
            #             and name["type"] == "Extended Edition"):
            #         en_title = name["name"]
            #         print("FOUND EN TITLE " + en_title)
            #         break

            global_score = m["rating"]["kp"]
            time = m["movieLength"]
            kp_id = str(m["id"])

            imdb_id = None
            tmdb_id = None
            if "externalId" in m.keys():
                if "imdb" in m["externalId"].keys():
                    imdb_id = str(m["externalId"]["imdb"])
                    if imdb_id == "" or imdb_id == "None":
                        imdb_id = None
                else:
                    imdb_id = None

                if "tmdb" in m["externalId"].keys():
                    tmdb_id = str(m["externalId"]["tmdb"])
                    if tmdb_id == "None":
                        tmdb_id = None
                else:
                    tmdb_id = None

            description = m["description"]
            year = m['year']

            image_url = m["poster"]["url"]
            print("===================================")
            print('\033[92m' + kp_id + ' ' + orig_title + '\033[0m')
            print(en_title)
            print("===================================")

            film = Film(movie=m,
                        title=orig_title,
                        ru_title=ru_title,
                        en_title=en_title,
                        time=time,
                        description=description,
                        global_score=global_score,
                        kp_id=kp_id,
                        imdb_id=imdb_id,
                        tmdb_id=tmdb_id,
                        image_url=image_url,
                        date_release=self.get_kinopoisk_release_date(m),
                        year_release=year
                        )
            arr.append(film)
        res = {}
        res["total"] = len(arr)
        res["contents"] = [x.to_dict() for x in arr]
        return res

    def search_by_id(self, id) -> list:
        headers = {
            "accept": "application/json",
            "X-API-KEY": self.kinopoisk_token
        }

        url = "https://api.kinopoisk.dev/v1.4/movie/" + str(id)
        response = requests.get(url, headers=headers)
        if (response.status_code != 200):
            return None
        m = response.json()
        if (m["isSeries"] is True):
            return None
        alternative_title = m["name"]
        orig_title = m["alternativeName"]
        global_score = m["rating"]["kp"]
        time = m["movieLength"]
        kp_id = str(m["id"])
        imdb_id = str(m["externalId"]["imdb"])
        image_url = m["poster"]["url"]

        film = Film(movie=m,
                    title=orig_title,
                    ru_title=alternative_title,
                    time=time,
                    global_score=global_score,
                    kp_id=kp_id,
                    imdb_id=imdb_id,
                    image_url=image_url,
                    date_release=self.get_kinopoisk_release_date(m)
                    )
        res = {}
        res["total"] = 1
        res["contents"] = [film.to_dict()]
        return res
    
    def get_kinopoisk_release_date(self, film):
        try:
            codes = ["world", "russia", "digital", "dvd", "bluray", "country"]
            for code in codes:
                if (code in film["premiere"].keys() 
                        and film["premiere"][code] is not None):
                    return film["premiere"][code][0:10]
            return None
        except KeyError:
            return None

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
            return None
        month = r.group(2)
        day = r.group(1)
        year = r.group(3)
        return year+'-'+self.month_to_num(month)+'-'+day
