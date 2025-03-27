namespace Quartile.Stores.Domain.Dtos.Endpoints.Store
{
    public class CreateStoreRequest
    {
        public string Name { get; set; }
        public string Provider { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
    }
}
