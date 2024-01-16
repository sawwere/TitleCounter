from flask import Flask, jsonify, request
from flask import abort
from flask import make_response

import base64

from functions import *
from models.Film import Film
from models.Game import Game
from models.TVSeries import TVSeries


app = Flask(__name__)

@app.errorhandler(404)
def not_found(error):
    return make_response(jsonify({'error': 'Not found'}), 404)

@app.route('/find/games', methods=['GET'])
async def get_game():
    name = request.args.get('title', default = "None", type = str)
    game = await choose_game(name)
    if game is None:
        abort(404)
    game.fixed_title = get_fixed_name(game.title)

    return jsonify(game.to_dict())

@app.route('/find/films', methods=['GET'])
async def get_film():
    name = request.args.get('title', default = "None", type = str)
    game = Film(name)
    if game is None:
        abort(404)
    game.fixed_title = get_fixed_name(game.title)

    return jsonify(game.to_dict())

@app.route('/find/image', methods=['GET'])
async def get_image():
    image_url = request.args.get('image_url', default = "https://kitairu.net/images/noimage.png", type = str)
    string = await download_image(image_url)
    base64_image = base64.b64encode(string)
    if base64_image is None:
        abort(404)
    return base64_image.decode()

#curl -i "http://localhost:5000/find/games?title=a&status=1&score=0"
#curl -i "http://localhost:5000/find/image?image_url=https://howlongtobeat.com/games/100639_a.jpg"
if __name__ == '__main__':
    app.run(debug=True)