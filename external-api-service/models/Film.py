class Film():
    def __init__(self,
                    movie, 
                    title = "ERROR NOT FOUND",
                    ru_title = None,
                    en_title = None,
                    image_url="https://kitairu.net/images/noimage.png",
                    kp_id=None,   
                    imdb_id=None, 
                    tmdb_id=None,
                    description=None,
                    time = 0,   
                    year_release = None,               
                    global_score = 0,
                    date_release = "1900-01-01"
                    ):
        self.movie = movie
        self.title = title
        self.ru_title = ru_title
        self.en_title = en_title
        self.image_url = image_url
        self.external_id = {"kp_id": kp_id, "imdb_id": imdb_id, "tmdb_id": tmdb_id}
        self.description = description
        self.time = time
        self.year_release = year_release
        self.date_release = date_release
        self.global_score = global_score
    
    def __getstate__(self):
        state = self.__dict__.copy()
        del state['movie']
        return state
    
    def to_dict(self) -> dict:
        return {"title": self.title,
                "ru_title" : self.ru_title,
                "en_title" : self.en_title,
                "image_url": self.image_url,
                "external_id": self.external_id,
                "description": self.description,
                "time": self.time,
                "year_release": self.year_release,
                "date_release": self.date_release,
                "global_score": self.global_score
                }

