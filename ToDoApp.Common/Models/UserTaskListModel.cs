using System.Collections.Generic;
using System.Linq;

namespace ToDoApp.Common.Models
{
    public class UserTaskListModel
    {
        public bool IsCompleted => UserTasks.FirstOrDefault(x => !x.Checked) == null;

        public int Id { get; set; }

        public string Name { get; set; }

        public List<UserTaskModel> UserTasks { get; set; }
    }
}