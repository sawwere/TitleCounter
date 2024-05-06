using System;
using System.Collections.Generic;

namespace hltb.Models;

public partial class Film : Content
{
    public string? RusTitle { get; set; }

    public static FilmBuilder Builder()
    {
        return new FilmBuilder();
    }

    public class FilmBuilder
    {
        private Film Film;
        public FilmBuilder()
        {
            Film = new Film();
        }

        public FilmBuilder Id(long id)
        {
            Film.Id = id;
            return this;
        }

        public FilmBuilder EntryId(long entryId)
        {
            Film.EntryId = entryId;
            return this;
        }

        public FilmBuilder UserId(long userId)
        {
            Film.UserId = userId;
            return this;
        }


        public FilmBuilder Title(string title)
        {
            Film.Title = title;
            return this;
        }

        public FilmBuilder RusTitle(string title)
        {
            Film.RusTitle = title;
            return this;
        }

        public FilmBuilder DateCompleted(DateOnly date)
        {
            Film.DateCompleted = date;
            return this;
        }

        public FilmBuilder DateRelease(DateOnly date)
        {
            Film.DateRelease = date;
            return this;
        }

        public FilmBuilder LinkUrl(string linkUrl)
        {
            Film.LinkUrl = linkUrl;
            return this;
        }

        public FilmBuilder Score(long score)
        {
            Film.Score = score;
            return this;
        }

        public FilmBuilder GlobalScore(double globalScore)
        {
            Film.GlobalScore = globalScore;
            return this;
        }

        public FilmBuilder Status(string status)
        {
            Film.Status = status;
            return this;
        }

        public FilmBuilder Time(long time)
        {
            Film.Time = time;
            return this;
        }

        public FilmBuilder Note(string? note)
        {
            Film.Note = note;
            return this;
        }

        public Film Build()
        {
            return Film;
        }
    }
}
