using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;

namespace ToDoApp.TaskListView
{
    public class TaskListItemTouchHelper : ItemTouchHelper.Callback
    {
        private TaskListAdapter _adapter;
        public TaskListItemTouchHelper(TaskListAdapter adapter)
        {
            _adapter = adapter;
        }
        public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
            int swipeFlags = ItemTouchHelper.End;
            return MakeMovementFlags(dragFlags, swipeFlags);
        }

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
        {
            _adapter.NotifyItemMoved(viewHolder.AdapterPosition, target.AdapterPosition);
            return true;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            _adapter.InvokeDeleteHandler(viewHolder.AdapterPosition);
        }
    }
}