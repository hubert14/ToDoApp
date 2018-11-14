using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics;
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

            vh.ItemView.LongClick += (s, e) =>
            {
                var item = new ClipData.Item(vh.Name.Text);
                var data = new ClipData(vh.Name.Text, new[] {ClipDescription.MimetypeTextPlain}, item);
                
                vh.ItemView.StartDragAndDrop(data, new View.DragShadowBuilder(vh.ItemView), vh, 0);
                //EditHandler?.Invoke(this, vh.AdapterPosition);
            };
            vh.ItemView.Drag += (sender, args) =>
            {
                var task = args.Event.LocalState as TaskListViewHolder;
                
                switch (args.Event.Action)
                {
                    case DragAction.Started:
                    {
                        vh.ItemView.SetBackgroundColor(Color.Blue);
                        vh.ItemView.Invalidate();
                        break;

                    }
                    case DragAction.Entered:
                    {
                        vh.ItemView.SetBackgroundColor(Color.Green);
                        vh.ItemView.Invalidate();
                        break;

                    }
                    case DragAction.Exited:
                    {
                        vh.ItemView.SetBackgroundColor(Color.Blue);
                        vh.ItemView.Invalidate();
                        break;

                    }
                    case DragAction.Location:
                        break;

                    case DragAction.Drop:
                    {
                        var position1 = task.AdapterPosition;
                        var position2 = vh.AdapterPosition;
                        var task1 = TaskList[position1];
                        var task2 = TaskList[position2];
                        TaskList[position2] = task1;
                        TaskList[position1] = task2;
                        NotifyDataSetChanged();
                        
                        vh.ItemView.SetBackgroundColor(Color.Transparent);
                        vh.ItemView.Invalidate();
                        break;
                    }
                    case DragAction.Ended:
                    {
                        vh.ItemView.SetBackgroundColor(Color.Transparent);
                        vh.ItemView.Invalidate();
                        break;
                    }
                }
            };

            return vh;
        }

        public override int ItemCount => TaskList.Count;
    }
}