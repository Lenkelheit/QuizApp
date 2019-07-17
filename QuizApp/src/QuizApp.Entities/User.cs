using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    [Table(nameof(User))]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(length: 128)]
        public string Username { get; set; }

        [Required]
        [MaxLength(length: 128)]
        public string Email { get; set; }

        [Required]
        [MaxLength(length: 256)]
        public string Password { get; set; }


        public ICollection<Test> CreatedTests { get; set; } = new List<Test>();
    }
}
