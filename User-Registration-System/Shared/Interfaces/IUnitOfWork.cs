namespace User_Registration_System.Shared.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
