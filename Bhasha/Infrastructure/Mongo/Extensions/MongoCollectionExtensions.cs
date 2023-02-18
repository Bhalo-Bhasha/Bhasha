﻿using System.Linq.Expressions;
using MongoDB.Driver;

namespace Bhasha.Infrastructure.Mongo.Extensions;

public static class MongoCollectionExtensions
{
    public static IEnumerable<string> CreateIndices<T>(this IMongoCollection<T> collection, params Expression<Func<T, object>>[] selectors)
    {
        var indices = selectors
            .Select(Builders<T>.IndexKeys.Ascending)
            .Select(x => new CreateIndexModel<T>(x));

        return collection.Indexes.CreateMany(indices);
    }

    public static IEnumerable<string> CreateIndices<T>(this IMongoCollection<T> collection, params string[] fields)
    {
        var indices = fields
            .Select(x => Builders<T>.IndexKeys.Ascending(x))
            .Select(x => new CreateIndexModel<T>(x));

        return collection.Indexes.CreateMany(indices);
    }
}
