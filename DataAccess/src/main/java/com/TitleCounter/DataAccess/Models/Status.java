package com.TitleCounter.DataAccess.Models;
import jakarta.persistence.*;

@Entity
@Table(name="statuses")
public class Status {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    @Column(name = "Name")
    private String Name;
}
