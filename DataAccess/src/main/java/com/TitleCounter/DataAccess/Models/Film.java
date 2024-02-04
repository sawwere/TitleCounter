package com.TitleCounter.DataAccess.Models;

import jakarta.persistence.*;

import java.time.LocalDate;

@Entity
@Table(name="films")
public class Film {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    @Column(name = "platform")
    private String platform;

    @Column(name = "title")
    private String title;

    @Column(name = "fixed_title")
    private String fixed_title;

    @Column(name = "rus_title")
    private String rus_title;

    @Column(name = "image_url")
    private String image_url;

    @Column(name = "link_url")
    private String link_url;

    @Column(name = "time")
    private long time;

    @Column(name = "status")
    private String status;

    @Column(name = "date_release")
    private LocalDate date_release;

    @Column(name = "date_completed")
    private LocalDate date_completed;

    @Column(name = "note")
    private String note;

    @Column(name = "score")
    private short score;


    public Film() {
    }

    public long getId() {
        return id;
    }

    public String getPlatform() {
        return platform;
    }

    public void setPlatform(String platform) {
        this.platform = platform;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getFixed_title() {
        return fixed_title;
    }

    public void setFixed_title(String fixed_title) {
        this.fixed_title = fixed_title;
    }

    public String getRus_title() {
        return rus_title;
    }

    public void setRus_title(String rus_title) {
        this.rus_title = rus_title;
    }

    public String getImage_url() {
        return image_url;
    }

    public void setImage_url(String image_url) {
        this.image_url = image_url;
    }

    public String getLink_url() {
        return link_url;
    }

    public void setLink_url(String link_url) {
        this.link_url = link_url;
    }

    public long getTime() {
        return time;
    }

    public void setTime(long time) {
        this.time = time;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public LocalDate getDate_release() {
        return date_release;
    }

    public void setDate_release(LocalDate date_release) {
        this.date_release = date_release;
    }

    public LocalDate getDate_completed() {
        return date_completed;
    }

    public void setDate_completed(LocalDate date_completed) {
        this.date_completed = date_completed;
    }

    public String getNote() {
        return note;
    }

    public void setNote(String note) {
        this.note = note;
    }

    public short getScore() {
        return score;
    }

    public void setScore(short score) {
        this.score = score;
    }
}
