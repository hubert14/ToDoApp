using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace ToDoApp.TaskListView
{
    public class TaskListViewHolder : RecyclerView.ViewHolder
    {
        public CheckBox CheckBox { get; private set; }

        public TextView Name { get; private set; }
        public TextView Description { get; private set; }

        public Button EditButton { get; private set; }
        public Button DeleteButton { get; private set; }

        public TaskListViewHolder(View itemView) : base(itemView)
        {
            CheckBox = itemView.FindViewById<CheckBox>(Resource.Id.userTask_checkBox);

            Name = itemView.FindViewById<TextView>(Resource.Id.userTask_task_name);
            Description = itemView.FindViewById<TextView>(Resource.Id.userTask_task_description);

            EditButton = itemView.FindViewById<Button>(Resource.Id.userTask_task_edit);
            DeleteButton = itemView.FindViewById<Button>(Resource.Id.userTask_task_delete);
        }
    }
}