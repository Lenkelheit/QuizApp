using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


        public ICollection<Test> CreatedTests { get; set; } = new List<Test>();
    }
}
