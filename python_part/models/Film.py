import imdb
import re

class Film():
    def __init__(self, title = "None",
                       fixed_title = "None",
                       rus_title = "Нету",
                       image_url="https://kitairu.net/images/noimage.png",
                       link_url="https://yandex.ru/",    
                       time = 0,                   
                       score = 0,
                       date_release = "1900-01-01",
                       date_completed = "1900-01-01",
                       note = "",
                       status_id = 2):
        if time == 0:
            self.imdb = imdb.Cinemagoer()
            self.movie = self.imdb.search_movie(title)[0]
            self.imdb.update(self.movie, info='main')
            self.title = self.movie['title']
            self.fixed_title = fixed_title
            self.rus_title = self.movie['original title']
            self.image_url = self.movie['cover url']
            self.link_url = 'https://www.imdb.com/title/tt' + self.movie['imdbID']
            try:
                self.time = float(self.movie['runtime'][0])
            except:
                self.time = 0
            date_release = self.movie['original air date']
            self.date_release = self.string_into_date(date_release)

            self.date_completed = date_completed
        else:
            self.title = title
            self.fixed_title = fixed_title
            self.rus_title = rus_title
            self.image_url = image_url
            self.link_url = link_url
            self.time = time

        self.status_id = status_id
        self.score = int(score)
        self.note = note
        
    def to_string(self):
        result = str(self.title + ' ' + self.rus_title + ' ' 
                         + ' ' + self.image_url + ' ' + self.link_url 
                         + ' ' + self.time + ' ' + self.score + ' ' + self.date_release 
                         + ' ' + self.status_id)
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
        month = r.group(2)
        day = r.group(1)
        year = r.group(3)
        return year+'-'+self.month_to_num(month)+'-'+day
    
    


    def to_dict(self):
        return {"Title": self.title,
                "FixedTitle": self.fixed_title,
                "RusTitle" : self.rus_title,
                "ImageUrl": self.image_url,
                "LinkUrl": self.link_url,
                "Time": self.time,
                "StatusId" : self.status_id,
                "DateRelease": self.date_release,
                "DateCompleted": self.date_completed,
                "Note": self.note,
                "Score": self.score}

