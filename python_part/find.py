import sys
from turtle import title
from functions import *



string = sys.argv[1].split('#')
#string = "Hollow Knight#Backlog#6#games".split('#')
#string = "Hobbit#Backlog#3#films".split('#')
#string = "House of cards#Backlog#7#tvseries".split('#')

def check(tp):
    flag = True
    result = "SUCCS"
    if tp == "games":
        a = choose_game(string[0])
        titles = read_json("games", False)
    elif tp == "films":
        a = Film(string[0])
        titles = read_json("films", False)
    elif tp == "tvseries":
        try:
            a = TVSeries(string[0])
        except:
            result = "ERROR t"
            return (result, string[0])
        else:
            titles = read_json("tvseries", False)

    for t in titles:
        if t.to_dict()["name"] == a.name:
            flag = False
            result = "ERROR a"
            break
    if a.name == "None":
        flag = False
        result = "ERROR f"

    temp = list()
    temp.append(a)
    create_json(temp, "temp", False)
    download_image(a, tp)
    if flag:
        a.status = string[1]
        a.score = int(string[2])
        return (result, a.name)
    else:
        return (result, string[0])

res = check(string[3])
print(res[0], res[1], sep="#")










