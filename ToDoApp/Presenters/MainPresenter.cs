using System.Collections.Generic;
using Android.App;
using Android.Views;
using ToDoApp.Activities.Interfaces;
using ToDoApp.Common.Models;

namespace ToDoApp.Presenters
{
    public class MainPresenter : BasePresenter
    {
        private readonly IMainView _view;

        private List<UserTaskList> _lists;

        public MainPresenter(IMainView view)
        {
            _view = view;
            
        }
        
        public void GetUserInfo()
        {
            _view.ShowUserInfo(User.Email, User.FirstName + " " + User.LastName);
        }

        public void ItemPressed(IMenuItem item)
        {
            foreach (var list in _lists)
            {
                if (item.TitleFormatted.ToString() == list.Name)
                {
                    // TODO: Go to this list activity
                }
            }
            
        }
    }
}