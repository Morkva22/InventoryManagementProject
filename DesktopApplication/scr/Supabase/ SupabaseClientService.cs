using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ManagementSystem;
using Supabase;

namespace DesktopApplication.Services
{
    public class SupabaseClientService : ISupabaseClientService
    {
        private readonly Client _client;

        public SupabaseClientService(Client client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        
        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            var response = await _client.From<ProductModel>().Get();
            return response.Models;
        }

        public async Task AddProduct(ProductModel product)
        {
            try
            {
                var insertData = new ProductModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    SupplierId = product.SupplierId,
                    PurchasePrice = product.PurchasePrice,
                    SellingPrice = product.SellingPrice,
                    CreatedAt = product.CreatedAt
                };

                var response = await _client.From<ProductModel>()
                    .Insert(insertData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in deleting product: {ex.Message}", ex);
            }
        }

        public async Task UpdateProduct(ProductModel product)
        {
            var partialProduct = new ProductModel
            {
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId,
                PurchasePrice = product.PurchasePrice,
                SellingPrice = product.SellingPrice
            };

            await _client.From<ProductModel>()
                .Where(p => p.Id == product.Id)
                .Update(partialProduct);
        }

        public async Task DeleteProduct(int id)
        {
            await _client.From<ProductModel>()
                .Where(p => p.Id == id)
                .Delete();
        }
        
        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            var response = await _client.From<CategoryModel>().Get();
            return response.Models;
        }

        public async Task AddCategory(CategoryModel category)
        {
            var newCategory = new CategoryModel
            {
                Name = category.Name
            };
            await _client.From<CategoryModel>().Insert(newCategory);
        }

        public async Task UpdateCategory(CategoryModel category)
        {
            var partialCategory = new CategoryModel { Name = category.Name };
            
            await _client.From<CategoryModel>()
                .Where(c => c.Id == category.Id)
                .Update(partialCategory);
        }

        public async Task DeleteCategory(int id)
        {
            await _client.From<CategoryModel>()
                .Where(c => c.Id == id)
                .Delete();
        }
        
        public async Task<IEnumerable<SupplierModel>> GetAllSuppliers()
        {
            var response = await _client.From<SupplierModel>().Get();
            return response.Models;
        }

        public async Task AddSupplier(SupplierModel supplier)
        {
            var newSupplier = new SupplierModel
            {
                Name = supplier.Name,
                ContactPerson = supplier.ContactPerson,
                Phone = supplier.Phone,
                Email = supplier.Email
            };
            await _client.From<SupplierModel>().Insert(newSupplier);
        }

        public async Task UpdateSupplier(SupplierModel supplier)
        {
            var partialSupplier = new SupplierModel
            {
                Name = supplier.Name,
                ContactPerson = supplier.ContactPerson,
                Phone = supplier.Phone,
                Email = supplier.Email
            };

            await _client.From<SupplierModel>()
                .Where(s => s.Id == supplier.Id)
                .Update(partialSupplier);
        }

        public async Task DeleteSupplier(int id)
        {
            await _client.From<SupplierModel>()
                .Where(s => s.Id == id)
                .Delete();
        }
        public async Task<IEnumerable<InventoryModel>> GetAllInventory()
        {
            var response = await _client.From<InventoryModel>().Get();
            return response.Models;
        }

        public async Task UpdateInventory(InventoryModel inventory)
        {
            var partialInventory = new InventoryModel
            {
                ProductId = inventory.ProductId,
                Quantity = inventory.Quantity,
                MinStock = inventory.MinStock
            };

            await _client.From<InventoryModel>()
                .Where(i => i.Id == inventory.Id)
                .Update(partialInventory);
        }
    }
}