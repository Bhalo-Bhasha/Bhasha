﻿using System;
using System.Linq;
using Bhasha.Common.Extensions;
using Bhasha.Common.MongoDB.Exceptions;

namespace Bhasha.Common.MongoDB.Dto
{
    public static class Converter
    {
        public static Procedure Convert(ProcedureDto dto)
        {
            try
            {
                var id = new ProcedureId(dto.ProcedureId);
                var tutorial = dto.Tutorial != default ? dto.Tutorial.Select(x => new ResourceId(x)).ToArray() : default;
                var support = dto.Support.Select(Enum.Parse<TokenType>).ToArray();
                var audio = ResourceId.Create(dto.AudioId);

                return new Procedure(id, dto.Description, tutorial, audio, support);
            }
            catch (Exception e)
            {
                throw new InvalidProcedureException($"loaded invalid procedure {dto.Stringify()}", e);
            }
        }

        public static Translation Convert(TranslationDto dto, string fromId, string toId)
        {
            try
            {
                var categories = dto.Categories.Select(x => new Category(x)).ToArray();
                var level = Enum.Parse<LanguageLevel>(dto.Level);
                var tokenType = Enum.Parse<TokenType>(dto.TokenType);
                var pictureId = ResourceId.Create(dto.PictureId);
                var reference = new Token(new TokenId(dto.GroupId, dto.SequenceNumber),
                    dto.Label, level, tokenType, categories, pictureId);

                var fromToken = dto.Tokens.First(x => x.LanguageId == fromId);
                var fromAudioId = ResourceId.Create(fromToken.AudioId);
                var from = new LanguageToken(Language.Parse(fromId),
                    fromToken.Native, fromToken.Spoken, fromAudioId);

                var toToken = dto.Tokens.First(x => x.LanguageId == toId);
                var toAudioId = ResourceId.Create(toToken.AudioId);
                var to = new LanguageToken(Language.Parse(toId),
                    toToken.Native, toToken.Spoken, toAudioId);

                return new Translation(reference, from, to);
            }
            catch (Exception e)
            {
                throw new InvalidTranslationException($"loaded invalid translation {dto.Stringify()} from {fromId} to {toId}", e);
            }
        }

        public static UserProgress Convert(UserProgressDto dto)
        {
            try
            {
                var userId = new EntityId(dto.UserId);
                var from = Language.Parse(dto.From);
                var to = Language.Parse(dto.To);

                var stats = new UserStats(
                    dto.GroupId,
                    dto.CompletedTokens,
                    dto.CompletedChapters,
                    dto.CompletedSequenceNumbers.ToHashSet(),
                    Enum.Parse<LanguageLevel>(dto.Level));

                return new UserProgress(userId, from, to, stats);
            }
            catch (Exception e)
            {
                throw new InvalidUserProgressException($"loaded invalid user progress {dto.Stringify()}", e);
            }
        }
    }
}
