namespace Classes
{
    public class TaskModel
    {
        public int Id { get; set; }

        public TaskStatuses Status { get; set; }

        public string? Description { get; set; }

        public DateTime LastUpdated { get; set; }

    }

    public enum TaskStatuses
    {
        Todo,
        In_Progress,
        Completed
    }
}
