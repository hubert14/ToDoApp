using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using ToDoApp.Common.Models;
using ToDoApp.Interfaces.Fragments;

namespace ToDoApp.Fragments
{
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

            var headerText = view.FindViewById<TextView>(Resource.Id.dialog_list_text);

            if (_taskList == null)
            {
                headerText.Text = GetString(Resource.String.createNewTaskList).ToUpper();
                builder.SetView(view)
                    .SetPositiveButton(Resource.String.create, (s, e) =>
                    {
                        var nameField = view.FindViewById<EditText>(Resource.Id.dialog_list_name);
                        _listener.OnConfirmListCreate(nameField.Text);
                    })
                    .SetNegativeButton(Resource.String.cancel, (s, e) => { });
            }
            else
            {
                var nameField = view.FindViewById<EditText>(Resource.Id.dialog_list_name);
                nameField.Text = _taskList.Name;
                headerText.Text = GetString(Resource.String.editTaskListName).ToUpper();

                builder.SetView(view)
                    .SetPositiveButton(Resource.String.edit, (s, e) =>
                    {
                        _taskList.Name = nameField.Text;
                        _listener.OnConfirmListEdit(_taskList);
                    })
                    .SetNegativeButton(Resource.String.cancel, (s, e) => { });
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
            catch (InvalidCastException)
            {
                throw new InvalidCastException(context + " must implement NoticeDialogListener");
            }
        }
    }
}