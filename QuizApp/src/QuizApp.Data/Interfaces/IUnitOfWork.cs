using System.Threading.Tasks;

namespace QuizApp.Data.Interfaces
{
    public interface IUnitOfWork
    {
        TIRepository GetRepository<TEntity, TIRepository>()
            where TEntity : class
            where TIRepository : class, IRepository<TEntity>;

        int Save();

        Task<int> SaveAsync();
    }
}
