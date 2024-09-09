import re

class Film():
    def __init__(self,
                    movie, 
                    title = "None",
                    alternative_title = "Нету",
                    image_url="https://kitairu.net/images/noimage.png",
                    kp_id=None,   
                    imdb_id=None, 
                    time = 0,                   
                    global_score = 0,
                    date_release = "1900-01-01"
                    ):
        self.movie = movie
        self.title = title
        self.alternative_title = alternative_title
        self.image_url = image_url
        self.kp_id = kp_id
        self.imdb_id = imdb_id
        self.time = time
        self.date_release = date_release
        self.global_score = global_score
    
    def __getstate__(self):
        state = self.__dict__.copy()
        del state['movie']
        return state
    
    def to_dict(self) -> dict:
        return {"title": self.title,
                "alternative_title" : self.alternative_title,
                "image_url": self.image_url,
                "kp_id": self.kp_id,
                "imdb_id": self.imdb_id,
                "time": self.time,
                "date_release": self.date_release,
                "global_score": self.global_score
                }

