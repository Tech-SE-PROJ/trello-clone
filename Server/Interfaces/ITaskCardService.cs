﻿using trello_clone.Shared.Classes;

namespace trello_clone.Server.Interfaces
{
    public interface ITaskCardService
    {
        public List<TaskCard> GetTaskCards(Guid columnId);
        public TaskCard GetTaskCard(Guid cardId);
        public void AddTaskCard(Guid boardId, Guid columnId, string taskName, int cardIndex);
        public void UpdateTaskCard();
        public void UpdateColumnTaskCards(List<Column> columns);
        public void DeleteTaskCard();
    }
}
