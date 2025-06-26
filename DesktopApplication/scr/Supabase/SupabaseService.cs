namespace ManagementSystem;

using Supabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface ISupabaseRepository
{
    Task InitDatabase();
    Task<List<ProductModel>> GetAllProducts();
    Task<ProductModel> GetProductById(int id);
    Task<bool> AddProduct(ProductModel product);
    Task<bool> UpdateProduct(ProductModel product);
    Task<bool> DeleteProduct(int id);
}

public class SupabaseRepository : ISupabaseRepository
{
    public Client SupabaseClient { get; private set; } = null!;
    private string _url { get; set; }
    private string _key { get; set; }
    private string _defaultSchema = "public";

    public SupabaseRepository(string url, string key)
    {
        _url = url;
        _key = key;
    }

    public async Task InitDatabase()
    {
        try
        {
            SupabaseOptions options = new SupabaseOptions { AutoConnectRealtime = true };
            SupabaseClient = new Client(_url, _key, options);
        }
        catch (Exception e)
        {
            throw new Exception($"Error connecting to Supabase: {e.Message}", e);
        }
    }

    public async Task<List<ProductModel>> GetAllProducts()
    {
        try
        {
            var result = await SupabaseClient.From<ProductModel>().Get();
            return result.Models.ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Error getting products: {e.Message}", e);
        }
    }

    public async Task<ProductModel> GetProductById(int id)
    {
        try
        {
            var result = await SupabaseClient.From<ProductModel>().Where(p => p.id == id).Get();
            return result.Models.FirstOrDefault();
        }
        catch (Exception e)
        {
            throw new Exception($"Error getting product by ID: {e.Message}", e);
        }
    }

    public async Task<bool> AddProduct(ProductModel product)
    {
        try
        {
            var result = await SupabaseClient.From<ProductModel>().Insert(product);
            return result.Models.Any();
        }
        catch (Exception e)
        {
            throw new Exception($"Error adding product: {e.Message}", e);
        }
    }

    public async Task<bool> UpdateProduct(ProductModel product)
    {
        try
        {
            var result = await SupabaseClient.From<ProductModel>().Update(product);
            return result.Models.Any();
        }
        catch (Exception e)
        {
            throw new Exception($"Error updating product: {e.Message}", e);
        }
    }

    public async Task<bool> DeleteProduct(int id)
    {
        try
        {
            await SupabaseClient.From<ProductModel>().Where(p => p.id == id).Delete();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Error deleting product: {e.Message}", e);
        }
    }
}