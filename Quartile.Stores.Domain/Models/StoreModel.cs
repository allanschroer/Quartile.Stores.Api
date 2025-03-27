namespace Quartile.Stores.Domain.Models
{
    public class StoreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Provider { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CompanyId { get; set; }

        public virtual CompanyModel Company { get; set; }
    }
}
