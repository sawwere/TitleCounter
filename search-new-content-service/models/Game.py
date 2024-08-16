import re
import os
import requests
from bs4 import BeautifulSoup

headers = {
    'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}

class Game:
    def __init__(self, 
                 title = "None",
                 image_url = "https://kitairu.net/images/noimage.png",
                 link_url = "https://howlongtobeat.com",
                 time = 0,
                 date_release = "1900-01-01",
                 score = 0,
                 similarity = 0.0
                 ):
        self.title = title
        self.image_url = image_url
        self.link_url = link_url
        
        mt = str(time)
        if (type(time) == str):
            if ("\u00bd" in mt):
                self.time = int(mt[:-1])+0.5
            else:
                self.time = int(mt)
        else:
            self.time = time
        self.DateRelease = self.find_DateRelease(link_url)
        self.score = score
        self.similarity = similarity

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


    def find_DateRelease(self, web_link):
        DateRelease = "1900-01-01"
        try:
            r = requests.get(web_link, headers=headers)
            
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

    def print(self):
        print(self.title, self.image_url, self.link_url,
              self.time, self.DateRelease, self.score)

    def to_string(self):
        return str(self.title 
                   + ' ' + self.image_url + ' ' + self.link_url
                   + ' ' + self.time + ' ' 
                   + ' ' + self.score + ' ' + self.DateRelease)

    def to_dict(self):
        return {
                "title": self.title,
                "image_url": self.image_url,
                "link_url": self.link_url,
                "time": self.time,
                "date_release": self.DateRelease,
                "score": self.score
                }
