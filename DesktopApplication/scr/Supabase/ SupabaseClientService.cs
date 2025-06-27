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

        #region Products
        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            var response = await _client.From<ProductModel>().Get();
            return response.Models;
        }

        public async Task AddProduct(ProductModel product)
        {
            await _client.From<ProductModel>().Insert(product);
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
        #endregion

        #region Categories
        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            var response = await _client.From<CategoryModel>().Get();
            return response.Models;
        }

        public async Task AddCategory(CategoryModel category)
        {
            await _client.From<CategoryModel>().Insert(category);
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
        #endregion

        #region Suppliers
        public async Task<IEnumerable<SupplierModel>> GetAllSuppliers()
        {
            var response = await _client.From<SupplierModel>().Get();
            return response.Models;
        }

        public async Task AddSupplier(SupplierModel supplier)
        {
            await _client.From<SupplierModel>().Insert(supplier);
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
        #endregion

        #region Inventory
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
        #endregion
    }
}