using Microsoft.EntityFrameworkCore;
using Quartile.Stores.Domain.Interfaces.Repositories;

namespace Quartile.Stores.Infra.Reporitories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbSet<T> Set { get; set; }

        public void Add(T entity)
        {
            Set.Add(entity);
        }

        public void Delete(T entity)
        {
            Set.Remove(entity);
        }

        public void Update(T entity)
        {
            Set.Update(entity);
        }
    }
}
