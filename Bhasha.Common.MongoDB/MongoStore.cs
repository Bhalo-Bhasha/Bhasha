﻿using System;
using System.Threading.Tasks;
using Bhasha.Common.Database;
using Bhasha.Common.MongoDB.Extensions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Bhasha.Common.MongoDB
{
    public class MongoStore<TProduct> : IStore<TProduct>
        where TProduct : class, IEntity, ICanBeValidated
    {
        private readonly IMongoClient _db;

        public MongoStore(IMongoClient db)
        {
            _db = db;
        }

        public async Task<TProduct> Add(TProduct product)
        {
            product.Validate();
            await _db.Collection<TProduct>().InsertOneAsync(product);
            return product;
        }

        public async Task<TProduct?> Get(Guid id)
        {
            return await _db
                .Collection<TProduct>()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Remove(Guid id)
        {
            var result = await _db
                .Collection<TProduct>()
                .DeleteOneAsync(x => x.Id == id);

            return (int)result.DeletedCount;
        }

        public async Task Replace(TProduct product)
        {
            await _db
                .Collection<TProduct>()
                .ReplaceOneAsync(x => x.Id == product.Id, product);
        }
    }
}
