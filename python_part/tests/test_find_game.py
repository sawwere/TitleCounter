import os
import unittest
import requests
from flask import Flask, jsonify, request
from flask import abort
from flask import make_response
from functions import *

class TestCase(unittest.TestCase):
    def setUp(self):
        app = Flask(__name__)

        @app.route('/find/games', methods=['GET'])
        async def get_game():
            name = request.args.get('title', default = "None", type = str)
            game = await choose_game(name)
            if game is None:
                abort(404)
            game.fixed_title = get_fixed_name(game.title)

            return jsonify(game.to_dict())
        
        self.app = app.test_client()

    def test_got_response(self):
        with self.app as c:
            rv = c.get('/find/games?title=a')
            assert request.args['title'] == 'a'

    def test_find_game(self):
        with self.app as c:
            rv = c.get('/find/games?title=a')
            obj = rv.json
            game = Game(**obj)
            assert game.DateCompleted == "1900-01-01"
            assert game.DateRelease == "2021-10-14"
            assert game.title == "a"
            assert game.time == 108

    def test_find_with_single_release_date(self):
        with self.app as c:
            rv = c.get('/find/games?title=LocoRoco Cocoreccho')
            obj = rv.json
            game = Game(**obj)
            assert game.DateRelease == "2007-09-20"
            assert game.title == "LocoRoco Cocoreccho"

    def test_find_with_only_release_year(self):
        with self.app as c:
            rv = c.get('/find/games?title=Replaced')
            obj = rv.json
            game = Game(**obj)
            assert game.DateRelease == "1900-01-01"

if __name__ == '__main__':
    unittest.main()