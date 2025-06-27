    using Supabase.Postgrest.Models;
    using Supabase.Postgrest.Attributes;

    namespace ManagementSystem
    {
        [Table("products")]
        public class ProductModel : BaseModel
        {
            [PrimaryKey("productID", true)]
            public int Id { get; set; }

            [Column("Name")]
            public string Name { get; set; }

            [Column("Description")]
            public string Description { get; set; }

            [Column("CategoryID")]
            public int CategoryId { get; set; }

            [Column("SupplierID")]
            public int SupplierId { get; set; }

            [Column("PurchasePrice")]
            public decimal PurchasePrice { get; set; }

            [Column("SellingPrice")]
            public decimal SellingPrice { get; set; }

            [Column("CreatedDate")]
            public DateTime CreatedAt { get; set; }

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