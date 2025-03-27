namespace Quartile.Stores.Domain.Dtos
{
    public class CompanyDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
