namespace ManagementSystem;

internal class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            var supa = new SupabaseRepository(
                
            await supa.InitDatabase();

            var result = await supa.SupabaseClient.From<ProductModel>().Get();
            var parsedResult = result.Models.ToList();
            foreach (var item in parsedResult)
            {
                Console.WriteLine($"ID: {item.id.ToString()}, Name: {item.Name}, Price: {item.SellingPrice}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}