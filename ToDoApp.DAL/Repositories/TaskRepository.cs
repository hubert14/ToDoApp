﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Realms;
using ToDoApp.DAL.Models;

namespace ToDoApp.DAL.Repositories
{
    public class TaskRepository
    {
        private readonly string _databasePath;


        public TaskRepository(string databaseName = "database")
        {
            _databasePath = databaseName;
        }

        public bool CreateTask(UserTaskList list, UserTask task)
        {
            var realm = Realm.GetInstance(_databasePath);

            var item = realm.All<UserTaskList>().FirstOrDefault(x => x.Id == list.Id);
            if (item == null) return false;
            task.RefList = item;

            var id = realm.All<UserTask>().LastOrDefault()?.Id ?? 0;
            task.Id = ++id;

            realm.Write(() =>
            {
                realm.Add(task);
                item.UserTasks.Add(task);
            });

            return true;
        }

        public bool UpdateTask(UserTask item)
        {
            var realm = Realm.GetInstance(_databasePath);
            var task = realm.All<UserTask>().FirstOrDefault(x => x.Id == item.Id);
            if (task == null) return false;

            var refTask = task.RefList.UserTasks.FirstOrDefault(x => x.Id == item.Id);
            realm.Write(() =>
            {
                task.Name = item.Name;
                task.Description = item.Description;
                task.Checked = item.Checked;

                refTask.Name = item.Name;
                refTask.Description = item.Description;
                refTask.Checked = item.Checked;
            });

            return true;
        }

        public bool DeleteTask(UserTask task)
        {
            var realm = Realm.GetInstance(_databasePath);
            var item = realm.All<UserTask>().FirstOrDefault(x => x.Id == task.Id);
            if (item == null) return false;

            realm.Write(() =>
            {
                realm.Remove(item);
            });
            return true;
        }

        public UserTask GetTask(string name)
        {
            var realm = Realm.GetInstance(_databasePath);
            var task = realm.All<UserTask>().FirstOrDefault(x => x.Name == name);

            return task ?? new UserTask();
        }
    }
}
