package DAO;

import com.TitleCounter.DataAccess.Models.Film;
import com.TitleCounter.DataAccess.Utils.HibernateSessionFactoryUtil;
import org.hibernate.Session;
import org.hibernate.Transaction;

import java.util.List;

public class FilmDao {
    public Film findById(long id) {
        return HibernateSessionFactoryUtil.getSessionFactory().openSession().get(Film.class, id);
    }

    public void save(Film film) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.save(film);
        tx1.commit();
        session.close();
    }

    public void update(Film film) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.merge(film);
        tx1.commit();
        session.close();
    }

    public void delete(Film film) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.delete(film);
        tx1.commit();
        session.close();
    }

    public List<Film> findAll() {
        List<Film> films = (List<Film>)  HibernateSessionFactoryUtil
                .getSessionFactory().openSession().createQuery("From Film").list();
        return films;
    }
}
