﻿using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Bhasha.Common
{
    public class GenericChapter : IEntity
    {
        /// <summary>
        /// Unique identifier of the chapter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; }

        /// <summary>
        /// Level of difficulty of the chapter.
        /// </summary>
        public int Level { get; }

        /// <summary>
        /// Name of the chapter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Description of the chapter.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Sequence of pages of the chapter.
        /// </summary>
        public GenericPage[] Pages { get; }

        /// <summary>
        /// Link to an image representing the content of the chapter (optional).
        /// </summary>
        [JsonIgnore]
        public ResourceId? PictureId { get; }

        [JsonConstructor]
        public GenericChapter(int level, string name, string description, GenericPage[] pages) : this(default, level, name, description, pages) { }

        public GenericChapter(Guid id, int level, string name, string description, GenericPage[] pages, ResourceId? pictureId = default)
        {
            Id = id;
            Level = level;
            Name = name;
            Description = description;
            Pages = pages;
            PictureId = pictureId;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Level)}: {Level}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Pages)}: {string.Join('/', Pages?.Select(x => x.ToString()))}, {nameof(PictureId)}: {PictureId}";
        }
    }
}
