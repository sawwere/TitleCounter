import imdb
import re

from models.Film import Film

class FilmService:
    def __init__(self) -> None:
        self.imdb = imdb.Cinemagoer()

    def search(self, title) -> Film:
        movies = self.imdb.search_movie(title)

        if (len(movies) > 0):
            m = movies[0]
            print(m)
            self.imdb.update(m, info='main')

            film = Film(movie = m, 
                    title = m['title'],
                    rus_title = m['original title'],
                    link_url = 'https://www.imdb.com/title/tt' + m['imdbID'],
                    image_url =m['cover url'],
                    global_score= m.data['rating'])
            try:
                film.time = float(m['runtime'][0])
            except:
                print("WARN: no time found for film " + title)

            if m.has_key('original air date'):
                DateRelease = m['original air date']
                film.date_release = self.string_into_date(DateRelease)
            
            return film
        else:
            return None
        
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