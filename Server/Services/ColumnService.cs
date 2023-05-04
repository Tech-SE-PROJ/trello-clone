using Microsoft.Data.SqlClient;
using trello_clone.Client.Pages;
using trello_clone.Server.ExtensionMethods;
using trello_clone.Server.Interfaces;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Services
{
    public class ColumnService : IColumnService
    {
        private SqlConnection _con = LocalServerConnection.Connection;
        private ITaskCardService _taskCardService = new TaskCardService();
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

            columns.ForEach(column => column.TaskCards = (List<TaskCard>)_taskCardService.GetTaskCards(column.Id));

            return columns;
        }
        public Column GetColumn(Guid columnId)
        {
            var column = new Column();

            _con.Open();
            SqlCommand cmd = _con.CreateCommand();
            cmd.CommandText = @"Select * from board_columns where columnId = @columnId order by columnIndex";
            cmd.Parameters.AddWithValue("@columnId", columnId);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                column.Id = columnId;
                column.Name = dr.GetSafeString("columnName");
                column.Index = dr.GetSafeInt("columnIndex").Value;
                column.BoardId = dr.GetSafeGuid("columnId").Value;
            }

            _con.Close();

            column.TaskCards = _taskCardService.GetTaskCards(columnId);

            return column;
        }
        public void AddColumn(Guid boardId, Guid columnId, string columnName, int columnIndex)
        {
            string query = $"insert into board_columns (columnId, columnName, columnIndex, boardId) values (@columnId, @columnName, @columnIndex, @boardId)";
            SqlCommand cmd = new SqlCommand(query, _con);
            cmd.Parameters.AddWithValue("@columnName", columnName);
            cmd.Parameters.AddWithValue("@columnIndex", columnIndex);
            cmd.Parameters.AddWithValue("@boardId", boardId);
            cmd.Parameters.AddWithValue("columnId", columnId);

            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
        }
        public void AddBasicColumns(Guid boardId)
        {
            _con.Open();
            using (SqlTransaction trans = _con.BeginTransaction())
            {
                using (SqlCommand cmd = _con.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandText = @"insert into board_columns (columnId, columnName, columnIndex, boardId) values (@columnId1, @columnName1, @columnIndex1, @boardId), (@columnId2, @columnName2, @columnIndex2, @boardId), (@columnId3, @columnName3, @columnIndex3, @boardId);";
                    cmd.Parameters.AddWithValue("@columnId1", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@columnName1", "Todo");
                    cmd.Parameters.AddWithValue("@columnIndex1", 0);

                    cmd.Parameters.AddWithValue("@columnId2", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@columnName2", "In-Prog");
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
        public void UpdateColumn(Column column)
        {

        }

        public void UpdateColumns(List<Column> columns)
        {
            _con.Open();
            using (SqlTransaction trans = _con.BeginTransaction())
            {
                foreach (Column column in columns)
                {
                    using (SqlCommand cmd = _con.CreateCommand())
                    {
                        cmd.Transaction = trans;
                        cmd.CommandText = @"update board_columns set columnName=@columnName, columnIndex=@columnIndex where columnId=@columnId";
                        cmd.Parameters.AddWithValue("@columnId", column.Id);
                        cmd.Parameters.AddWithValue("@columnName", column.Name);
                        cmd.Parameters.AddWithValue("@columnIndex", column.Index);

                        cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
            }
            _con.Close();

            _taskCardService.UpdateColumnTaskCards(columns);
        }

        public void DeleteColumn(Guid columnId)
        {
            _con.Open();
            using (SqlTransaction trans = _con.BeginTransaction())
            {
                using (SqlCommand cmd = _con.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandText = @"delete from board_columns where columnId=@columnId";
                    cmd.Parameters.AddWithValue("@columnId", columnId);

                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            _con.Close();
        }
    }
}
