import re

class Film():
    def __init__(self,
                    movie, 
                    title = "None",
                    rus_title = "Нету",
                    image_url="https://kitairu.net/images/noimage.png",
                    link_url="https://yandex.ru/",    
                    time = 0,                   
                    global_score = 0,
                    date_release = "1900-01-01"
                    ):
        self.movie = movie
        self.title = title
        self.rus_title = rus_title
        self.image_url = image_url
        self.link_url = link_url
        self.time = time
        self.date_release = date_release
        self.global_score = global_score
        
    def to_string(self) -> str:
        result = str(self.title + ' ' + self.rus_title + ' ' 
                         + ' ' + self.image_url + ' ' + self.link_url 
                         + ' ' + self.time + ' ' + self.global_score + ' ' + self.date_release)
        return result
    
    def __getstate__(self):
        state = self.__dict__.copy()
        del state['movie']
        return state
    
    def to_dict(self) -> dict:
        return {"title": self.title,
                "rus_title" : self.rus_title,
                "image_url": self.image_url,
                "link_url": self.link_url,
                "time": self.time,
                "date_release": self.date_release,
                "global_score": self.global_score
                }

