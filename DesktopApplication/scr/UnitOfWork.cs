using System;
using System.Threading.Tasks;
using Supabase;

public class UnitOfWork : IDisposable
{
    private readonly Client _client;
    private bool _disposed;

    public UnitOfWork(Client client)
    {
        _client = client;
    }

    public async Task ExecuteTransactionAsync(Func<Task> operations)
    {
        try
        {

            await operations.Invoke();
        }
        catch (Exception ex)
        {
            throw new Exception("Transaction failed", ex);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
        }
        _disposed = true;
    }
}