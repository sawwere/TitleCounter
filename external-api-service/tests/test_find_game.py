import os
import unittest
import requests
from flask import Flask, jsonify, request
from flask import abort
from flask import make_response
from service.game_service import GameService
from models.Game import Game

class TestCase(unittest.TestCase):
    def setUp(self):
        app = Flask(__name__)
        self.service = GameService({})

        @app.route('/find/games', methods=['GET'])
        def get_game():
            name = request.args.get('title', default = "None", type = str)
            id = request.args.get('id', type = str)
            if id != None:
                res = self.service.search_by_id(id)
            else:
                res = self.service.search(name)
            if res is None:
                abort(503)
            return jsonify(res)
        
        self.app = app.test_client()

    def test_got_response(self):
        with self.app as c:
            rv = c.get('/find/games?title=a')
            assert request.args['title'] == 'a'

    def test_find_game(self):
        with self.app as c:
            rv = c.get('/find/games?title=a')
            obj = rv.json
            d = dict(**obj)
            assert d['contents'][0]["date_release"] == "2021-10-14"
            assert d['contents'][0]["title"] == "a"
            assert d['contents'][0]["time"] == 108

    def test_find_with_single_release_date(self):
        with self.app as c:
            rv = c.get('/find/games?title=LocoRoco Cocoreccho')
            obj = rv.json
            game = dict(**obj)["contents"][0]
            assert game["date_release"] == "2007-09-20"
            assert game["title"] == "LocoRoco Cocoreccho"

    def test_find_with_only_release_year(self):
        with self.app as c:
            rv = c.get('/find/games?title=Replaced')
            obj = rv.json
            game = dict(**obj)["contents"][0]
            assert game["date_release"] == None

if __name__ == '__main__':
    unittest.main()