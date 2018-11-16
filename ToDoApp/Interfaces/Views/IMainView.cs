using System.Collections.Generic;
using ToDoApp.Common.Models;

namespace ToDoApp.Interfaces.Views
{
    public interface IMainView
    {
        void ShowUserInfo(string email, string name);
        void ShowTaskLists(List<UserTaskListModel> list);
        void ShowTasks(UserTaskListModel list);
        
        void ShowDeleteListAlert();

        void ShowEditListDialog(UserTaskListModel model);
        void ShowCreateListDialog();

        void ShowEditTaskDialog(UserTaskModel model);
        void ShowCreateTaskDialog();

        string GetStringFromResourceId(int resId);
    }
}