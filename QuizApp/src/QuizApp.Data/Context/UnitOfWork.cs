using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using QuizApp.Data.Interfaces;
using QuizApp.Data.Repositories;
using QuizApp.Entities;

namespace QuizApp.Data.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> interfaceRepositoriesFactory = new Dictionary<Type, object>();


        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;

            RegisterBasicRepositories();
        }


        public TIRepository GetRepository<TEntity, TIRepository>()
            where TEntity : class
            where TIRepository : class, IRepository<TEntity>
        {
            Type key = typeof(TIRepository);

            return interfaceRepositoriesFactory.ContainsKey(key) ? (TIRepository)interfaceRepositoriesFactory[key] : null;
        }

        public void RegisterRepository<TEntity, TIRepository, TRepository>()
            where TEntity : class
            where TIRepository : IRepository<TEntity>
            where TRepository : IRepository<TEntity>
        {
            Type key = typeof(TIRepository);

            if (!interfaceRepositoriesFactory.ContainsKey(key))
            {
                interfaceRepositoriesFactory.Add(key, Activator.CreateInstance(typeof(TRepository), dbContext));
            }
        }

        public int Save()
        {
            return dbContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        private void RegisterBasicRepositories()
        {
            RegisterRepository<User, IUserRepository, UserRepository>();
            RegisterRepository<Url, IUrlRepository, UrlRepository>();
            RegisterRepository<TestResult, ITestResultRepository, TestResultRepository>();
            RegisterRepository<Test, ITestRepository, TestRepository>();
            RegisterRepository<TestQuestion, ITestQuestionRepository, TestQuestionRepository>();
            RegisterRepository<TestQuestionOption, ITestQuestionOptionRepository, TestQuestionOptionRepository>();
            RegisterRepository<ResultAnswer, IResultAnswerRepository, ResultAnswerRepository>();
            RegisterRepository<ResultAnswerOption, IResultAnswerOptionRepository, ResultAnswerOptionRepository>();
            RegisterRepository<TestEvent, ITestEventRepository, TestEventRepository>();
        }
    }
}
