using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace hltb;

public partial class TitleCounterContext : DbContext
{
    public TitleCounterContext()
    {
    }

    public TitleCounterContext(DbContextOptions<TitleCounterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder
        .UseLazyLoadingProxies()
        .UseNpgsql("Host=localhost;Port=5432;Database=TitleCounter;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("games_pkey");

            entity.ToTable("games");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCompleted)
                .HasDefaultValueSql("'1000-01-01'::date")
                .HasColumnName("date_completed");
            entity.Property(e => e.DateRelease)
                .HasDefaultValueSql("'1000-01-01'::date")
                .HasColumnName("date_release");
            entity.Property(e => e.FixedTitle)
                .HasMaxLength(63)
                .HasDefaultValueSql("'None'::character varying")
                .HasColumnName("fixed_title");
            entity.Property(e => e.ImageUrl)
                .HasDefaultValueSql("'https://howlongtobeat.com'::text")
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
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Time)
                .HasDefaultValueSql("0")
                .HasColumnName("time");
            entity.Property(e => e.Title)
                .HasMaxLength(63)
                .HasDefaultValueSql("'None'::character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.Status).WithMany(p => p.Games)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("status");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("statuses_pkey");

            entity.ToTable("statuses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(16)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
