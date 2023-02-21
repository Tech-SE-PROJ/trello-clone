using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trello_clone.Shared.Classes;
using trello_clone.Server.Interfaces;
using trello_clone.Server;
using Microsoft.Data.SqlClient;

namespace trello_clone.Server.Services
{
    public class BoardService : IBoardService
    {
        private SqlConnection _con = new SqlConnection(LocalServerConnection.ConnectionString);
        private ColumnService _columnService = new ColumnService();
        public IEnumerable<Board> GetBoards()
        {
            var boards = new List<Board>();

            string query = "Select * from boards";
            SqlCommand cmd = new SqlCommand(query, _con);
            _con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Board board = new Board();

                board.ID = (Guid) dr["boardId"];
                board.Name = (string) dr["boardName"];
                board.Columns = (List<Column>) _columnService.GetColumns(board.ID);

                boards.Add(board);
            }
            _con.Close();

            return boards;
        }

        public Board GetBoard(Guid boardId)
        {
            string query = $"Select * from boards where boardId={boardId}";
            return new Board();
        }

        public void AddBoard(string boardName = "New Board", bool addBasicColumns = true)
        {
            var boardId = new Guid();
            string queary = $"insert into boards (boardId, boardName) values ({boardId},{boardName})";
            SqlCommand cmd = new SqlCommand(queary, _con);
            
            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();

            if (addBasicColumns) _columnService.AddBasicColumns(boardId);
        }

        public void UpdateBoard()
        {

        }

        public void DeleteBoard()
        {

        }
    }
}
