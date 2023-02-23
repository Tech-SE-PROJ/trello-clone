using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trello_clone.Shared.Classes
{
    public class Board
    {
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public List<Column>? Columns { get; set; }
        public List<BoardCard>? Cards { get; set; }

    }
}
