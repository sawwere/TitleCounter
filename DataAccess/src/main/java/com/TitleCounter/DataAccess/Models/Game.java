package com.TitleCounter.DataAccess.Models;

import jakarta.persistence.*;

@Entity
@Table(name="games")
public class Game {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    @Column(name = "platform")
    private String platform;

    @Column(name = "title")
    private String title;

    @Column(name = "fixed_title")
    private String fixed_title;

    @Column(name = "image_url")
    private String image_url;

    @Column(name = "link_url")
    private String link_url;

    @Column(name = "time")
    private long time;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "status_id")
    private Status status;

    @Column(name = "date_release")
    private String date_release;

    @Column(name = "date_completed")
    private String date_completed;

    @Column(name = "note")
    private String note;

    @Column(name = "score")
    private long score;


    public Game() {
    }

    public Game(String platform, String title, String fixed_title, String image_url, String link_url, long time, Status status, String date_release, String date_completed, String note, long score) {
        this.platform = platform;
        this.title = title;
        this.fixed_title = fixed_title;
        this.image_url = image_url;
        this.link_url = link_url;
        this.time = time;
        this.status = status;
        this.date_release = date_release;
        this.date_completed = date_completed;
        this.note = note;
        this.score = score;
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

    public Status getStatus() {
        return status;
    }

    public void setStatus(Status status) {
        this.status = status;
    }

    public String getDate_release() {
        return date_release;
    }

    public void setDate_release(String date_release) {
        this.date_release = date_release;
    }

    public String getDate_completed() {
        return date_completed;
    }

    public void setDate_completed(String date_completed) {
        this.date_completed = date_completed;
    }

    public String getNote() {
        return note;
    }

    public void setNote(String note) {
        this.note = note;
    }

    public long getScore() {
        return score;
    }

    public void setScore(long score) {
        this.score = score;
    }
}
