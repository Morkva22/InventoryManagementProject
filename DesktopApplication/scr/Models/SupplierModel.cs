namespace ManagementSystem;

using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

[Table("suppliers")]
public class SupplierModel : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("contact_person")]
    public string ContactPerson { get; set; }
    
    [Column("phone")]
    public string Phone { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
}