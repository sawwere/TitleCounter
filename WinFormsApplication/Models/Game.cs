using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hltb.Models;

public partial class Game : Content
{
    public string? Platform { get; set; }

    public static GameBuilder builder()
    {
        return new GameBuilder();
    }

    public class GameBuilder
    {
        private Game game;
        public GameBuilder()
        {
            game = new Game();
        }

        public GameBuilder id(long id)
        {
            game.Id = id;
            return this;
        }

        public GameBuilder entryId(long entryId) {
            game.EntryId = entryId;
            return this;
        }

        public GameBuilder userId(long userId)
        {
            game.UserId = userId;
            return this;
        }


        public GameBuilder title(string title)
        {
            game.Title = title;
            return this;
        }

        public GameBuilder dateCompleted(DateOnly date)
        {
            game.DateCompleted = date;
            return this;
        }

        public GameBuilder dateRelease(DateOnly date)
        {
            game.DateRelease = date;
            return this;
        }

        public GameBuilder platform(string platform)
        {
            game.Platform = platform;
            return this;
        }

        public GameBuilder imageUrl(string imageUrl)
        {
            game.ImageUrl = imageUrl;
            return this;
        }

        public GameBuilder linkUrl(string linkUrl)
        {
            game.LinkUrl = linkUrl;
            return this;
        }

        public GameBuilder score(long score)
        {
            game.Score = score;
            return this;
        }

        public GameBuilder globalScore(double globalScore)
        {
            game.GlobalScore = globalScore;
            return this;
        }

        public GameBuilder status(string status)
        {
            game.Status = status;
            return this;
        }

        public GameBuilder time(long time)
        {
            game.Time = time;
            return this;
        }

        public GameBuilder globalTime(long globalTime)
        {
            game.GlobalTime = globalTime;
            return this;
        }

        public GameBuilder note(string note)
        {
            game.Note = note;
            return this;
        }

        public Game build()
        {
            return game;
        }
    }
}
