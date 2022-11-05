﻿using Bhasha.Web.Domain;

namespace Bhasha.Web.Interfaces;

public interface IChapterProvider
{
	Task<ChapterDescription[]> GetChapters(Guid profileId);
	Task<DisplayedChapter> GetChapter(Guid profileId, Guid chapterId);
}
