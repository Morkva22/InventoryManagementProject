using System.Collections.Generic;
using System.Threading.Tasks;
using ManagementSystem;

namespace DesktopApplication.Services
{
    public interface ISupaRepository
    {
        // Products
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task AddProduct(ProductModel product);
        Task UpdateProduct(ProductModel product);
        Task DeleteProduct(int id);
        
        // Categories
        Task<IEnumerable<CategoryModel>> GetAllCategories();
        Task AddCategory(CategoryModel category);
        Task UpdateCategory(CategoryModel category);
        Task DeleteCategory(int id);
        
        // Suppliers
        Task<IEnumerable<SupplierModel>> GetAllSuppliers();
        Task AddSupplier(SupplierModel supplier);
        Task UpdateSupplier(SupplierModel supplier);
        Task DeleteSupplier(int id);
        
        // Inventory
        Task<IEnumerable<InventoryModel>> GetAllInventory();
        Task UpdateInventory(InventoryModel inventory);
    }
}