
# class TVSeries():
#     def __init__(self, title = "None",
#                         rus_title = "Нету",
#                         image_url="https://kitairu.net/images/noimage.png",
#                         link="https://yandex.ru/",    
#                         time = 0,                   
#                         score = 0,
#                         year=-1,
#                         status="Backlog",
#                         genres=list(),
#                         seasons=list()):
#         if time == 0:
#             self.imdb = imdb.Cinemagoer()
#             self.movie = self.imdb.search_movie(title)[0]
#             self.imdb.update(self.movie, info='main')
#             self.title = self.movie['title']
#             self.rus_title = self.movie['original title']
#             self.image_url = self.movie['cover url']
#             self.link = 'https://www.imdb.com/title/tt' + self.movie['imdbID']
#             self.time = int(self.movie['runtime'][0])
#             self.year = int(self.movie['year'])
#             self.status = status
#             self.score = int(score)
#             self.genres = list()
#             for g in self.movie['genres']:
#                 self.genres.append(g)
#             self.imdb.update(self.movie, 'episodes')
#             self.seasons = dict()
#             for s in self.movie['episodes']:
#                 self.seasons[s] = len(self.movie['episodes'][s])
#         else:
#             self.title = title
#             self.rus_title = rus_title
#             self.image_url = image_url
#             self.link = link
#             self.time = time
#             self.year = year
#             self.status = status
#             self.score = score
#             self.genres = genres
#             self.seasons = seasons

        

#     def print(self):
#         print(self.title, self.rus_title, self.image_url,
#               self.link, self.time, self.score, self.year, self.status)
#         for g in self.genres:
#             print(g)
#         for s in self.seasons:
#             print(s)
        
#     def to_string(self):
#         result = str(self.title + ' ' + self.rus_title + ' ' 
#                          + ' ' + self.image_url + ' ' + self.link 
#                          + ' ' + self.time + ' ' + self.score + ' ' + self.year 
#                          + ' ' + self.status)
#         for g in self.genres:
#             result += g + ' '
#         return result

#     def to_dict(self):
#         return {"title" : self.title,
#                 "rus_title" : self.rus_title,
#                 "image_url" : self.image_url,
#                 "link" : self.link,
#                 "time" : self.time,
#                 "score": self.score,
#                 "year" : self.year,
#                 "status" : self.status,
#                 "genres" : self.genres,
#                 "seasons" : self.seasons}

