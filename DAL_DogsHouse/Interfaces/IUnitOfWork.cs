namespace DAL_DogsHouse.Interfaces
{
    public interface IUnitOfWork
    {
        IDogRepository DogRepository { get; }
        Task<int> SaveAsync();
    }
}
