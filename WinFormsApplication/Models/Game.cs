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

        public GameBuilder Id(long id)
        {
            game.Id = id;
            return this;
        }

        public GameBuilder EntryId(long entryId) {
            game.EntryId = entryId;
            return this;
        }

        public GameBuilder UserId(long userId)
        {
            game.UserId = userId;
            return this;
        }


        public GameBuilder Title(string title)
        {
            game.Title = title;
            return this;
        }

        public GameBuilder DateCompleted(DateOnly date)
        {
            game.DateCompleted = date;
            return this;
        }

        public GameBuilder DateRelease(DateOnly date)
        {
            game.DateRelease = date;
            return this;
        }

        public GameBuilder Platform(string? platform)
        {
            game.Platform = platform;
            return this;
        }

        public GameBuilder ImageUrl(string imageUrl)
        {
            game.ImageUrl = imageUrl;
            return this;
        }

        public GameBuilder LinkUrl(string linkUrl)
        {
            game.LinkUrl = linkUrl;
            return this;
        }

        public GameBuilder Score(long score)
        {
            game.Score = score;
            return this;
        }

        public GameBuilder GlobalScore(double globalScore)
        {
            game.GlobalScore = globalScore;
            return this;
        }

        public GameBuilder Status(string status)
        {
            game.Status = status;
            return this;
        }

        public GameBuilder Time(long time)
        {
            game.Time = time;
            return this;
        }

        public GameBuilder GlobalTime(long globalTime)
        {
            game.GlobalTime = globalTime;
            return this;
        }

        public GameBuilder Note(string? note)
        {
            game.Note = note;
            return this;
        }

        public Game Build()
        {
            return game;
        }
    }
}
