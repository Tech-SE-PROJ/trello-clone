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

            _con.Open();
            SqlCommand cmd = _con.CreateCommand();
            cmd.CommandText = @"Select * from board_columns where boardId = @boardId order by columnIndex";
            cmd.Parameters.AddWithValue("@boardId", boardId);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var column = new Column();

                column.Id = (Guid) dr["columnId"];
                column.Name = (string) dr["columnName"];
                column.Index = (int) dr["columnIndex"];
                column.BoardId = boardId;

                columns.Add(column);
            }

            _con.Close();
            return columns;
        }
        public Column GetColumn()
        {
            return new Column();
        }
        public void AddColumn(Guid boardId, string columnName, int columnIndex)
        {
            string query = $"insert into board_columns (columnName, columnIndex, boardId) values (@columnName, @columnIndex, @boardId)";
            SqlCommand cmd = new SqlCommand(query, _con);
            cmd.Parameters.AddWithValue("@columnName", columnName);
            cmd.Parameters.AddWithValue("@columnIndex", columnIndex);
            cmd.Parameters.AddWithValue("@boardId", boardId);

            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
        }
        public void AddBasicColumns(Guid boardId)
        {
            var columnNames = new List<string>() { "Planning", "In-Work", "Completed" };

            string query = $"insert into board_columns (columnName, columnIndex, boardId) values ({boardId},{columnNames[0]},{0}), ({boardId},{columnNames[1]},{1}), ({boardId},{columnNames[2]},{2})";

            _con.Open();
            using (SqlTransaction trans = _con.BeginTransaction())
            {
                using (SqlCommand cmd = _con.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandText = @"insert into board_columns (columnId, columnName, columnIndex, boardId) values (@columnId1, @columnName1, @columnIndex1, @boardId), (@columnId2, @columnName2, @columnIndex2, @boardId), (@columnId3, @columnName3, @columnIndex3, @boardId);";
                    cmd.Parameters.AddWithValue("@columnId1", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@columnName1", "Planning");
                    cmd.Parameters.AddWithValue("@columnIndex1", 0);

                    cmd.Parameters.AddWithValue("@columnId2", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@columnName2", "In-Work");
                    cmd.Parameters.AddWithValue("@columnIndex2", 1);

                    cmd.Parameters.AddWithValue("@columnId3", Guid.NewGuid());
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
