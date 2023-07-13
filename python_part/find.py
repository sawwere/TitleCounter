import sys
import base64
from turtle import title
from functions import *


# <mode>;;<title>;;<status_id>;;<score>

string = sys.argv[1]
#string = "games;;LittleBigPlanet 2;;1;;6"
#string = "films;;Hobbit;;1;;3"
#string = "tvseries;;House of cards;;1#7"


def check(query_string):
    query_parts = query_string.split(';;')
    tp = query_parts[0]
    op_status = 0
    json_string = ""

    if tp == "games":
        content = choose_game(query_parts[1])
        #titles = read_json("games", False)
    elif tp == "films":
        content = Film(query_parts[0])
        #titles = read_json("films", False)
    elif tp == "tvseries":
        try:
            content = TVSeries(query_parts[1])
        except:
            op_status = 3  # incorrect mode
            return (op_status, query_parts[1])
        else:
            pass
            #titles = read_json("tvseries", False)
    set_fixed_name(content)
    
    if content.title == "None":
        op_status = 1 # not found
    else:
        content.status_id = int(query_parts[2])
        content.score = int(query_parts[3])    
    # temp = list()
    # temp.append(content)
    # create_json(temp, "temp", False)
    # download_image(content, tp)
    # if flag:
    #     content.status = string[1]
    #     content.score = int(string[2])
    #     return (op_status, content.name)
    # else:
    #     return (op_status, string[0])
    json_string = json.dumps(content.to_dict())
    base64_image = base64.b64encode(download_image(content, "games"))
    return (op_status, json_string, base64_image)

res = check(string)
print(res[0], res[1], res[2], sep=";;")










