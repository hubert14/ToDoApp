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
    public class UserTaskDialogFragment : DialogFragment
    {
        private IUserTaskDialogListener _listener;
        private UserTaskModel _taskModel;

        public UserTaskDialogFragment()
        {
        }

        public UserTaskDialogFragment(UserTaskModel taskModel)
        {
            _taskModel = taskModel;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            LayoutInflater inflater = Activity.LayoutInflater;
            var view = inflater.Inflate(ToDoApp.Resources.Resource.Layout.dialog_userTask, null);

            var nameField = view.FindViewById<EditText>(ToDoApp.Resources.Resource.Id.dialog_userTask_name);
            var descriptionField = view.FindViewById<EditText>(ToDoApp.Resources.Resource.Id.dialog_userTask_description);

            var headerText = view.FindViewById<TextView>(ToDoApp.Resources.Resource.Id.dialog_userTask_text);

            if (_taskModel != null)
            {
                nameField.Text = _taskModel.Name;
                descriptionField.Text = _taskModel.Description;
                headerText.Text = "EDIT TASK";

                builder.SetView(view)
                    .SetPositiveButton("Edit", (s, e) =>
                    {
                        _taskModel.Name = nameField.Text;
                        _taskModel.Description = descriptionField.Text;

                        _listener.OnConfirmTaskEdit(_taskModel);
                    })
                    .SetNegativeButton("Cancel", (s, e) => { });
            }
            else
            {
                headerText.Text = "CREATE NEW TASK";

                builder.SetView(view)
                    .SetPositiveButton("Create", (s, e) =>
                    {
                        _taskModel = new UserTaskModel()
                        {
                            Name = nameField.Text,
                            Description = descriptionField.Text,
                            Checked = false
                        };
                        _listener.OnConfirmTaskCreate(_taskModel);
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
                _listener = (IUserTaskDialogListener) context;
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException(context + " must implement NoticeDialogListener");
            }
        }
    }
}