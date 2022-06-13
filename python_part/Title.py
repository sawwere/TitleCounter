from sqlalchemy import false


class Title():
    def __init__(self, name="None",
                 image_url="https://kitairu.net/images/noimage.png",
                 link="https://yandex.ru/",
                 time=0,
                 score=0,
                 year=-1,
                 status="Backlog"):
        self.name = name
        self.image_url = image_url
        self.link = link
        self.time = float(time)
        self.score = int(score)
        self.year = int(year)
        self.status = status
        self.has_image = false
        self.image_name = name

