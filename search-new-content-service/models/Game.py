import re
import os
import requests
from bs4 import BeautifulSoup

headers = {
    'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}

class Game:
    def __init__(self, 
                 platforms,
                 hltb_id = None,
                 steam_id = None,
                 title = "ERROR_MISSING_TITLE",
                 game_type = "game",
                 developer = None,
                 description = None,
                 image_url = None,
                 time = None,
                 date_release = None,
                 score = 0,
                 similarity = 0.0
                 ):
        self.external_id = {"hltb_id": hltb_id, "steam_id": steam_id}
        self.title = title
        self.game_type = game_type
        self.developer = developer
        self.description = description
        self.image_url = image_url       

        mt = str(time)
        if (type(time) == str):
            if ("\u00bd" in mt):
                self.time = int(mt[:-1])+0.5
            else:
                self.time = int(mt)
        else:
            self.time = time

        self.date_release = date_release
        self.score = score
        self.similarity = similarity
        self.platforms = platforms

    def to_dict(self):
        return {
                "title": self.title,
                "game_type": self.game_type,
                "developer": self.developer,
                "description": self.description,
                "external_id": self.external_id,
                "platforms": self.platforms,
                "time": self.time,
                "date_release": self.date_release,
                "global_score": self.score,
                "image_url": self.image_url
                }
