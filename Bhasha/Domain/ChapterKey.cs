﻿namespace Bhasha.Domain;

public record ChapterKey(int ChapterId, ProfileKey ProfileKey)
{
    public override string ToString()
    {
        return $"{ChapterId}:[{ProfileKey}]";
    }
}
