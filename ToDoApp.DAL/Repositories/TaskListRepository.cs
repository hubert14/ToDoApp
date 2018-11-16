using System.Collections.Generic;
using System.Linq;
using Realms;
using ToDoApp.DAL.Models;

namespace ToDoApp.DAL.Repositories
{
    public class TaskListRepository
    {
        private readonly int _userId;
        private readonly string _databasePath;

        public TaskListRepository(int userId, string databaseName = "database")
        {
            _userId = userId;
            _databasePath = databaseName;
        }

        public List<UserTaskList> GetAllTaskLists()
        {
            var realm = Realm.GetInstance(_databasePath);

            var user = realm.All<User>().FirstOrDefault(x => x.Id == _userId);

            var taskList = user?.UserTaskLists.ToList();
            return taskList ?? new List<UserTaskList>();
        }

        public bool CreateTaskList(UserTaskList model)
        {
            var realm = Realm.GetInstance(_databasePath);

            var user = realm.All<User>().FirstOrDefault(x => x.Id == _userId);
            if (user == null) return false;
            model.RefUser = user;

            var id = realm.All<UserTaskList>()?.LastOrDefault()?.Id ?? 0;
            model.Id = ++id;
            while (realm.All<UserTaskList>().FirstOrDefault(x => x.Id == id) != null) id++;
            realm.Write(() =>
            {
                realm.Add(model);
                user.UserTaskLists.Add(model);
            });
            return true;
        }

        public UserTaskList GetTaskList(string name)
        {
            var realm = Realm.GetInstance(_databasePath);

            var user = realm.All<User>().FirstOrDefault(x => x.Id == _userId);
            var taskList = user?.UserTaskLists.FirstOrDefault(x => x.Name == name);
            return taskList ?? new UserTaskList(new List<UserTask>());
        }

        public bool UpdateTaskList(UserTaskList model)
        {
            var realm = Realm.GetInstance(_databasePath);
            realm.Write(() => { realm.Add(model, true); });
            return true;
        }

        public bool DeleteTaskList(UserTaskList model)
        {
            var realm = Realm.GetInstance(_databasePath);
            var user = realm.All<User>().FirstOrDefault(x => x.Id == _userId);
            var taskList = realm.All<UserTaskList>().FirstOrDefault(x => x.Id == model.Id);
            if (user == null || taskList == null) return false;
            
            realm.Write(() =>
            {
                user.UserTaskLists.Remove(taskList);
                realm.Remove(taskList);
            });
            return true;
        }
    }
}
