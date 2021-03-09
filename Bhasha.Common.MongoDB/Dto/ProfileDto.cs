﻿using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Bhasha.Common.MongoDB.Dto
{
    public class ProfileDto
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement]
        public Guid UserId { get; set; }

        [BsonElement]
        public string From { get; set; }

        [BsonElement]
        public string To { get; set; }

        [BsonElement]
        public int Level { get; set; }
    }
}