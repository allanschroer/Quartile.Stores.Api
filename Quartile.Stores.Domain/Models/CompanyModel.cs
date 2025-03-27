namespace Quartile.Stores.Domain.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<StoreModel> Stores { get; set; }
    }
}
