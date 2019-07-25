using Microsoft.EntityFrameworkCore;

using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class UrlRepository : GenericRepository<Url>, IUrlRepository
    {
        public UrlRepository(DbContext dbContext) : base(dbContext) { }
    }
}
