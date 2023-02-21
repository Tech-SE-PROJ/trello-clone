using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trello_clone.Shared.Classes
{
    public class BoardCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Index { get; set; }
        public int AssignedUserId { get; set; }
        public int ColumnId { get; set; }
        public int BoardId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }

    }
}
