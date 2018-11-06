using Realms;

namespace ToDoApp.Common.Models
{
    public class User : RealmObject
    {
        [PrimaryKey] public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}