using trello_clone.Shared.Classes;

namespace trello_clone.Server.Interfaces
{
    public interface IColumnService
    {
        public ICollection<Column> GetColumns(Guid boardId);
        public Column GetColumn(Guid columnId);
        public void AddColumn(Guid boardId, Guid columnId, string columnName, int columnIndex);
        public void AddBasicColumns(Guid boardId);
        public void UpdateColumn(Column column);
        public void UpdateColumns(List<Column> columns);
        public void DeleteColumn(Guid columnId);
    }
}
