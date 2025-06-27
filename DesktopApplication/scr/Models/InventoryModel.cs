namespace ManagementSystem;

using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

[Table("inventory")]
public class InventoryModel : BaseModel
{
    [PrimaryKey("inventory_id")]
    public int Id { get; set; }
    
    [Column("product_id")]
    public int ProductId { get; set; }
    
    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("min_stock")]
    public int MinStock { get; set; }
    
    [Column("last_updated")]
    public DateTime LastUpdated { get; set; }
}