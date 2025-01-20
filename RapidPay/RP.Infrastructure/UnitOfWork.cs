using Microsoft.EntityFrameworkCore.Storage;

namespace RP.Infrastructure
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        void Dispose();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync() => 
            _transaction = await _context.Database.BeginTransactionAsync();
        public async Task CommitAsync() => await _transaction.CommitAsync();
        public async Task RollbackAsync() => await _transaction.RollbackAsync();

        public void Dispose() { 
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
