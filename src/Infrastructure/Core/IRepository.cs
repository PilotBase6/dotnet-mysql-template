
namespace Infrastructure.Core
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T toCreate);
        Task<T> Update(T toUpdate);
        Task<T> Delete(T toDelete);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
    }
}
