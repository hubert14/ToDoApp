using Realms;

namespace ToDoApp.DAL.Models
{
    public class UserTask : RealmObject
    {
        [PrimaryKey] public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Checked { get; set; }

        public UserTaskList RefList { get; set; }
    }
}