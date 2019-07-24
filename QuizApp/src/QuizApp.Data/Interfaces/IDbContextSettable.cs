using Microsoft.EntityFrameworkCore;

namespace QuizApp.Data.Interfaces
{
    public interface IDbContextSettable
    {
        void SetDbContext(DbContext dbContext);
    }
}
