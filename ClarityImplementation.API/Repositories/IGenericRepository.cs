using System.Linq.Expressions;

namespace ClarityImplementation.API.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);
        Task<T> Update(T entity,int id);
        Task<bool> Delete(int id);

        Task<T> GetByCompanyId(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllByCompanyId(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllByPageId(Expression<Func<T, bool>> predicate);

        Task<bool> DeleteByCompanyId(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllByStatus(Expression<Func<T, bool>> predicate);

       

    }
}
