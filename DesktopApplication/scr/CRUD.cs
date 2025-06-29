using System.Collections.Generic;
using System.Threading.Tasks;
using ManagementSystem;
using Supabase.Postgrest;
using Supabase.Postgrest.Models;
using Supabase;

namespace DesktopApplication.Services
{
    public class SupabaseRepository : ISupabaseClientService
    {
        private readonly Supabase.Client _supabase;

        public SupabaseRepository(Supabase.Client supabase)
        {
            _supabase = supabase;
        }
        
        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            var response = await _supabase
                .From<ProductModel>()
                .Get();
            return response.Models;
        }

        public async Task AddProduct(ProductModel product)
        {
            await _supabase
                .From<ProductModel>()
                .Insert(product);
        }

        public async Task UpdateProduct(ProductModel product)
        {
            await _supabase
                .From<ProductModel>()
                .Where(x => x.Id == product.Id)
                .Set(x => x.Name, product.Name)
                .Set(x => x.Description, product.Description)
                .Set(x => x.CategoryId, product.CategoryId)
                .Set(x => x.SupplierId, product.SupplierId)
                .Set(x => x.PurchasePrice, product.PurchasePrice)
                .Set(x => x.SellingPrice, product.SellingPrice)
                .Update();
        }

        public async Task DeleteProduct(int id)
        {
            await _supabase
                .From<ProductModel>()
                .Where(x => x.Id == id)
                .Delete();
        }
        
        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            var response = await _supabase
                .From<CategoryModel>()
                .Get();
            return response.Models;
        }

        public async Task AddCategory(CategoryModel category)
        {
            await _supabase
                .From<CategoryModel>()
                .Insert(category);
        }

        public async Task UpdateCategory(CategoryModel category)
        {
            await _supabase
                .From<CategoryModel>()
                .Where(x => x.Id == category.Id)
                .Set(x => x.Name, category.Name)
                .Update();
        }

        public async Task DeleteCategory(int id)
        {
            await _supabase
                .From<CategoryModel>()
                .Where(x => x.Id == id)
                .Delete();
        }
        
        public async Task<IEnumerable<SupplierModel>> GetAllSuppliers()
        {
            var response = await _supabase
                .From<SupplierModel>()
                .Get();
            return response.Models;
        }

        public async Task AddSupplier(SupplierModel supplier)
        {
            await _supabase
                .From<SupplierModel>()
                .Insert(supplier);
        }

        public async Task UpdateSupplier(SupplierModel supplier)
        {
            await _supabase
                .From<SupplierModel>()
                .Where(x => x.Id == supplier.Id)
                .Set(x => x.Name, supplier.Name)
                .Set(x => x.ContactPerson, supplier.ContactPerson)
                .Set(x => x.Phone, supplier.Phone)
                .Set(x => x.Email, supplier.Email)
                .Update();
        }

        public async Task DeleteSupplier(int id)
        {
            await _supabase
                .From<SupplierModel>()
                .Where(x => x.Id == id)
                .Delete();
        }
        
        public async Task<IEnumerable<InventoryModel>> GetAllInventory()
        {
            var response = await _supabase
                .From<InventoryModel>()
                .Get();
            return response.Models;
        }

        public async Task UpdateInventory(InventoryModel inventory)
        {
            await _supabase
                .From<InventoryModel>()
                .Where(x => x.Id == inventory.Id)
                .Set(x => x.ProductId, inventory.ProductId)
                .Set(x => x.Quantity, inventory.Quantity)
                .Set(x => x.MinStock, inventory.MinStock)
                .Update();
        }
    
    }
}