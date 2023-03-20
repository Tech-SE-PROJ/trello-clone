using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trello_clone.Shared.Classes
{
    public class TaskCard
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Index { get; set; }
        public int AssignedUserId { get; set; }
        public int ColumnId { get; set; }
        public int BoardId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
		public string Status { get; set; }

		public TaskCard(string name, string status)
        {
            Name = name;
            Status = status;
        }
    }
}
