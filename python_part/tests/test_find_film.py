import os
import unittest
import requests
from flask import Flask, jsonify, request
from flask import abort
from flask import make_response
from models.Film import Film

class TestCase(unittest.TestCase):
    def setUp(self):
        app = Flask(__name__)

        @app.route('/find/films', methods=['GET'])
        async def get_film():
            name = request.args.get('title', default = "None", type = str)
            film = Film(name)
            if film is None:
                abort(404)
            return jsonify(film.to_dict())
        self.app = app.test_client()

    def test_got_response(self):
        with self.app as c:
            rv = c.get('/find/films?title=matrix')
            assert request.args['title'] == 'matrix'

    def test_find_film(self):
        with self.app as c:
            rv = c.get('/find/films?title=matrix')
            obj = rv.json
            film = Film(**obj)
            #assert film.DateRelease == "1999-10-14"
            assert film.Title == "The Matrix"
            assert film.Time == 136

if __name__ == '__main__':
    unittest.main()