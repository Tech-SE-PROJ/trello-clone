using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trello_clone.Shared.Classes;

namespace trello_clone.Shared
{
    public class StateContainer 
    {
        private List<Board>? boards;
        public List<Board>? Boards
        {
            get { return boards; }
            
            set { boards = value; }
        }

        private Board? selectedBoard;
        public Board? SelectedBoard
        {
            get { return selectedBoard; }
            
            set { selectedBoard = value; }

        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
