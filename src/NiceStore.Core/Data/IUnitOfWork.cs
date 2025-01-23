namespace NiceStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }   
}
