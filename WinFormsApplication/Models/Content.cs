﻿namespace hltb.Models
{
    public enum TitleStatus { COMPLETED = 1, BACKLOG, RETIRED, IN_PROGRESS };

    public class Content
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string FixedTitle { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string? LinkUrl { get; set; }

        public long? Time { get; set; }

        public string Status { get; set; }

        public DateOnly DateRelease { get; set; }

        public DateOnly DateCompleted { get; set; }

        public string? Note { get; set; }

        public long Score { get; set; }

        public Content()
        {

        }
    }
}
