using System.ComponentModel.DataAnnotations;
namespace trello_clone.Shared.Classes
{
    [Serializable]
    public class User
    {
        [Key]
        public Guid? UserId { get; set; }
        [Required]
        public string? UserEmail { get; set; }
        [Required]
        public string? UserPassword { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public int? TeamId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }

        public User()
        {
            
        }
    }
}
