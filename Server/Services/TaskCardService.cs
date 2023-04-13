using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography.Xml;
using trello_clone.Client.Pages;
using trello_clone.Server.ExtensionMethods;
using trello_clone.Server.Interfaces;
using trello_clone.Shared.Classes;
using static MudBlazor.CategoryTypes;

namespace trello_clone.Server.Services
{
    public class TaskCardService : ITaskCardService
    {
        private SqlConnection _con = LocalServerConnection.Connection;
        public IEnumerable<TaskCard> GetTaskCards(Guid columnId)
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
        public void AddTaskCard(Guid columnId, string cardName, int cardIndex)
        {
            string query = $"insert into board_cards(cardId, itemName, cardIndex, columnId, dateCreated, lastModifiedDate) values (@cardId, @itemName, @cardIndex, @columnId, @dateCreated, @lastModifiedDate)";
            SqlCommand cmd = new SqlCommand(query, _con);
            cmd.Parameters.AddWithValue("@cardId", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@itemName", cardName);
            cmd.Parameters.AddWithValue("@cardIndex", cardIndex);
            cmd.Parameters.AddWithValue("@columnId", columnId);
            cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@lastModifiedDate", DateTime.Now);

            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
        }
        	
        public void UpdateTaskCard()
        {

        }
        public void DeleteTaskCard()
        {

        }
    }
}
