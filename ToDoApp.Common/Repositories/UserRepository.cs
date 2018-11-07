using System.Linq;
using Realms;
using ToDoApp.Common.Models;

namespace ToDoApp.Common.Repositories
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
            var realm = Realm.GetInstance(_databasePath);

            var id = realm.All<User>()?.LastOrDefault()?.Id ?? 0;
            model.Id = ++id;

            realm.Write(() => { realm.Add(model); });
            return true;
        }
        
        public User Login(string email, string password)
        {
            var realm = Realm.GetInstance(_databasePath);
            return realm.All<User>().FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public User GetUser(string email)
        {
            var realm = Realm.GetInstance(_databasePath);
            return realm.All<User>().FirstOrDefault(x => x.Email == email);
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
