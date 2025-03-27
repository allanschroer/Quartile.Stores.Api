using Microsoft.EntityFrameworkCore;

namespace Quartile.Stores.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Set { get; set; }
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
