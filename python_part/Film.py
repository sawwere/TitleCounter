import imdb


class Film():
    def __init__(self, title = "None",
                       fixed_title = "None",
                       rus_title = "Нету",
                       image_url="https://kitairu.net/images/noimage.png",
                       link_url="https://yandex.ru/",    
                       time = 0,                   
                       score = 0,
                       year=-1,
                       status_id = 2,
                       genres = list()):
        if time == 0:
            self.imdb = imdb.Cinemagoer()
            self.movie = self.imdb.search_movie('matrix')[0]
            print(self.movie)
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
            self.year = int(self.movie['year'])
            self.status_id = status_id
            self.score = int(score)
            self.genres = list()
            for g in self.movie['genres']:
                self.genres.append(g)
        else:
            self.title = title
            self.fixed_title = fixed_title
            self.rus_title = rus_title
            self.image_url = image_url
            self.link_url = link_url
            self.time = time
            self.year = year
            self.status_id = status_id
            self.score = score
            self.genres = genres

    def print(self):
        print(self.title, self.rus_title, self.image_url,
              self.link_url, self.time, self.score, self.year, self.status_id)
        for g in self.genres:
            print(g)
        
    def to_string(self):
        result = str(self.title + ' ' + self.rus_title + ' ' 
                         + ' ' + self.image_url + ' ' + self.link_url 
                         + ' ' + self.time + ' ' + self.score + ' ' + self.year 
                         + ' ' + self.status_id)
        for g in self.genres:
            result += g + ' '
        return result

    def to_dict(self):
        return {"Title": self.title,
                "FixedTitle": self.fixed_title,
                "rus_title" : self.rus_title,
                "ImageUrl": self.image_url,
                "linkUrl": self.link_url,
                "Time": self.time,
                "StatusId" : self.status_id,
                "DateRelease": self.date_release,
                "DateCompleted": self.date_completed,
                "Note": self.note,
                "Score": self.score,
                "genres" : self.genres}

