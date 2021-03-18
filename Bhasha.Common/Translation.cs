﻿using System;

namespace Bhasha.Common
{
    public class Translation : IEntity
    {
        /// <summary>
        /// Unique identifier of the translation.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Reference to language-independent token description.
        /// </summary>
        public Guid TokenId { get; }

        /// <summary>
        /// Language of the translation.
        /// </summary>
        public Language Language { get; }

        /// <summary>
        /// Translation in the native script of the language.
        /// </summary>
        public string Native { get; }

        /// <summary>
        /// Translation in IPA (International Phonetic Alphabet).
        /// </summary>
        public string Spoken { get; }

        /// <summary>
        /// Optional link to an audio file.
        /// </summary>
        public ResourceId? AudioId { get; }

        public Translation(Guid id, Guid tokenId, Language language, string native, string spoken, ResourceId? audioId = default)
        {
            Id = id;
            TokenId = tokenId;
            Language = language;
            Native = native;
            Spoken = spoken;
            AudioId = audioId;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(TokenId)}: {TokenId}, {nameof(Language)}: {Language}, {nameof(Native)}: {Native}, {nameof(Spoken)}: {Spoken}, {nameof(AudioId)}: {AudioId}";
        }
    }
}