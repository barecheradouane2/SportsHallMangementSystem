using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Sportshall.Core.interfaces;
using Sportshall.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Sportshall.Core.Sharing;

namespace Sportshall.infrastructure.Repositries
{
    public class GenericRepositry<T> : IGenericRepositry <T> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepositry(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
          return  await _context.Set<T>().AsNoTracking().ToListAsync();

        }

        //public async Task<IReadOnlyList<T>> GetAllAsync(GeneralParams generalParams)
        //{
        //    var query = _context.Set<T>().AsQueryable();

        //    if (!string.IsNullOrEmpty(generalParams.Search))
        //    {
        //        query = query.Where(e => EF.Property<string>(e, "Name").Contains(generalParams.Search));
        //    }

        //    if (!string.IsNullOrEmpty(generalParams.Sort))
        //    {
        //        query = query.OrderBy(e => EF.Property<string>(e, generalParams.Sort));
        //    }


        //    if (generalParams.PageNumber > 0 && generalParams.PageSize > 0)
        //    {
        //        query = query.Skip((generalParams.PageNumber - 1) * generalParams.PageSize)
        //                     .Take(generalParams.PageSize);
        //    }

        //    return await query.ToListAsync();
        //}


        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (Expression < Func<T, object> > item in includes)
            {
                query = query.Include(item);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (Expression<Func<T, object>> item in includes)
            {
                query = query.Include(item);
            }

            var entity  =  await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            return entity;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {

            _context.Entry(entity).State = EntityState.Modified;

        
             await  _context.SaveChangesAsync();
        }

        public Task  DeleteAsync(int id)
        {
           var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                return _context.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }

        public async Task<int> CountAsync()
        {
            
            return await _context.Set<T>().CountAsync();

        }
    }
    
}
