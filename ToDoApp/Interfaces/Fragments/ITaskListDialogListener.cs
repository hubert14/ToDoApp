using ToDoApp.Common.Models;

namespace ToDoApp.Interfaces.Fragments
{
    public interface ITaskListDialogListener
    {
        void OnConfirmListCreate(string listName);
        void OnConfirmListEdit(UserTaskListModel taskList);
    }
}