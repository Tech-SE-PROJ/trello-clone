using trello_clone.Shared.Classes;

namespace trello_clone.Server.Interfaces
{
    public interface IColumnService
    {
        public ICollection<Column> GetColumns(Guid boardId);
        public Column GetColumn();
        public void AddColumn(Guid boardId, string columnName, int columnIndex);
        public void AddBasicColumns(Guid boardId);
        public void UpdateColumn();
        public void DeleteColumn();
    }
}
