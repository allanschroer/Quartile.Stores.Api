﻿namespace Quartile.Stores.Domain.Models
{
    public class StoreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        public int CompanyId { get; set; }

        public virtual CompanyModel Company { get; set; }
    }
}
