using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Realms;
using ToDoApp.Common.Models;

namespace ToDoApp.Common.Repositories
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
            if (user == null) return null;

            var taskList = user.UserTaskLists;

            return taskList.ToList();
        }

        public bool CreateTaskList(UserTaskList model)
        {
            var realm = Realm.GetInstance(_databasePath);

            var user = realm.All<User>().FirstOrDefault(x => x.Id == _userId);
            if (user == null) return false;

            var id = realm.All<UserTaskList>()?.LastOrDefault()?.Id ?? 0;
            model.Id = ++id;
            
            realm.Write(() => { user.UserTaskLists.Add(model); });
            return true;
        }

        public UserTaskList GetTaskList(string name)
        {
            var realm = Realm.GetInstance(_databasePath);

            var user = realm.All<User>().FirstOrDefault(x => x.Id == _userId);
            if (user == null) return null;

            return realm.All<UserTaskList>().FirstOrDefault(x => x.Name == name);
        }

        public bool UpdateUserTask(UserTaskList model)
        {
            var realm = Realm.GetInstance(_databasePath);
            realm.Write(() => { realm.Add(model, true); });
            return true;
        }

        public bool DeleteUserTask(UserTaskList model)
        {
            var realm = Realm.GetInstance(_databasePath);
            var user = realm.All<User>().FirstOrDefault(x => x.Id == _userId);
            if (user == null) return false;


            realm.Write(() => { user.UserTaskLists.Remove(model); });
            return true;
        }
    }
}
