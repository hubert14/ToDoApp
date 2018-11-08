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

namespace ToDoApp.Fragments
{
    public interface ICreateListDialogListener
    {
        void OnConfirmListCreate(string listName);
    }

    public class CreateTaskListFragment : DialogFragment
    {
        private ICreateListDialogListener _listener;

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            LayoutInflater inflater = Activity.LayoutInflater;
            var view = inflater.Inflate(Resource.Layout.dialog_createList, null);
            builder.SetView(view)
                .SetPositiveButton("Create", (s, e) =>
                {
                    var nameField = view.FindViewById<EditText>(Resource.Id.create_list_name);
                    _listener.OnConfirmListCreate(nameField.Text);
                })
                .SetNegativeButton("Cancel", (s, e) => { });

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