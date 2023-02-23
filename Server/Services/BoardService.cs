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
        private IColumnService _columnService = new ColumnService();
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

                board.Id = (Guid) dr["boardId"];
                board.Name = (string) dr["boardName"];

                boards.Add(board);
            }
            _con.Close();

            boards.ForEach(board => board.Columns = (List<Column>) _columnService.GetColumns(board.Id));

            return boards;
        }

        public Board GetBoard(Guid boardId)
        {
            string query = $"Select * from boards where boardId={boardId}";
            return new Board();
        }

        public void AddBoard(string boardName)
        {
            var boardId = Guid.NewGuid();

            _con.Open();
            using (SqlTransaction trans = _con.BeginTransaction())
            {
                using (SqlCommand cmd = _con.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandText = @"insert into boards (boardId, boardName) values (@boardId, @boardName);";
                    cmd.Parameters.AddWithValue("@boardId", boardId);
                    cmd.Parameters.AddWithValue("@boardName", boardName);
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
            }
            _con.Close();

            _columnService.AddBasicColumns(boardId);
        }

        public void UpdateBoard()
        {

        }

        public void DeleteBoard()
        {

        }
    }
}
