using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Common.Models;
using ToDoApp.Common.Utils;
using ToDoApp.DAL.Repositories;

namespace ToDoApp.Common.Services
{
    public class TaskListService
    {
        private TaskListRepository _repository;

        public TaskListService(int userId)
        {
            _repository = new TaskListRepository(userId);
        }

        public List<UserTaskListModel> GetAllTaskLists()
        {
            return Mapper.MapTaskLists(_repository.GetAllTaskLists());
        }

        public UserTaskListModel GetTaskList(string name)
        {
            return Mapper.MapTaskList(_repository.GetTaskList(name));
        }

        public bool CreateTaskList(UserTaskListModel list)
        {
            return _repository.CreateTaskList(Mapper.MapTaskList(list));
        }

        public bool DeleteTaskList(UserTaskListModel list)
        {
            return _repository.DeleteTaskList(Mapper.MapTaskList(list));
        }

        public bool UpdateTaskList(UserTaskListModel list)
        {
            return _repository.UpdateTaskList(Mapper.MapTaskList(list));
        }
    }
}

