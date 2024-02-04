package DAO;

import com.TitleCounter.DataAccess.Models.Game;
import com.TitleCounter.DataAccess.Utils.HibernateSessionFactoryUtil;
import org.hibernate.Session;
import org.hibernate.Transaction;

import java.util.List;

public class GameDao {
    public Game findById(long id) {
        return HibernateSessionFactoryUtil.getSessionFactory().openSession().find(Game.class, id);
    }

    public void save(Game game) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.save(game);
        tx1.commit();
        session.close();
    }

    public void update(Game game) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.merge(game);
        tx1.commit();
        session.close();
    }

    public void delete(Game game) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.delete(game);
        tx1.commit();
        session.close();
    }

    public List<Game> findAll() {
        List<Game> games = (List<Game>)  HibernateSessionFactoryUtil
                .getSessionFactory().openSession().createQuery("From Game").list();
        return games;
    }
}
