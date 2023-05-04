using Microsoft.Data.SqlClient;
using trello_clone.Server.ExtensionMethods;
using trello_clone.Server.Interfaces;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Services
{
    public class TaskCardService : ITaskCardService
    {
        private SqlConnection _con = LocalServerConnection.Connection;
        public List<TaskCard> GetTaskCards(Guid columnId)
        {
            var taskCards = new List<TaskCard>();

            _con.Open();
            SqlCommand cmd = _con.CreateCommand();
            cmd.CommandText = @"Select * from board_cards where columnId = @columnId order by cardIndex";
            cmd.Parameters.AddWithValue("@columnId", columnId);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var taskCard = new TaskCard();

                taskCard.Id = dr.GetSafeGuid("cardId");
                taskCard.Name = dr.GetSafeString("itemName");
                taskCard.Index = dr.GetSafeInt("cardIndex");
                taskCard.ColumnId = columnId;
                taskCard.DateCreated = dr.GetSafeDateTime("dateCreated");
                taskCard.LastModified = dr.GetSafeDateTime("lastModifiedDate");
                taskCard.Description = dr.GetSafeString("itemDescription");
                taskCard.AssignedUserId = dr.GetSafeGuid("assignedUserId");

                taskCards.Add(taskCard);
            }

            _con.Close();

            return taskCards;
        }
        public TaskCard GetTaskCard(Guid cardId)
        {
            return new TaskCard();
        }
        public void AddTaskCard(Guid boardId, Guid columnId, string cardName, int cardIndex)
        {
            string query = $"insert into board_cards(cardId, itemName, cardIndex, columnId, dateCreated, lastModifiedDate, boardId) values (@cardId, @itemName, @cardIndex, @columnId, @dateCreated, @lastModifiedDate, @boardId)";
            SqlCommand cmd = new SqlCommand(query, _con);
            cmd.Parameters.AddWithValue("@cardId", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@itemName", cardName);
            cmd.Parameters.AddWithValue("@cardIndex", cardIndex);
            cmd.Parameters.AddWithValue("@columnId", columnId);
            cmd.Parameters.AddWithValue("@boardId", boardId);
            cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@lastModifiedDate", DateTime.Now);

            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
        }
        	
        public void UpdateTaskCard()
        {

        }

        public void UpdateColumnTaskCards(List<Column> columns)
        {
            _con.Open();
            using (SqlTransaction trans = _con.BeginTransaction())
            {
                foreach (Column column in columns)
                {
                    foreach (TaskCard taskCard in column.TaskCards)
                    {
                        int newIndex = column.TaskCards.IndexOf(taskCard);
                        
                        using (SqlCommand cmd = _con.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = @"update board_cards set itemName=@itemName, lastModifiedDate=@lastModifiedDate, cardIndex=@cardIndex, columnId=@columnId where cardId=@cardId";
                            
                            cmd.Parameters.AddWithValue("@itemName", taskCard.Name);
                            cmd.Parameters.AddWithValue("@lastModifiedDate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@cardIndex", newIndex);
                            cmd.Parameters.AddWithValue("@columnId", column.Id);
                            cmd.Parameters.AddWithValue("@cardId", taskCard.Id);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                trans.Commit();
            }
            _con.Close();
        }

        public void DeleteTaskCard()
        {

        }
    }
}
