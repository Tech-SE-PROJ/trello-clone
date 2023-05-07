using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trello_clone.Shared.Classes
{
    [Serializable]
    public class User
    {
        [Column("userId")]
        [Key]
        public Guid? UserId { get; set; }
        [Column("userEmail")]
        [Required]
        public string? UserEmail { get; set; }
        [Column("userName")]
        [Required]
        public string? UserName { get; set; }
        [Column("userPassword")]
        [Required]
        public string? UserPassword { get; set; }
        [Column("firstName")]
        public string? FirstName { get; set; }
        [Column("lastName")]
        public string? LastName { get; set; }
        [Column("jobTitle")]
        public string? JobTitle { get; set; }
    }
}
