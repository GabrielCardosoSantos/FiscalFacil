﻿
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FiscalFacil.Database
{
    public class Database<T> : IDatabase<T> where T: class, new()
    {
        private SQLiteAsyncConnection db;

        public Database(SQLiteAsyncConnection dbPath)
        {
            db = dbPath;
        }

        public AsyncTableQuery<T> AsQueryable() =>
            db.Table<T>();

        public async Task<List<T>> Get() =>
            await db.Table<T>().ToListAsync();

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = db.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }

        public async Task<T> Get(int id) =>
             await db.FindAsync<T>(id);
        public async Task<T> Get(Expression<Func<T, bool>> predicate) =>
            await db.FindAsync<T>(predicate);
        public async Task<T> Get<TValue>(Expression<Func<T, TValue>> predicate) =>
            await db.FindAsync<T>(predicate);
        public async Task<int> Insert(T entity) =>
             await db.InsertAsync(entity);
        public async Task<int> Update(T entity) =>
             await db.UpdateAsync(entity);
        public async Task<int> Delete(T entity) =>
             await db.DeleteAsync(entity);
    }
}
