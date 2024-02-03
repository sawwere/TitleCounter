package com.TitleCounter.DataAccess.Services;

import DAO.GameDao;
import com.TitleCounter.DataAccess.Models.Game;
import org.springframework.stereotype.Service;

import java.util.List;
@Service
public class GameService {
        private GameDao gameDao = new GameDao();

        public GameService() {
        }

        public Game findGame(long id) {
            return gameDao.findById(id);
        }

        public void saveGame(Game game) {
            gameDao.save(game);
        }

        public void deleteGame(Game game) {
            gameDao.delete(game);
        }

        public void updateGame(Game game) {
            gameDao.update(game);
        }

        public List<Game> findAllTopics() {
            return gameDao.findAll();
        }
}
