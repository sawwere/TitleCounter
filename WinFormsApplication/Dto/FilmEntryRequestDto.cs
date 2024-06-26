﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hltb.Dto
{
    public class FilmEntryRequestDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }


        [JsonPropertyName("custom_title")]
        public string CustomTitle { get; set; }

        [JsonPropertyName("note")]
        public string? Note { get; set; }

        [JsonPropertyName("score")]
        public long Score { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("date_completed")]
        public string DateCompleted { get; set; }

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("film_id")]
        public long FilmId { get; set; }

        public static FilmEntryDtoBuilder builder() { return new FilmEntryDtoBuilder(); }

        public class FilmEntryDtoBuilder
        {
            private FilmEntryRequestDto _result;
            public FilmEntryDtoBuilder()
            {
                _result = new FilmEntryRequestDto();
            }

            public FilmEntryDtoBuilder Id(long id)
            {
                _result.Id = id;
                return this;
            }

            public FilmEntryDtoBuilder FilmId(long FilmId)
            {
                _result.FilmId = FilmId;
                return this;
            }

            public FilmEntryDtoBuilder UserId(long userId)
            {
                _result.UserId = userId;
                return this;
            }

            public FilmEntryDtoBuilder CustomTitle(string title)
            {
                _result.CustomTitle = title;
                return this;
            }

            public FilmEntryDtoBuilder DateCompleted(DateOnly date)
            {
                _result.DateCompleted = $"{date.Year}-{string.Format("{0:D2}", date.Month)}-{string.Format("{0:D2}", date.Day)}";
                return this;
            }

            public FilmEntryDtoBuilder Score(long score)
            {
                _result.Score = score;
                return this;
            }

            public FilmEntryDtoBuilder Status(string status)
            {
                _result.Status = status;
                return this;
            }

            public FilmEntryDtoBuilder Note(string? note)
            {
                _result.Note = note;
                return this;
            }

            public FilmEntryRequestDto Build()
            {
                return _result;
            }
        }
    }
}
