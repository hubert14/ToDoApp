using System.Collections.Generic;

namespace ToDoApp.Common.Models
{
    public class UserTaskListModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<UserTaskModel> UserTasks { get; set; }
    }
}