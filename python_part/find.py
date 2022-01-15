import sys
from functions import *



string = sys.argv[1].split('#')
#string = "Hollow Knight#Baclog#6#games".split('#')
#string = "Hobbit#Baclog#3#films".split('#')
#string = "House of cards#Backlog#7#tvseries".split('#')

def check(tp):
    flag = True
    result = "SUCCS"
    if tp == "games":
        a = choose_game(string[0])
        titles = read_json("game", False)
    elif tp == "films":
        a = Film(string[0])
        titles = read_json("film", False)
    elif tp == "tvseries":
        try:
            a = TVSeries(string[0])
        except:
            result = "ERROR t"
            return (result, string[0])
        else:
            titles = read_json("tvseries")

    for t in titles:
        #print(t.to_dict())
        if t.to_dict()["name"] == a.name:
            flag = False
            result = "ERROR a"
            break
    if a.name == "None":
        flag = False
        result = "ERROR f"
    if flag:
        a.status = string[1]
        a.score = int(string[2])
        if tp == "games":
            a.year = get_game_year(a.link)       
        titles.append(a)
        s = tp[:-1] if tp != "tvseries" else tp 
        create_json(titles, s, False)
        download_image(a, s)
        return (result, a.name)
    else:
        return (result, string[0])

res = check(string[3])
print(res[0], res[1])










