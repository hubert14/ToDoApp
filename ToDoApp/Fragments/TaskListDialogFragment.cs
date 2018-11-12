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
    public interface ITaskListDialogListener
    {
        void OnConfirmListCreate(string listName);
        void OnConfirmListEdit(UserTaskListModel taskList);
    }

    public class TaskListDialogFragment : DialogFragment
    {
        private ITaskListDialogListener _listener;
        private UserTaskListModel _taskList;

        public TaskListDialogFragment(UserTaskListModel model)
        {
            _taskList = model;
        }

        public TaskListDialogFragment()
        {
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            LayoutInflater inflater = Activity.LayoutInflater;
            var view = inflater.Inflate(Resource.Layout.dialog_taskList, null);
            var headerTextView = view.FindViewById<TextView>(Resource.Id.dialog_list_text);
            if (_taskList == null)
            {
                headerTextView.Text = "CREATE NEW TASK LIST";
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
                var nameField = view.FindViewById<EditText>(Resource.Id.dialog_list_name);
                nameField.Text = _taskList.Name;
                headerTextView.Text = "EDIT TASK LIST";

                builder.SetView(view)
                    .SetPositiveButton("Edit", (s, e) =>
                    {
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
                _listener = (ITaskListDialogListener) context;
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException(context + " must implement NoticeDialogListener");
            }
        }
    }
}