using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trello_clone.Shared.Classes
{
    [Serializable]
    public class Team
    {
        [Column("teamId")]
        [Key]
        public Guid TeamId { get; set; }
        [Column("teamName")]
        public string? TeamName { get; set; }
    }
}
