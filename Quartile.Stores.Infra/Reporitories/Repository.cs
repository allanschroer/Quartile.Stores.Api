using Microsoft.EntityFrameworkCore;
using Quartile.Stores.Domain.Interfaces.Repositories;
using Quartile.Stores.Infra.Configuration;

namespace Quartile.Stores.Infra.Reporitories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly StoresContext _context;
        public DbSet<T> Set { get; private set; }

        public Repository(StoresContext context)
        {
            _context = context;
            Set = _context.Set<T>();
        }

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
