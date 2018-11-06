using Realms;

namespace ToDoApp.Common.Models
{
    public class UserTask : RealmObject
    {
        [PrimaryKey] public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}