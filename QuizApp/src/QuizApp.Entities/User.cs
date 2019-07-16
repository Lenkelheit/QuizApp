using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    [Table(nameof(User))]
    public class User
    {
        public const int UsernameMaxLength = 128;

        public const int EmailMaxLength = 128;

        public const int PasswordMaxLength = 256;
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(length: UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(length: EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(length: PasswordMaxLength)]
        public string Password { get; set; }


        public ICollection<Test> CreatedTests { get; set; } = new List<Test>();
    }
}
