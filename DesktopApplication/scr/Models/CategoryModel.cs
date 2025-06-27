namespace ManagementSystem;

using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

[Table("categories")]
public class CategoryModel : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
}