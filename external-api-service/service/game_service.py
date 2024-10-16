import re
import requests

from bs4 import BeautifulSoup
from howlongtobeatpy import HowLongToBeat, SearchModifiers

from models.Game import Game

LIMIT = 5


class GameService:
    def __init__(self, api_keys) -> None:
        self.api_keys = api_keys
        self.headers = {
    'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}
    
    def search(self, title: str) -> list:
        """ 
        OUTDATED
        """
        searh_results = HowLongToBeat().search(
            game_name=title,
            similarity_case_sensitive=False,
            search_modifiers=SearchModifiers.HIDE_DLC
        )
        if searh_results is None:
            return None
        searh_results.sort(key=lambda x: x.similarity, reverse=True)    
        
        count = min(LIMIT, len(searh_results))
        arr = []
        for i in range(0, count):
            sr = searh_results[i]
            r = requests.get(sr.game_web_link, headers=self.headers)

            soup = BeautifulSoup(r.text.encode('utf-8'),
                                 features="html.parser")
            date_release = self.get_hltb_release_date(soup)
            steam_id = self.get_steam_id(soup)
            if (sr.profile_platforms is not None
                    and len(sr.profile_platforms) > 0):
                platforms = sr.profile_platforms
            else:
                platforms = self.get_platforms(soup)
            genres = self.get_genres(soup)

            developer = self.get_developer(soup)

            game = Game(platforms=platforms,
                        genres=genres,
                        title=sr.game_name,
                        hltb_id=sr.game_id,
                        steam_id=steam_id,
                        game_type=sr.game_type,
                        developer=developer,
                        description=None,
                        image_url=sr.game_image_url,
                        time=sr.main_story*60,
                        date_release=date_release,
                        score=sr.review_score,
                        similarity=sr.similarity
                        )
            arr.append(game)
        res = {}
        res = {}
        res["total"] = len(arr)
        res["contents"] = [x.to_dict() for x in arr]
        return res
    
    def search_by_id(self, id: str) -> list:
        """ 
        Function that searches the game with given hltb_id
        """
        searh_results = HowLongToBeat().search_from_id(id)
        if searh_results is None:
            return None
        print("===================================")
        print('\033[92m'+id + " " + searh_results.game_type+'\033[0m')
        print("===================================")
        r = requests.get(searh_results.game_web_link, headers=self.headers)
        soup = BeautifulSoup(r.text.encode('utf-8'), features="html.parser")

        date_release = self.get_hltb_release_date(soup)
        steam_id = self.get_steam_id(soup)
        if (searh_results.profile_platforms is not None
                and len(searh_results.profile_platforms) > 0):
            platforms = searh_results.profile_platforms
        else:
            platforms = self.get_platforms(soup)
        genres = self.get_genres(soup)

        developer = self.get_developer(soup)

        game = Game(platforms=platforms,
                    genres=genres,
                    title=searh_results.game_name,
                    hltb_id=searh_results.game_id,
                    steam_id=steam_id,
                    game_type=searh_results.game_type,
                    developer=developer,
                    description=None,
                    image_url=searh_results.game_image_url,
                    time=searh_results.main_story*60,
                    date_release=date_release,
                    score=searh_results.review_score,
                    similarity=searh_results.similarity
                    )
        res = {}
        res["total"] = 1
        res["contents"] = [game.to_dict()]
        return res
    
    def parse_hltb(self, web_link):
        steam_id = None
        date_release = None
        platforms = []
        try:
            r = requests.get(web_link, headers=self.headers)
            soup = BeautifulSoup(r.text.encode('utf-8'), features="html.parser")
            date_release = self.get_hltb_release_date(soup)
            steam_id = self.get_steam_id(soup)
            platforms = self.get_platforms(soup)
        except:
            print("===================================")
            print('\033[31m'+id + " " + web_link + '\033[0m')
            print("===================================")
        finally:
            return (steam_id, date_release, platforms)

    def get_steam_id(self, soup: BeautifulSoup):
        divs = soup.find('a', {'class': 'StoreButton_steam__RJCCL StoreButton_store_botton__IrB3D'})
        if divs is not None:
            steam_id = divs.attrs['href'][35:-1]
            return steam_id
        return None
    
    def get_platforms(self, soup: BeautifulSoup):
        #try get text from html
        string_platforms = ''
        divs = soup.find_all('div', {'class': 'GameSummary_profile_info__HZFQu GameSummary_medium___r_ia'})
        for div in divs:
            if (div.contents[0].text.startswith('Platform')
                    and len(div.contents[0].text) < 13):
                string_platforms = div.contents[3]
        if string_platforms == '':
            divs = soup.find_all('div', {'class': 'GameSummary_profile_info__HZFQu GameSummary_large__TIGhL'})
            for div in divs:
                if (div.contents[0].text.startswith('Platform')
                        and len(div.contents[0].text) < 13):
                    string_platforms = div.contents[3]
        if string_platforms == '':
            print('NOT FOUND PLATFORMS')
        return [p.strip() for p in string_platforms.split(', ')]
    
    def get_genres(self, soup: BeautifulSoup):
        #try get text from html
        string_genres = ''
        divs = soup.find_all('div', {'class': 'GameSummary_profile_info__HZFQu GameSummary_medium___r_ia'})
        for div in divs:
            if div.contents[0].text.startswith('Genre'):
                string_genres = div.contents[3]
        if string_genres == '':
            print('NOT FOUND GENRES')
            return []
        return [p.strip() for p in string_genres.split(', ')]
    
    def get_developer(self, soup: BeautifulSoup):
        #try get text from html
        string_developer = ''
        divs = soup.find_all('div', {'class': 'GameSummary_profile_info__HZFQu GameSummary_medium___r_ia'})
        for div in divs:
            if div.contents[0].text.startswith('Developer'):
                string_developer = div.contents[3]
        if string_developer == '':
            divs = soup.find_all('div', {'class': 'GameSummary_profile_info__HZFQu GameSummary_large__TIGhL'})
            for div in divs:
                if div.contents[0].text.startswith('Developer'):
                    string_developer = div.contents[3]
        if string_developer == '':
            print('NOT FOUND DEVELOPER')
            return None
        return string_developer.strip()
        
    def get_hltb_release_date(self, soup: BeautifulSoup):
        try:
            # get text from html
            divs = soup.find_all('div', {'class': 'GameSummary_profile_info__HZFQu'})
            dates = []
            for div in divs:
                if len(div.attrs['class']) == 1:
                    string_date = div.contents[3]
                    extracted_date = self.string_into_date(string_date)
                    if extracted_date is not None:
                        dates.append(extracted_date)

            dates.sort()
            return dates[0]
        except:
            return None

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
            return None
        month = r.group(1)
        day = r.group(2)
        year = r.group(3)
        return year+'-'+self.month_to_num(month)+'-'+day