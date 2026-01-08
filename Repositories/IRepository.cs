using System.Linq.Expressions;

namespace Net9RestApi.Repositories
{
    // T: Üzerinde çalışacağımız Entity (User, Article vb.)
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        
        // Filtreleme için opsiyonel metod (Örn: Kategorisine göre makaleleri getir)
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}