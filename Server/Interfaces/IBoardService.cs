using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Interfaces
{
    public interface IBoardService
    {
        public IEnumerable<Board> GetBoards();
        public Board GetBoard(Guid boardId);
        public void AddBoard(string boardName, bool addBasicColumns);
        public void UpdateBoard();
        public void DeleteBoard();
    }
}
