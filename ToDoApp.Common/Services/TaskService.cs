using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Common.Models;
using ToDoApp.Common.Utils;
using ToDoApp.DAL.Repositories;

namespace ToDoApp.Common.Services
{
    public class TaskService
    {
        private TaskRepository _repository;

        public TaskService()
        {
            _repository = new TaskRepository();
        }

        public UserTaskModel GetUserTask(string name)
        {
            return Mapper.MapUserTask(_repository.GetTask(name));
        }

        public bool CreateTask(UserTaskListModel list, UserTaskModel model)
        {
            return _repository.CreateTask(Mapper.MapTaskList(list), Mapper.MapUserTask(model));
        }

        public bool UpdateTask(UserTaskModel model)
        {
            return _repository.UpdateTask(Mapper.MapUserTask(model));
        }

        public bool DeleteTask(UserTaskModel model)
        {
            return _repository.DeleteTask(Mapper.MapUserTask(model));
        }
    }
}
