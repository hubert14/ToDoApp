using ToDoApp.Common.Models;

namespace ToDoApp.Interfaces.Fragments
{
    public interface IUserTaskDialogListener
    {
        void OnConfirmTaskCreate(UserTaskModel taskModel);
        void OnConfirmTaskEdit(UserTaskModel taskModel);
    }
}