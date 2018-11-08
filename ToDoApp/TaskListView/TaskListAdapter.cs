using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ToDoApp.Common.Models;

namespace ToDoApp.TaskListView
{
    public delegate void CheckHandler(object sender, int position);

    public class TaskListAdapter : RecyclerView.Adapter
    {
        public event CheckHandler TouchHandler;
        
        public List<UserTaskModel> TaskList;
        public TaskListAdapter(List<UserTaskModel> taskList)
        {
            TaskList = taskList;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (!(holder is TaskListViewHolder vh)) return;

            var isChecked = TaskList[position].Checked;

            vh.Image.SetImageResource(isChecked 
                ? Resource.Drawable.ic_check_box_green_200_24dp 
                : Resource.Drawable.ic_check_box_outline_blank_red_400_24dp);

            vh.Name.Text = TaskList[position].Name;
            vh.Description.Text = TaskList[position].Description;

            vh.Image.Touch += (sender, args) =>
            {
                if(args.Event.Action == MotionEventActions.Up)
                    TouchHandler?.Invoke(this, position);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.userTask_view, parent, false);

            TaskListViewHolder vh = new TaskListViewHolder(itemView);
            return vh;
        }

        public override int ItemCount => TaskList.Count;
    }
}