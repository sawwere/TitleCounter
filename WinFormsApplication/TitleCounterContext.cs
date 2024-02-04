using System;
using System.Collections.Generic;
using System.Xml.Linq;
using hltb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace hltb;

public partial class TitleCounterContext : DbContext
{
    public TitleCounterContext()
    {
    }

    public TitleCounterContext(DbContextOptions<TitleCounterContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("films_pkey");

            entity.ToTable("films");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.DateCompleted)
                .HasDefaultValueSql("'1900-01-01'::date")
                .HasColumnName("date_completed");
            entity.Property(e => e.DateRelease)
                .HasDefaultValueSql("'1900-01-01'::date")
                .HasColumnName("date_release");
            entity.Property(e => e.FixedTitle)
                .HasMaxLength(63)
                .HasDefaultValueSql("'None'::character varying")
                .HasColumnName("fixed_title");
            entity.Property(e => e.ImageUrl)
                .HasDefaultValueSql("'https://kitairu.net/images/noimage.png'::text")
                .HasColumnName("image_url");
            entity.Property(e => e.LinkUrl)
                .HasDefaultValueSql("'https://howlongtobeat.com'::text")
                .HasColumnName("link_url");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");
            entity.Property(e => e.RusTitle)
                .HasMaxLength(63)
                .HasDefaultValueSql("'None'::character varying")
                .HasColumnName("rus_title");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Time)
                .HasDefaultValueSql("0")
                .HasColumnName("time");
            entity.Property(e => e.Title)
                .HasMaxLength(63)
                .HasDefaultValueSql("'None'::character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("games_pkey");

            entity.ToTable("games");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCompleted)
                .HasDefaultValueSql("'1900-01-01'::date")
                .HasColumnName("date_completed");
            entity.Property(e => e.DateRelease)
                .HasDefaultValueSql("'1900-01-01'::date")
                .HasColumnName("date_release");
            entity.Property(e => e.FixedTitle)
                .HasMaxLength(63)
                .HasDefaultValueSql("'None'::character varying")
                .HasColumnName("fixed_title");
            entity.Property(e => e.ImageUrl)
                .HasDefaultValueSql("'https://kitairu.net/images/noimage.png'::text")
                .HasColumnName("image_url");
            entity.Property(e => e.LinkUrl)
                .HasDefaultValueSql("'https://howlongtobeat.com'::text")
                .HasColumnName("link_url");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");
            entity.Property(e => e.Platform)
                .HasMaxLength(63)
                .HasColumnName("platform");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Time)
                .HasDefaultValueSql("0")
                .HasColumnName("time");
            entity.Property(e => e.Title)
                .HasMaxLength(63)
                .HasDefaultValueSql("'None'::character varying")
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
