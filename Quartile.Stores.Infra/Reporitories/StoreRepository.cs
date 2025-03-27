using Quartile.Stores.Domain.Interfaces.Repositories;
using Quartile.Stores.Domain.Models;
using Quartile.Stores.Infra.Configuration;

namespace Quartile.Stores.Infra.Reporitories
{
    public class StoreRepository : Repository<StoreModel>, IStoreRepository
    {
        public StoreRepository(StoresContext context) : base(context)
        {
        }

        public StoreModel? GetById(int id)
        {
            return Set.Find(id);
        }

        public IEnumerable<StoreModel> GetAllByCompanyId(int companyId)
        {
            return Set.Where(a => a.CompanyId == companyId).ToList();
        }
    }
}
