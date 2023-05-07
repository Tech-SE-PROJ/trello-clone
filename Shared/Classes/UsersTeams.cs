using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace trello_clone.Shared.Classes
{
    [Table("userteams")]
    [PrimaryKey(nameof(UserId), nameof(TeamId))]
    public class UsersTeams
    {
        [Column("userId")]
        public Guid? UserId { get; set; }
        [Column("teamId")]
        public Guid TeamId { get; set; }
    }
}