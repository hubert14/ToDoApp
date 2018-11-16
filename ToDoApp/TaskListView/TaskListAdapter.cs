using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
using ToDoApp.Common.Models;
using PopupMenu = Android.Support.V7.Widget.PopupMenu;

namespace ToDoApp.TaskListView
{
    public delegate void CheckHandler(object sender, int position);

    public class TaskListAdapter : RecyclerView.Adapter
    {
        public event CheckHandler CheckboxClickHandler;
        public event CheckHandler EditHandler;
        public event CheckHandler DeleteHandler;
        
        private readonly Context _context;
        public List<UserTaskModel> TaskList;

        public TaskListAdapter(RecyclerView view, Context context, List<UserTaskModel> taskList)
        {
            _context = context;
            TaskList = taskList;

            ItemTouchHelper.Callback callback = new TaskListItemTouchHelper(this);
            ItemTouchHelper itemTouchHelper = new ItemTouchHelper(callback);
            itemTouchHelper.AttachToRecyclerView(view);
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
            InitHandlers(vh);
            return vh;
        }

        private void InitHandlers(TaskListViewHolder vh)
        {
            vh.CheckBox.Click += (s, e) =>
            {
                CheckboxClickHandler?.Invoke(this, vh.AdapterPosition);
            };

            var toolbar = vh.ItemView.FindViewById<ImageView>(Resource.Id.task_toolbar);
            toolbar.Click += (sender, args) =>
            {
                var popup = new PopupMenu(_context, toolbar);

                popup.Inflate(Resource.Menu.menu_task);
                popup.MenuItemClick += (o, eventArgs) =>
                {
                    if (eventArgs.Item.ItemId == Resource.Id.userTask_task_delete)
                    {
                        InvokeDeleteHandler(vh.AdapterPosition);
                    }
                    else if (eventArgs.Item.ItemId == Resource.Id.userTask_task_edit)
                    {
                        EditHandler?.Invoke(this, vh.AdapterPosition);
                    }
                };
                popup.Show();
            };
        }

        public void InvokeDeleteHandler(int position)
        {
            DeleteHandler?.Invoke(this, position);
        }

        public override int ItemCount => TaskList.Count;
    }
}