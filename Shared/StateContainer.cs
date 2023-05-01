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

        private TaskCard? _currentCard;
        public TaskCard? CurrentCard
        {
            get => _currentCard;
            set => _currentCard = value;
        }

        private User? _loggedInUser;
        public User? LoggedInUser
        {
            get => _loggedInUser;
            set => _loggedInUser = value;
        }

        //private TaskCard[]? _currentTaskCards;
        //public TaskCard[]? CurrentTaskCards
        //{
        //    get => _currentTaskCards;
        //    set => _currentTaskCards = value;
        //}
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public bool isCompleted;

    }
}
