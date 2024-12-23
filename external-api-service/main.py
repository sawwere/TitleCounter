import os
from flask import Flask, jsonify, request
from flask import abort
from flask import make_response, Response
import py_eureka_client.eureka_client as eureca_client

from functions import download_image
from service.film_service import FilmService
from service.game_service import GameService

SERVER_PORT = 5000

eureca_client.init(
    eureka_server="http://localhost:8761/eureka",
    app_name="external-content-search-service",
    instance_host="localhost",
    instance_port=SERVER_PORT
)

api_keys = {}
api_keys['KP_API_KEY'] = os.environ.get('KP_API_KEY')
print(api_keys['KP_API_KEY'] is None)
app = Flask(__name__)
film_service = FilmService(api_keys)
game_service = GameService(api_keys)


@app.errorhandler(404)
def not_found(error):
    return make_response(jsonify({'error': 'Not found'}), 404)


@app.errorhandler(503)
def service_unavailabl(error):
    return make_response(jsonify({'error': 'Cant get answer from api'}), 404)


@app.route('/find/games', methods=['GET'])
def get_game():
    name = request.args.get('title', default="None", type=str)
    id = request.args.get('id', type=str)
    if id is not None:
        res = game_service.search_by_id(id)
    else:
        res = game_service.search(name)
    if res is None:
        abort(503)
    return jsonify(res)


@app.route('/find/films', methods=['GET'])
def get_film():
    name = request.args.get('title', default=None, type=str)
    page = request.args.get('page', default=1, type=int)
    id = request.args.get('id', type=str)
    if id is not None:
        res = film_service.search_by_id(id)
    elif name is None:
        res = film_service.search_by_page(page)
    else:
        res = film_service.search(name, page)
    if res is None:
        abort(503)
    return jsonify(res)


@app.route('/find/image', methods=['GET'])
def get_image():
    image_url = request.args.get(
        'image_url',
        default="https://kitairu.net/images/noimage.png",
        type=str
    )
    image_bytes = download_image(image_url)
    r = Response(response=image_bytes, status=200, mimetype="image/png")
    r.headers["Content-Type"] = "image/png"
    return r


if __name__ == '__main__':
    app.run(debug=True, port=SERVER_PORT)