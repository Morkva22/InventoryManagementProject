namespace ManagementSystem;

using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

[Table("products")]
public class ProductModel : BaseModel
{
    [Column("productID")]
    public int id { get; set; }
    
    [Column("Name")]
    public string Name { get; set; }
    
    [Column("Description")]
    public string Description { get; set; }
    
    [Column("CategoryID")]
    public int CategoryID { get; set; }
    
    [Column("SupplierID")]
    public int SupplierID { get; set; }
    
    [Column("PurchasePrice")]
    public decimal PurchasePrice { get; set; }
    
    [Column("SellingPrice")]
    public decimal SellingPrice { get; set; }
    
    [Column("CreatedDate")]
    public DateTime created_at { get; set; }
}