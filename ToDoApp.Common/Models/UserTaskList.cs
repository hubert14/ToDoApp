using Realms;

namespace ToDoApp.Common.Models
{
    public class UserTaskList : RealmObject
    {
        [PrimaryKey] public int Id { get; set; }

        public string Name { get; set; }
    }
}