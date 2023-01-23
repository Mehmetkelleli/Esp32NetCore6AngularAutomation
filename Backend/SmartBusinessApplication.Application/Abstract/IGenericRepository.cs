using Microsoft.Extensions.Logging;
using SmartBusinessApplication.Domain.Entity.BaseEntity;
using System.Linq.Expressions;

namespace SmartBusinessApplication.Application.Abstract
{
    public interface IGenericRepository<T> where T :BaseClass
    {
        Task<bool> CreateAsycn(T T);
        bool Update(T T);
        bool Delete(T T);
        Task<T> GetByIdAsync(int Id,bool tracking = false);
        IQueryable<T> GetAll(bool stracking = false);
        IQueryable<T> GetAllFilterAsync(Expression<Func<T, bool>> expression, bool stracking = false);
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> expression, bool stracking = false);
        Task<int> SaveAsync();
       }
}
