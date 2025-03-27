namespace Quartile.Stores.Domain.Dtos
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Provider { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }

        public CompanyDto Company { get; set; }
    }
}
