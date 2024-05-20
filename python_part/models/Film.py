import imdb
import re

class Film():
    def __init__(self, Title = "None",
                       RusTitle = "Нету",
                       ImageUrl="https://kitairu.net/images/noimage.png",
                       LinkUrl="https://yandex.ru/",    
                       Time = 0,                   
                       Score = 0,
                       DateRelease = "1900-01-01"
                       ):

        self.imdb = imdb.Cinemagoer()
        self.movie = self.imdb.search_movie(Title)[0]
        self.imdb.update(self.movie, info='main')
        self.Title = self.movie['title']
        self.RusTitle = self.movie['original title']
        self.ImageUrl = self.movie['cover url']
        self.LinkUrl = 'https://www.imdb.com/title/tt' + self.movie['imdbID']
        try:
            self.Time = float(self.movie['runtime'][0])
        except:
            self.Time = 0
        

        if self.movie.has_key('original air date'):
            DateRelease = self.movie['original air date']
            self.DateRelease = self.string_into_date(DateRelease)
        self.score = self.movie.data['rating']
        
    def to_string(self):
        result = str(self.Title + ' ' + self.RusTitle + ' ' 
                         + ' ' + self.ImageUrl + ' ' + self.LinkUrl 
                         + ' ' + self.Time + ' ' + self.score + ' ' + self.DateRelease)
        return result
    
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

    def to_dict(self):
        return {"Title": self.Title,
                "RusTitle" : self.RusTitle,
                "ImageUrl": self.ImageUrl,
                "LinkUrl": self.LinkUrl,
                "Time": self.Time,
                "DateRelease": self.DateRelease,
                "Score": self.score
                }

