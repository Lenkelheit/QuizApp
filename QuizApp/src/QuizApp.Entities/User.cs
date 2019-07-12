using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public ICollection<Test> Tests { get; set; } = new List<Test>();
    }
}
