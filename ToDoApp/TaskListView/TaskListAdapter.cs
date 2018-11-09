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
        public event CheckHandler CheckboxClickHandler;
        public event CheckHandler EditHandler;
        public event CheckHandler DeleteButtonHandler;
        
        public List<UserTaskModel> TaskList;
        public TaskListAdapter(List<UserTaskModel> taskList)
        {
            TaskList = taskList;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (!(holder is TaskListViewHolder vh)) return;

            vh.CheckBox.Checked = TaskList[position].Checked;
            vh.Name.Text = TaskList[position].Name;
            vh.Description.Text = TaskList[position].Description;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.userTask_view, parent, false);

            TaskListViewHolder vh = new TaskListViewHolder(itemView);

            vh.CheckBox.CheckedChange += (s, e) => { CheckboxClickHandler?.Invoke(this, vh.AdapterPosition); };

            void OnDeleteButtonClick(object s, EventArgs e)
            {
                DeleteButtonHandler?.Invoke(this, vh.AdapterPosition);
            }
            vh.DeleteButton.Click += OnDeleteButtonClick;

            vh.View.LongClick += (s,e) => { EditHandler?.Invoke(this, vh.AdapterPosition); };

            return vh;
        }

        public override int ItemCount => TaskList.Count;
    }
}