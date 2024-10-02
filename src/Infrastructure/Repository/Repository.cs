using Infrastructure.Core;


namespace Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public abstract Task<T> Create(T toCreate);
        public abstract Task<T> Update(T toUpdate);
        public abstract Task<T> Delete(T toDelete);
        public abstract Task<T> GetById(Guid id);
        public abstract Task<IEnumerable<T>> GetAll();
    }
}