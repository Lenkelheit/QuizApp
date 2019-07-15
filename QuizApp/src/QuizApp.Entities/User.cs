using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(length: EntitiesConstraints.USERNAME_MAX_LENGTH)]
        public string Username { get; set; }

        [Required]
        [MaxLength(length: EntitiesConstraints.EMAIL_MAX_LENGTH)]
        public string Email { get; set; }

        [Required]
        [MaxLength(length: EntitiesConstraints.PASSWORD_MAX_LENGTH)]
        public string Password { get; set; }


        public ICollection<Test> CreatedTests { get; set; } = new List<Test>();
    }
}
