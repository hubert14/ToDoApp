namespace ToDoApp.Common.Models
{
    public class UserTaskModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Checked { get; set; }
    }
}