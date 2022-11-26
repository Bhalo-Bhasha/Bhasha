﻿using System.Linq;
using AutoFixture;
using Bhasha.Web.Domain;

namespace Bhasha.Web.Tests.Support;

public static class SupportedLanguageKey
{
    private static readonly Fixture _rog = new();

    public static LangKey Create()
    {
        var native = Language.Supported.Keys.Random();
        var target = Language.Supported.Keys.Where(key => key != native).Random();

        return new LangKey(native, target);
    }
}
