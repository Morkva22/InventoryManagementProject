using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace ManagementSystem
{
    [Table("inventory")]
    public class InventoryModel : BaseModel
    {
        [PrimaryKey("InventoryID", true)]
        public int Id { get; set; }

        [Column("ProductID")]
        public int ProductId { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("LastUpdated")]
        public DateTime LastUpdated { get; set; }

        [Column("MinStock")]
        public int MinStock { get; set; }

        public InventoryModel Clone()
        {
            return new InventoryModel
            {
                Id = this.Id,
                ProductId = this.ProductId,
                Quantity = this.Quantity,
                LastUpdated = this.LastUpdated,
                MinStock = this.MinStock
            };
        }
    }
}