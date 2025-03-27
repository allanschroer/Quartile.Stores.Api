namespace Quartile.Stores.Domain.Dtos.Endpoints.Store
{
    public class CreateStoreRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
    }
}
