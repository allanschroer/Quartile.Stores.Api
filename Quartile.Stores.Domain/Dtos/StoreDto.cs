namespace Quartile.Stores.Domain.Dtos
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }

        public CompanyDto Company { get; set; }
    }
}
