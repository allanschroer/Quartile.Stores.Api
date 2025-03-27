using AutoMapper;
using Quartile.Stores.Domain.Dtos;
using Quartile.Stores.Domain.Dtos.Endpoints.Store;
using Quartile.Stores.Domain.Models;

namespace Quartile.Stores.Api.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<CompanyModel, CompanyDto>().ReverseMap();
            CreateMap<StoreModel, StoreDto>().ReverseMap();
            CreateMap<CreateStoreRequest, StoreModel>().ReverseMap();
        }
    }
}
