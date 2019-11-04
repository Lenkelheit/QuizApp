using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class TestEventRepository : GenericRepository<TestEvent>, ITestEventRepository
    {
        public TestEventRepository(DbContext dbContext) : base(dbContext) { }


        public void Delete(IEnumerable<TestEvent> testEvents)
        {
            if (testEvents == null) throw new ArgumentNullException(nameof(testEvents));

            DbSet.RemoveRange(testEvents);
        }
    }
}
