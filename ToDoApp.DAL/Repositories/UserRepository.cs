using System.Collections.Generic;
using System.Linq;
using Realms;
using ToDoApp.DAL.Models;

namespace ToDoApp.DAL.Repositories
{
    public class UserRepository
    {
        private readonly string _databasePath;

        public UserRepository(string databaseName = "database")
        {
            _databasePath = databaseName;
        }

        public bool CreateUser(User model)
        {
            model = new User(new List<UserTaskList>())
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
            };

            var realm = Realm.GetInstance(_databasePath);

            var id = realm.All<User>()?.LastOrDefault()?.Id ?? 0;
            model.Id = ++id;

            realm.Write(() => { realm.Add(model); });
            return true;
        }

        public User GetUser(string email)
        {
            var realm = Realm.GetInstance(_databasePath);
            var user = realm.All<User>().FirstOrDefault(x => x.Email == email);
            return user ?? new User();
        }

        public bool UpdateUser(User model)
        {
            var realm = Realm.GetInstance(_databasePath);
            realm.Write(() => { realm.Add(model, true); });
            return true;
        }

        public bool DeleteUser(User model)
        {
            var realm = Realm.GetInstance(_databasePath);
            realm.Write(() => { realm.Remove(model); });
            return true;
        }
    }
}
