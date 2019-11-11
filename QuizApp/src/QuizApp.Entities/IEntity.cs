using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
