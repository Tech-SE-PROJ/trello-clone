using System.ComponentModel.DataAnnotations;
namespace trello_clone.Shared.Classes
{
    [Serializable]
    public class User
    {
        [Key]
        public Guid? userId { get; set; }
        [Required]
        public string? email { get; set; }
        [Required]
        public string? userPassword { get; set; }
        [Required]
        public string? userName { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
        public int? TeamId { get; set; }

        public User()
        {
            
        }
    }
}
