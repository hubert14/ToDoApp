using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using ToDoApp.Activities.Interfaces;
using ToDoApp.Common.Models;

namespace ToDoApp.Presenters
{
    public class MainPresenter : BasePresenter
    {
        private readonly IMainView _view;

        private List<UserTaskListModel> _lists;
        private UserTaskListModel _currentList;

        public MainPresenter(IMainView view)
        {
            _view = view;
            _lists = TaskListService.GetAllTaskLists();

            _view.ShowUserInfo(User.Email, User.FirstName + " " + User.LastName);

            if (_lists.Count < 1) return;
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

            foreach (var list in _lists)
            {
                if (item.TitleFormatted.ToString() != list.Name) continue;

                _currentList = list;
                break;
            }
            Update();
        }

        public void CreateListRequest(string listName)
        {
            TaskListService.CreateTaskList(new UserTaskListModel() {Name = listName});
            _lists = TaskListService.GetAllTaskLists();
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
            if (_lists.Count < 1 || _currentList == null) return;

            _view.ShowTaskLists(_lists);
            _view.ShowTasks(_currentList.UserTasks.ToList());
        }

        public void SendChangeCheckRequest(UserTaskModel item)
        {
            item.Checked = !item.Checked;
            TaskService.UpdateTask(item);
            Update();
        }
    }
}