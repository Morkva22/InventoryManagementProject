using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace ManagementSystem
{
    [Table("products")]
    public class ProductModel : BaseModel
    {
        [PrimaryKey("productid", false)] // false означает, что ключ НЕ должен включаться в INSERT
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = "New product";
        [Column("description")]
        public string Description { get; set; } = "";

        [Column("categoryid")]
        public int CategoryId { get; set; } = 1;

        [Column("supplierid")]
        public int SupplierId { get; set; } = 1;

        [Column("purchaseprice")]
        public decimal PurchasePrice { get; set; } = 1;

        [Column("sellingprice")]
        public decimal SellingPrice { get; set; } = 1;

        [Column("createddate")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Метод валидации
        public bool IsValid(out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(Name))
            {
                errorMessage = "The product name cannot be empty";                return false;
            }

            if (PurchasePrice <= 0)
            {
                errorMessage = "The purchase price must be greater than 0";                return false;
            }

            if (SellingPrice <= 0)
            {
                errorMessage = "The selling price must be greater than 0";                return false;
            }

            if (CategoryId <= 0)
            {
                errorMessage = "A category must be selected";                return false;
            }

            if (SupplierId <= 0)
            {
                errorMessage = "A supplier must be selected";                return false;
            }

            return true;
        }

        public ProductModel Clone()
        {
            return new ProductModel
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                CategoryId = this.CategoryId,
                SupplierId = this.SupplierId,
                PurchasePrice = this.PurchasePrice,
                SellingPrice = this.SellingPrice,
                CreatedAt = this.CreatedAt
            };
        }
    }
}