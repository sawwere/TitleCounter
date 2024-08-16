from flask import Flask, jsonify, request
from flask import abort
from flask import make_response, send_file, Response
import py_eureka_client.eureka_client as eureca_client

import base64
from functions import *
from service.film_service import FilmService
from service.game_service import GameService

eureca_client.init(
    eureka_server="http://localhost:8761/eureka",
    app_name="external-content-search-service",
    instance_host="localhost",
    instance_port=5000
)
app = Flask(__name__)
film_service = FilmService()
game_service = GameService()

@app.errorhandler(404)
def not_found(error):
    return make_response(jsonify({'error': 'Not found'}), 404)

@app.errorhandler(503)
def service_unavailabl(error):
    return make_response(jsonify({'error': 'Cant get answer from api'}), 404)

@app.route('/find/games', methods=['GET'])
def get_game():
    name = request.args.get('title', default = "None", type = str)
    games = game_service.search(name)
    if games is None:
        abort(503)
    res = {}
    res["total"] = len(games)
    res["contents"] = [x.to_dict() for x in games]
    return jsonify(res)

@app.route('/find/films', methods=['GET'])
def get_film():
    name = request.args.get('title', default = "None", type = str)
    page = request.args.get('page', default = 1, type = int)
    films = film_service.search(name, page)
    if films is None:
        abort(503)
    res = {}
    res["total"] = len(films)
    res["contents"] = [x.to_dict() for x in films]
    return jsonify(res)

@app.route('/find/image', methods=['GET'])
def get_image():
    image_url = request.args.get('image_url', default = "https://kitairu.net/images/noimage.png", type = str)
    image_bytes = download_image(image_url)
    r = Response(response=image_bytes, status=200, mimetype="image/png")
    r.headers["Content-Type"] = "image/png"
    return r

#curl -i "http://localhost:5000/find/films?title=matrix"
#curl -i "http://localhost:5000/find/image?image_url=https://howlongtobeat.com/games/100639_a.jpg"
if __name__ == '__main__':
    app.run(debug=True)