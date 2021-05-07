using DataAccessLayer.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entyties.Base;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext dbContext;

        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<T> GetRange<T>(bool tracking, Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
            where T : BaseTable
        {
            var query = this.Include(tracking, includeProperties);
            return query.Where(predicate);
        }

        public T Get<T>(bool tracking, Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
            where T : BaseTable
        {
            var query = this.Include(tracking, includeProperties);
            return query.AsEnumerable().Where(e => predicate(e)).FirstOrDefault();
        }

        public T Add<T>(T exemplar)
            where T : BaseTable
        {
            T newExemplar = this.dbContext.Set<T>().Add(exemplar).Entity;
            this.dbContext.SaveChanges();
            return newExemplar;
        }

        public void AddRange<T>(IEnumerable<T> range)
            where T : BaseTable
        {
            this.dbContext.Set<T>().AddRange(range);
            this.dbContext.SaveChanges();
        }

        public void DeleteRange<T>(IEnumerable<T> range)
            where T : BaseTable
        {
            this.dbContext.Set<T>().RemoveRange(range);
            this.dbContext.SaveChanges();
        }

        public void Delete<T>(T exemplar)
            where T : BaseTable
        {
            this.dbContext.Set<T>().Remove(exemplar);
            this.dbContext.SaveChanges();
        }

        public void UpdateRange<T>(IEnumerable<T> exemplars)
            where T : BaseTable
        {
            this.dbContext.Set<T>().UpdateRange(exemplars);
            this.dbContext.SaveChanges();
        }

        public void Update<T>(T exemplar)
            where T : BaseTable
        {
            this.dbContext.Set<T>().Update(exemplar);
            this.dbContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetRangeAsync<T>(bool tracking, Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
            where T : BaseTable
        {
            var query = await this.Include(tracking, includeProperties).ToListAsync();
            return query.Where(predicate);
        }

        public async Task<T> GetAsync<T>(bool tracking, Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
            where T : BaseTable
        {
            var query = await this.Include(tracking, includeProperties).ToListAsync();
            return query.FirstOrDefault(predicate);
        }

        public async Task<T> AddAsync<T>(T exemplar)
            where T : BaseTable
        {
            var newExemplarTask = await this.dbContext.Set<T>().AddAsync(exemplar);
            await this.dbContext.SaveChangesAsync();
            return newExemplarTask.Entity;
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> range)
            where T : BaseTable
        {
            await this.dbContext.Set<T>().AddRangeAsync(range);
            await this.dbContext.SaveChangesAsync();
        }

        public Task DeleteRangeAsync<T>(IEnumerable<T> range)
            where T : BaseTable
        {
            this.dbContext.Set<T>().RemoveRange(range);
            return this.dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync<T>(T exemplar)
            where T : BaseTable
        {
            this.dbContext.Set<T>().Remove(exemplar);
            return this.dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync<T>(T exemplar)
            where T : BaseTable
        {
            this.dbContext.Set<T>().Update(exemplar);
            return this.dbContext.SaveChangesAsync();
        }

        public Task UpdateRangeAsync<T>(IEnumerable<T> range)
            where T : BaseTable
        {
            this.dbContext.Set<T>().UpdateRange(range);
            return this.dbContext.SaveChangesAsync();
        }

        private IQueryable<T> Include<T>(bool tracking, params Expression<Func<T, object>>[] includeProperties)
            where T : BaseTable
        {
            if (tracking)
            {
                IQueryable<T> query = this.dbContext.Set<T>();
                return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            else
            {
                IQueryable<T> query = this.dbContext.Set<T>().AsNoTracking();
                return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
        }
    }
}
