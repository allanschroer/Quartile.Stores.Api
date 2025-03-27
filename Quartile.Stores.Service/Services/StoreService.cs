using AutoMapper;
using Microsoft.Extensions.Logging;
using Quartile.Stores.Domain.Dtos;
using Quartile.Stores.Domain.Dtos.Endpoints.Store;
using Quartile.Stores.Domain.Interfaces.Repositories;
using Quartile.Stores.Domain.Interfaces.Services;
using Quartile.Stores.Domain.Models;
using Quartile.Stores.Domain.Restuls;
using Quartile.Stores.Infra.Context;

namespace Quartile.Stores.Service.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ILogger<StoreService> _logger;
        private readonly IMapper _mapper;
        private readonly StoresContext _appContext;

        public StoreService(IStoreRepository storeRepository, ILogger<StoreService> logger, IMapper mapper, StoresContext appContext)
        {
            _storeRepository = storeRepository;
            _logger = logger;
            _mapper = mapper;
            _appContext = appContext;
        }

        public OperationResult<IEnumerable<StoreDto>?> GetAllByCompanyId(int companyId)
        {
            try
            {
                var entities = _storeRepository.GetAllByCompanyId(companyId);
                var dtos = _mapper.Map<IEnumerable<StoreDto>>(entities);
                return OperationResult<IEnumerable<StoreDto>?>.CreateSuccess(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Get All Stores");
                return OperationResult<IEnumerable<StoreDto>?>.CreateFailure("An error ocurred while trying to get all stores.");
            }
        }

        public OperationResult<StoreDto?> GetById(int id)
        {
            try
            {
                var entity = _storeRepository.GetById(id);

                if (entity == null)
                    return OperationResult<StoreDto?>.CreateFailure("Store not found.");

                return OperationResult<StoreDto?>.CreateSuccess(_mapper.Map<StoreDto>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Get Store by Id");
                return OperationResult<StoreDto?>.CreateFailure("An error ocurred while trying to get a store.");
            }
        }

        public OperationResult Create(CreateStoreRequest store)
        {
            try
            {
                var entity = _mapper.Map<StoreModel>(store);
                _storeRepository.Add(entity);
                _appContext.SaveChanges();

                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Create Store");
                return OperationResult.CreateFailure("An error ocurred while trying to create a store.");
            }
        }

        public OperationResult Update(int id, CreateStoreRequest store)
        {
            try
            {
                var entity = _storeRepository.GetById(id);

                if (entity == null)
                    return OperationResult<StoreDto?>.CreateFailure("Store not found.");

                entity = _mapper.Map(store, entity);
                _storeRepository.Update(entity);
                _appContext.SaveChanges();

                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Update Store");
                return OperationResult.CreateFailure("An error ocurred while trying to update a store.");
            }
        }

        public OperationResult Delete(int id)
        {
            try
            {
                var entity = _storeRepository.GetById(id);

                if (entity == null)
                    return OperationResult.CreateFailure("Store not found.");

                _storeRepository.Delete(entity);
                _appContext.SaveChanges();

                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Delete Store");
                return OperationResult.CreateFailure("An error ocurred while trying to delete a store.");
            }
        }
    }
}
