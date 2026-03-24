using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using User_Registration_System.Data.DBContexts;
using User_Registration_System.Shared.Interfaces;

namespace User_Registration_System.Shared.UnitOfWork
{
    public class UnitofWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dBContext;

        public IDbContextTransaction? _Transaction { get; set; }

        public UnitofWork(ApplicationDbContext DBContext)
        {
            dBContext = DBContext;
        }
        public async Task BeginTransactionAsync()
        {
            _Transaction = await dBContext.Database.BeginTransactionAsync();

        }

        public async Task CommitTransactionAsync()
        {
            if (_Transaction !=null)
            {
              await  _Transaction.CommitAsync();
               await _Transaction.DisposeAsync();
                _Transaction = null;
            }
        }

       

        public async Task RollbackTransactionAsync()
        {
            if (_Transaction != null)
            {
                await _Transaction.RollbackAsync();
                await _Transaction.DisposeAsync();
                _Transaction = null;

            }
        }

        public void Dispose()
        {
            _Transaction?.Dispose();
            dBContext.Dispose();
        }
    }
}
