using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApp.Common.Models;

namespace ToDoApp.Activities.Interfaces
{
    public interface ISettingsView
    {
        void ShowDeleteUserAlert();
        void ShowUserInfo(UserModel user);
        UserModel GetUserInfo();
        void ShowMessage(string message);
        void GoToLoginActivity();
    }
}