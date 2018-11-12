using System.Collections.Generic;

namespace ToDoApp.Common.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public bool PushNotifications { get; set; }

        public List<UserTaskListModel> UserTaskLists { get; set; }
    }
}