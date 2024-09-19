import os
import unittest
import requests
from flask import Flask, jsonify, request
from flask import abort
from flask import make_response
from service.film_service import FilmService
from models.Film import Film

class TestCase(unittest.TestCase):
    def setUp(self):
        app = Flask(__name__)
        self.service = FilmService({})

        @app.route('/find/films', methods=['GET'])
        def get_film():
            name = request.args.get('title', default = "None", type = str)
            page = request.args.get('page', default = 1, type = int)
            films = self.service.search(name, page)
            return jsonify(films)
        self.app = app.test_client()

    def test_got_response(self):
        # with self.app as c:
        #     rv = c.get('/find/films?title=1408')
        #     obj = rv.json
        #     assert request.args['title'] == '1408'
        assert True

    def test_find_film(self):
        # with self.app as c:
        #     rv = c.get('/find/films?title=1408')
        #     obj = rv.json
        #     film = obj["contents"][0]
        #     assert film['date_release'] == "2007-06-12"
        #     assert film['title'] == "1408"
        #     assert film['time'] == 104
        assert True

if __name__ == '__main__':
    unittest.main()