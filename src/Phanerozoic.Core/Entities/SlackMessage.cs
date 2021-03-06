﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Phanerozoic.Core.Entities;
//
//    var slackMessage = SlackMessage.FromJson(jsonString);

namespace Phanerozoic.Core.Entities
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SlackMessage
    {
        [JsonProperty("attachments", NullValueHandling = NullValueHandling.Ignore)]
        public List<Attachment> Attachments { get; set; }
    }

    public partial class Attachment
    {
        [JsonProperty("fallback", NullValueHandling = NullValueHandling.Ignore)]
        public string Fallback { get; set; }

        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("pretext", NullValueHandling = NullValueHandling.Ignore)]
        public string Pretext { get; set; }

        [JsonProperty("author_name", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthorName { get; set; }

        [JsonProperty("author_link", NullValueHandling = NullValueHandling.Ignore)]
        public Uri AuthorLink { get; set; }

        [JsonProperty("author_icon", NullValueHandling = NullValueHandling.Ignore)]
        public Uri AuthorIcon { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("title_link", NullValueHandling = NullValueHandling.Ignore)]
        public Uri TitleLink { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public List<Field> Fields { get; set; }

        [JsonProperty("image_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ImageUrl { get; set; }

        [JsonProperty("thumb_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ThumbUrl { get; set; }

        [JsonProperty("footer", NullValueHandling = NullValueHandling.Ignore)]
        public string Footer { get; set; }

        [JsonProperty("footer_icon", NullValueHandling = NullValueHandling.Ignore)]
        public Uri FooterIcon { get; set; }

        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long? Ts { get; set; }
    }

    public partial class Field
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("short", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Short { get; set; }
    }

    public partial class SlackMessage
    {
        public static SlackMessage FromJson(string json) => JsonConvert.DeserializeObject<SlackMessage>(json, Phanerozoic.Core.Entities.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this SlackMessage self) => JsonConvert.SerializeObject(self, Phanerozoic.Core.Entities.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}