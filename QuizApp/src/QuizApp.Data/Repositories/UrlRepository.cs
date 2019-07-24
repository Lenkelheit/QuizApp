﻿using QuizApp.Data.Context;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class UrlRepository : GenericRepository<Url>, IUrlRepository
    {
    }
}
