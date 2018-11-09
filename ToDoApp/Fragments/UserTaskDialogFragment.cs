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
    public interface IUserTaskDialogListener
    {
        void OnConfirmTaskCreate(UserTaskModel taskModel);
        void OnConfirmTaskEdit(UserTaskModel taskModel);
    }

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

            if (_taskModel != null)
            {
                nameField.Text = _taskModel.Name;
                descriptionField.Text = _taskModel.Description;

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