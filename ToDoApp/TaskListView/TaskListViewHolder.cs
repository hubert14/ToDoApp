using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ToDoApp.Activities;

namespace ToDoApp.TaskListView
{
    public class TaskListViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Name { get; private set; }
        public TextView Description { get; private set; }

        public TaskListViewHolder(View itemView) : base(itemView)
        {
            Image = itemView.FindViewById<ImageView>(Resource.Id.userTask_image);
            Name = itemView.FindViewById<TextView>(Resource.Id.userTask_task_name);
            Description = itemView.FindViewById<TextView>(Resource.Id.userTask_task_description);
        }
    }
}