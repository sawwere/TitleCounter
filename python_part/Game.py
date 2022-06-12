class Game:
    def __init__(self, name = "None",
                image_url = "https://kitairu.net/images/noimage.png", 
                link="https://yandex.ru/",
                time = 0.0, 
                similarity = 0.0, 
                score=0, 
                year=-1,
                status = "Backlog"):
        self.name = name
        self.image_url = "https://howlongtobeat.com" + image_url
        self.link = link
        
        mt = str(time)
        if (type(time) == str):
            
            if ("\u00bd" in mt):
                self.time = float(mt[:-1])+0.5
            else:
                self.time = float(mt)
        else:
            self.time = time
        self.similarity = similarity
        self.score = score
        self.year = year
        self.status = status

    def save_to_sheet(self):
        file_name = "game_temp.txt"
        file = open(file_name, 'a')
        file.write(self.name+';'+self.image_url+';'+self.link
                +';'+self.time+';'+self.similarity+';'+self.score
                +';'+self.year+';'+self.status)
        file.close

    def print(self):
        print(self.name, self.image_url, self.link,
              self.time, self.similarity, self.score, self.year, self.status)

    def to_string(self):
        return str(self.name + ' ' + self.image_url + ' ' + self.link
                   + ' ' + self.time + ' ' + self.similarity
                   + ' ' + self.score + ' ' + self.year + ' ' + self.status)

    def to_dict(self):
        return {"name": self.name,
                "image_url": self.image_url,
                "link": self.link,
                "time": self.time,
                "similarity": self.similarity,
                "score": self.score,
                "year": self.year,
                "status": self.status}
