class Game:
    def __init__(self, 
                 title = "None",
                 fixed_title = "None",
                 image_url = "https://kitairu.net/images/noimage.png",
                 link_url = "https://howlongtobeat.com",
                 time = 0, 
                 status_id = 2,
                 date_release = "1900-01-01",
                 date_completed = "1900-01-01",
                 note = "",
                 platform = "",
                 score = 0,
                 similarity = 0.0
                 ):
        self.title = title
        self.fixed_title = fixed_title
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

        self.status_id = status_id
        self.date_release = date_release
        self.date_completed = date_completed
        self.note = note
        self.platform = platform
        self.score = score
        self.similarity = similarity

    def print(self):
        print(self.title, self.fixed_title, self.image_url, self.link_url,
              self.time, self.status_id, self.date_release, self.date_completed, self.score)

    def to_string(self):
        return str(self.title + ' ' + self.fixed_title 
                   + ' ' + self.image_url + ' ' + self.link_url
                   + ' ' + self.time + ' ' 
                   + ' ' + self.score + ' ' + self.date_release + ' ' + self.status_id)

    def to_dict(self):
        return {"Title": self.title,
                "FixedTitle": self.fixed_title,
                "ImageUrl": self.image_url,
                "linkUrl": self.link_url,
                "Time": self.time,
                "StatusId": self.status_id,
                "DateRelease": self.date_release,
                "DateCompleted": self.date_completed,
                "Note": self.note,
                "Platform": self.platform,
                "Score": self.score
                }
