using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using ToDoApp.Activities.Interfaces;
using ToDoApp.Common.Models;
using ToDoApp.TaskListView;

namespace ToDoApp.Presenters
{
    public class MainPresenter : BasePresenter
    {
        private IMainView _view;

        private List<UserTaskListModel> _lists;
        private UserTaskListModel _currentList;

        public MainPresenter(IMainView view)
        {
            _view = view;
            _lists = TaskListService.GetAllTaskLists();

            _view.ShowUserInfo(User.Email, User.FirstName + " " + User.LastName);

            if (_lists.Count > 1)
                _currentList = _lists[0];

            UpdateView();
        }

        public void ItemPressed(IMenuItem item)
        {
            if (item.TitleFormatted.ToString() == "New list")
            {
                _view.StartCreateListActivity();
                return;
            }

            _currentList = _lists.FirstOrDefault(x => x.Name == item.TitleFormatted.ToString());
            if (_currentList == null) return;

            Update();
        }

        public void CreateListRequest(string listName)
        {
            var newList = new UserTaskListModel() {Name = listName};
            TaskListService.CreateTaskList(newList);
            _currentList = newList;
            Update();
        }

        public void EditTaskRequest(UserTaskModel taskModel)
        {
            TaskService.UpdateTask(taskModel);
            Update();
        }

        public void CreateTaskRequest(UserTaskModel taskModel)
        {
            TaskService.CreateTask(_currentList, taskModel);
            Update();
        }

        public void SendChangeCheckRequest(UserTaskModel item)
        {
            item.Checked = !item.Checked;
            TaskService.UpdateTask(item);
            _view.ShowTaskLists(_lists);
        }

        public void DeleteTaskRequest(UserTaskModel item)
        {
            TaskService.DeleteTask(item);
            Update();
        }

        private void Update()
        {
            UpdateData();
            UpdateView();
        }

        private void UpdateData()
        {
            _lists = TaskListService.GetAllTaskLists();

            _currentList = TaskListService.GetTaskList(_currentList.Name);
        }

        private void UpdateView()
        {
            _view.ShowTaskLists(_lists);

            if (_currentList == null) return;
            _view.ShowTasks(_currentList);
        }

        public void DeleteListRequest()
        {
            _view.
            TaskListService.DeleteTaskList(_currentList);
            _currentList = _lists[0] ?? new UserTaskListModel();

            Update();
        }

        public void EditTaskListRequest(UserTaskListModel taskList)
        {
            TaskListService.UpdateTaskList(taskList);
            _currentList = taskList;
            Update();
        }

        public void SendEditListRequest()
        {
            _view.
        }
    }
}