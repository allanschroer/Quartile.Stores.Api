using Quartile.Stores.Domain.Dtos;
using Quartile.Stores.Domain.Dtos.Endpoints.Store;
using Quartile.Stores.Domain.Models;
using Quartile.Stores.Domain.Restuls;

namespace Quartile.Stores.Domain.Interfaces.Services
{
    public interface IStoreService
    {
        OperationResult<IEnumerable<StoreDto>?> GetAllByCompanyId(int companyId);
        OperationResult<StoreDto?> GetById(int id);
        OperationResult Update(int id, CreateStoreRequest store);
        OperationResult Create(CreateStoreRequest store);
        OperationResult Delete(int id);
    }
}
