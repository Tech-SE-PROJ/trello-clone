using Microsoft.Data.SqlClient;
using trello_clone.Server.Interfaces;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Services
{
    public class ColumnService : IColumnService
    {
        private SqlConnection _con = LocalServerConnection.Connection;
        public ICollection<Column> GetColumns(Guid boardId)
        {
            var columns = new List<Column>();
            
            //string query = $"Select * from board_columns where boardId={boardId}";
            //SqlCommand cmd = new SqlCommand(query, _con);
            //_con.Open();
            //SqlDataReader dr = cmd.ExecuteReader();

            //while (dr.Read()) 
            //{
            //    var column = new Column();

            //    column.Id = Convert.ToInt32(dr["columnId"]);
            //    column.Name = dr["columnName"].ToString();
            //    column.Index = Convert.ToInt32(dr["columnIndex"]);
            //    column.BoardId = boardId;

            //    columns.Add(column);
            //}
            
            //_con.Close();
            return columns;
        }
        public Column GetColumn()
        {
            return new Column();
        }
        public void AddColumn(Guid boardId, string columnName, int columnIndex)
        {
            string query = $"insert into board_columns (columnName, columnIndex, boardId) values ({columnName},{columnIndex},{boardId})";
            SqlCommand cmd = new SqlCommand(query, _con);
            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
        }
        public void AddBasicColumns(Guid boardId)
        {
            var columnNames = new List<string>() { "Todo", "In-Prog", "Completed" };

            string query = $"insert into board_columns (columnName, columnIndex, boardId) values ({boardId},{columnNames[0]},{0}), ({boardId},{columnNames[1]},{1}), ({boardId},{columnNames[2]},{2})";

            _con.Open();
            using (SqlTransaction trans = _con.BeginTransaction())
            {
                using (SqlCommand cmd = _con.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandText = @"insert into board_columns (columnName, columnIndex, boardId) values (@columnName1, @columnIndex1, @boardId), (@columnName2, @columnIndex2, @boardId), (@columnName3, @columnIndex3, @boardId);";
                    cmd.Parameters.AddWithValue("@columnName1", "Todo");
                    cmd.Parameters.AddWithValue("@columnIndex1", 0);
                    
                    cmd.Parameters.AddWithValue("@columnName2", "In-Prog");
                    cmd.Parameters.AddWithValue("@columnIndex2", 1);
                    
                    cmd.Parameters.AddWithValue("@columnName3", "Completed");
                    cmd.Parameters.AddWithValue("@columnIndex3", 2);

                    cmd.Parameters.AddWithValue("boardId", boardId);
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
            }
            _con.Close();
        }
        public void UpdateColumn()
        {

        }
        public void DeleteColumn()
        {

        }
    }
}
