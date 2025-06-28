using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace ManagementSystem
{
    [Table("products")]
    public class ProductModel : BaseModel
    {
        [PrimaryKey("productid", true)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = "Новый продукт";

        [Column("description")]
        public string Description { get; set; } = "";

        [Column("categoryid")]
        public int CategoryId { get; set; } = 1; // Значение по умолчанию

        [Column("supplierid")]
        public int SupplierId { get; set; } = 1; // Значение по умолчанию

        [Column("purchaseprice")]
        public decimal PurchasePrice { get; set; } = 1; // Минимальная цена по умолчанию

        [Column("sellingprice")]
        public decimal SellingPrice { get; set; } = 1; // Минимальная цена по умолчанию

        [Column("createddate")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Метод валидации
        public bool IsValid(out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(Name))
            {
                errorMessage = "Название продукта не может быть пустым";
                return false;
            }

            if (PurchasePrice <= 0)
            {
                errorMessage = "Цена закупки должна быть больше 0";
                return false;
            }

            if (SellingPrice <= 0)
            {
                errorMessage = "Цена продажи должна быть больше 0";
                return false;
            }

            if (CategoryId <= 0)
            {
                errorMessage = "Необходимо выбрать категорию";
                return false;
            }

            if (SupplierId <= 0)
            {
                errorMessage = "Необходимо выбрать поставщика";
                return false;
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