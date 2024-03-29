﻿namespace trello_clone.Shared.Classes
{
    public class Column
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public Guid BoardId { get; set; }
        public List<TaskCard>? TaskCards { get; set; }
    }
}
