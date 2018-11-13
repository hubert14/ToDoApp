using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApp.Common.Models;
using ToDoApp.DAL.Models;

namespace ToDoApp.Common.Utils
{
   public static class Mapper
    {
        public static User MapUser(UserModel model)
        {
            if (model == null) return null;

            return new User(MapTaskLists(model.UserTaskLists))
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password
            };
        }

        public static UserModel MapUser(User model)
        {
            if (model == null) return null;

            return new UserModel()
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                UserTaskLists = MapTaskLists(model.UserTaskLists)
            };
        }

        public static IList<UserTaskList> MapTaskLists(List<UserTaskListModel> model)
        {
            if (model == null) return null;

            var returnable = new List<UserTaskList>();
            foreach (var item in model)
            {
                returnable.Add(MapTaskList(item));
            }

            return returnable;
        }

        public static List<UserTaskListModel> MapTaskLists(IList<UserTaskList> model)
        {
            return model?.Select(MapTaskList).ToList();
        }

        public static UserTaskList MapTaskList(UserTaskListModel model)
        {
            if (model == null) return null;

            return new UserTaskList(MapUserTaskList(model.UserTasks))
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static UserTaskListModel MapTaskList(UserTaskList model)
        {
            if (model == null) return null;

            return new UserTaskListModel()
            {
                Id = model.Id,
                Name = model.Name,
                UserTasks = MapUserTaskList(model.UserTasks).ToList()
            };
        }

        public static UserTask MapUserTask(UserTaskModel model)
        {
            if (model == null) return null;

            return new UserTask()
            {
                Id = model.Id,
                Checked = model.Checked,
                Name = model.Name,
                Description = model.Description
            };
        }

        public static UserTaskModel MapUserTask(UserTask model)
        {
            if (model == null) return null;

            return new UserTaskModel()
            {
                Id = model.Id,
                Checked = model.Checked,
                Name = model.Name,
                Description = model.Description
            };
        }

        public static List<UserTaskModel> MapUserTaskList(IList<UserTask> model)
        {
            return model?.Select(MapUserTask).ToList();
        }

        public static IList<UserTask> MapUserTaskList(List<UserTaskModel> model)
        {
            return model?.Select(MapUserTask).ToList();
        }
    }
}
