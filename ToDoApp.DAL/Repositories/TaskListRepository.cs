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

        private void InitTable()
        {
            var taskList = new UserTaskList(new List<UserTask>())
            {
                Id = 1,
                Name = "taskList1",
            };
            taskList.UserTasks.Add(new UserTask() { Id = 2, Name = "fdbd", Description = "dwqnggfdgsjdwqj", Checked = false });
            taskList.UserTasks.Add(new UserTask() { Id = 3, Name = "dwqnj", Description = "dwqnjdwqj", Checked = false });
            taskList.UserTasks.Add(new UserTask() { Id = 4, Name = "gfdsgs", Description = "gfsd", Checked = true });

            var taskList2 = new UserTaskList(new List<UserTask>())
            {
                Id = 2,
                Name = "taskList2",
            };
            taskList2.UserTasks.Add(new UserTask() { Id = 5, Name = "brttr", Description = "dwqnggfdgsjdwqj", Checked = false });
            taskList2.UserTasks.Add(new UserTask() { Id = 6, Name = "bgfbg", Description = "dwqnjdwqj", Checked = true });
            taskList2.UserTasks.Add(new UserTask() { Id = 7, Name = "gffdsfesfdsgs", Description = "gfsd", Checked = true });

            var list = new List<UserTaskList> {taskList, taskList2};
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
