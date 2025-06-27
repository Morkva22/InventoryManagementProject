using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace ManagementSystem
{
    [Table("suppliers")]
    public class SupplierModel : BaseModel
    {
        [PrimaryKey("Id", true)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("ContactPerson")]
        public string ContactPerson { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        public SupplierModel Clone()
        {
            return new SupplierModel
            {
                Id = this.Id,
                Name = this.Name,
                ContactPerson = this.ContactPerson,
                Phone = this.Phone,
                Email = this.Email
            };
        }

        public static SupplierModel CreateDefault()
        {
            return new SupplierModel 
            { 
                Name = "New Supplier",
                ContactPerson = "",
                Phone = "",
                Email = ""
            };
        }
    }
}