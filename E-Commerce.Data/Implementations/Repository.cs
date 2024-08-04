using System;
using System.Linq.Expressions;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Implimentations
{
	public class Repository<T>:IRepository<T>where T:BaseEntity
	{
        private readonly DataContext _context;
        private readonly DbSet<T> _table;
		public Repository(DataContext context)
		{
            _context = context;
            _table = context.Set<T>();
		}

        public async Task Commit()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Create(T entity)
        {
            try
            {
                entity.CreatedAt = DateTime.Now;
                if (entity.Id==null)
                {
                    entity.Id = Guid.NewGuid().ToString();

                }
                var resoult = _context.Entry(entity);
                resoult.State = EntityState.Added;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(T entity)
        {
            try
            {
                var resoult = _context.Entry(entity);
                resoult.State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            try
            {
                IQueryable<T> query = _table;

                if (includes.Length>0)
                {
                    query = GetAllIncludes(includes);
                }
                return predicate == null ? await query.ToListAsync() : await query.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetEntity(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            try
            {
                IQueryable<T> query = _table;
                if (includes.Length>0)
                {
                    query = GetAllIncludes(includes);
                }
                return predicate == null ? await query.FirstOrDefaultAsync() : await query.FirstOrDefaultAsync(predicate);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                return predicate == null ? false : await _table.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                var resoult = _context.Entry(entity);
                resoult.State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IQueryable<T>GetAllIncludes(params string[] includes)
        {
            try
            {
                IQueryable<T> query = _table;
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

