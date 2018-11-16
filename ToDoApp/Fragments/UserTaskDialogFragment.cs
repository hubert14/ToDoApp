using System;
using System.Globalization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Util;
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
            var view = inflater.Inflate(Resource.Layout.dialog_userTask, null);

            var nameField = view.FindViewById<EditText>(Resource.Id.dialog_userTask_name);
            var descriptionField = view.FindViewById<EditText>(Resource.Id.dialog_userTask_description);

            var headerText = view.FindViewById<TextView>(Resource.Id.dialog_userTask_text);

            if (_taskModel != null)
            {
                nameField.Text = _taskModel.Name;
                descriptionField.Text = _taskModel.Description;
                headerText.Text = GetString(Resource.String.editTask);

                builder.SetView(view)
                    .SetPositiveButton(Resource.String.edit, (s, e) =>
                    {
                        _taskModel.Name = nameField.Text;
                        _taskModel.Description = descriptionField.Text;

                        _listener.OnConfirmTaskEdit(_taskModel);
                    })
                    .SetNegativeButton(Resource.String.cancel, (s, e) => { });
            }
            else
            {
                headerText.Text = GetString(Resource.String.createNewTask);

                builder.SetView(view)
                    .SetPositiveButton(Resource.String.create, (s, e) =>
                    {
                        _taskModel = new UserTaskModel()
                        {
                            Name = nameField.Text,
                            Description = descriptionField.Text,
                            Checked = false
                        };
                        _listener.OnConfirmTaskCreate(_taskModel);
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
                _listener = (IUserTaskDialogListener) context;
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException(context + " must implement NoticeDialogListener");
            }
        }
    }
}