using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trello_clone.Shared.Classes
{
    public class TaskCard
    {
        [Column("cardId")]
        public Guid? Id { get; set; }
        [Column("itemName")]
        public string? Name { get; set; }
        [Column("itemDescription")]
        public string? Description { get; set; }
        [Column("cardIndex")]
        public int? Index { get; set; }
        [Column("assignedUserId")]
        public Guid? AssignedUserId { get; set; }
        [Column("columnId")]
        public Guid? ColumnId { get; set; }
        [Column("boardId")]
        public Guid? BoardId { get; set; }
        [Column("dateCreated")]
        public DateTime? DateCreated { get; set; }
        [Column("lastModifiedDate")]
        public DateTime? LastModified { get; set; }
        [Column("dateEnd")]
        public DateTime? EndDate { get; set; }
        [NotMapped]
        public string? Status { get; set; }
        [NotMapped]
        public string? assignedUserName { get; set; }

        public TaskCard() { }
		public TaskCard(string name, string status)
        {
            Name = name;
            Status = status;
        }
        public TaskCard(Guid? assignedUserId, Guid cardId)
        {
            AssignedUserId = assignedUserId;
            Id = cardId;
        }
    }
}
