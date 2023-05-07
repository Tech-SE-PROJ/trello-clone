namespace trello_clone.Shared.Classes
{
    public class Board
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Column>? Columns { get; set; }

    }
}
