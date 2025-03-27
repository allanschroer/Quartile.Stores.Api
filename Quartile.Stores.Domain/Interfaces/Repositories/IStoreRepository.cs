using Quartile.Stores.Domain.Models;

namespace Quartile.Stores.Domain.Interfaces.Repositories
{
    public interface IStoreRepository : IRepository<StoreModel>
    {
        StoreModel? GetById(int id);
        IEnumerable<StoreModel> GetAllByCompanyId(int companyId);

    }
}
