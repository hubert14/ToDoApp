using System.Collections.Generic;
using Realms;

namespace ToDoApp.DAL.Models
{
    public class User : RealmObject
    {
        public User()
        {
            
        }

        public User(IList<UserTaskList> list)
        {
            UserTaskLists = list;
        }

        [PrimaryKey] public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<UserTaskList> UserTaskLists { get; }
    }
}