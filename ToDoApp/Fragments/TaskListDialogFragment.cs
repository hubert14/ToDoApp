using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ToDoApp.Common.Models;

namespace ToDoApp.Fragments
{
    public interface ICreateListDialogListener
    {
        void OnConfirmListCreate(string listName);
        void OnConfirmListEdit(UserTaskListModel taskList);
    }

    public class TaskListDialogFragment : DialogFragment
    {
        private ICreateListDialogListener _listener;
        private UserTaskListModel _taskList;

        public TaskListDialogFragment(UserTaskListModel model)
        {
            _taskList = model;
        }

        public TaskListDialogFragment()
        {
            _taskList = new UserTaskListModel();
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            LayoutInflater inflater = Activity.LayoutInflater;
            var view = inflater.Inflate(Resource.Layout.dialog_taskList, null);

            if (_taskList == null)
            {
                builder.SetView(view)
                    .SetPositiveButton("Create", (s, e) =>
                    {
                        var nameField = view.FindViewById<EditText>(Resource.Id.dialog_list_name);
                        _listener.OnConfirmListCreate(nameField.Text);
                    })
                    .SetNegativeButton("Cancel", (s, e) => { });
            }
            else
            {
                builder.SetView(view)
                    .SetPositiveButton("Edit", (s, e) =>
                    {
                        var nameField = view.FindViewById<EditText>(Resource.Id.dialog_list_name);
                        _taskList.Name = nameField.Text;
                        _listener.OnConfirmListEdit(_taskList);
                    })
                    .SetNegativeButton("Cancel", (s, e) => { });
            }

            return builder.Create();
        }

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            try
            {
                _listener = (ICreateListDialogListener) context;
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException(context + " must implement NoticeDialogListener");
            }
        }
    }
}