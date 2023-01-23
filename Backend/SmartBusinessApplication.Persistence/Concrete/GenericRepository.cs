using Microsoft.EntityFrameworkCore;
using SmartBusinessApplication.Application.Abstract;
using SmartBusinessApplication.Domain.Entity.BaseEntity;
using SmartBusinessApplication.Persistence.Data.Context;
using System.Linq.Expressions;

namespace SmartBusinessApplication.Persistence.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T:BaseClass
    {
        private DataContext _Context;
        public GenericRepository(DataContext Context)
        {
            _Context= Context;
        }
        public async Task<bool> CreateAsycn(T T)
        {
            T.CreatedDate = DateTime.Now;
            var result = await _Context.Set<T>().AddAsync(T);
            return result.State == EntityState.Added;
        }

        public bool Delete(T T)
        {
            var result = _Context.Set<T>().Remove(T);
            return result.State == EntityState.Deleted;
        }


        public IQueryable<T> GetAll(bool tracking = false)
        {
            var liste = _Context.Set<T>().AsQueryable();
            if (!tracking)
            {
                liste = liste.AsNoTracking();
            }
            return liste;
        }

        public IQueryable<T> GetAllFilterAsync(Expression<Func<T, bool>> expression, bool tracking = false)
        {
            var liste = _Context.Set<T>().AsQueryable();
            if (!tracking)
            {
                liste = liste.AsNoTracking();
            }
            return liste.Where(expression);
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> expression, bool tracking = false)
        {
            var liste = _Context.Set<T>().AsQueryable();
            if (!tracking)
            {
                liste = liste.AsNoTracking();
            }
            return await liste.FirstOrDefaultAsync(expression);
        }

        public async Task<T> GetByIdAsync(int Id, bool tracking = false)
        {
            var liste = _Context.Set<T>().AsQueryable();
            if (!tracking)
            {
                liste = liste.AsNoTracking();
            }
            return await liste.FirstOrDefaultAsync(i=>i.Id == Id);
        }

        public async Task<int> SaveAsync()
        {
            var state = await _Context.SaveChangesAsync();
            return state;
        }

        public bool Update(T T)
        {
            T.UpdatedDate = DateTime.Now;
            var result = _Context.Set<T>().Update(T);
            return result.State == EntityState.Modified;
        }

    }
}
