using System.Collections.Generic;
using Realms;

namespace ToDoApp.DAL.Models
{
    public class UserTaskList : RealmObject
    {
        public UserTaskList()
        {}

        public UserTaskList(IList<UserTask> list)
        {
            UserTasks = list;
        }

        [PrimaryKey] public int Id { get; set; }

        public string Name { get; set; }

        public IList<UserTask> UserTasks { get; }

        public User RefUser { get; set; }
    }
}