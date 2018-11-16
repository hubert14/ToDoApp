using Android.Support.Design.Widget;
using Java.Lang;
using ToDoApp.Common.Models;
using ToDoApp.Presenters;

namespace ToDoApp
{
    public class SnackbarUndoCallback : BaseTransientBottomBar.BaseCallback
    {
        private readonly UserTaskModel _item;
        private readonly MainPresenter _presenter;

        public SnackbarUndoCallback(UserTaskModel item, MainPresenter presenter)
        {
            _item = item;
            _presenter = presenter;
        }

        public override void OnDismissed(Object transientBottomBar, int @event)
        {
            _presenter.DeleteTask(_item);
            base.OnDismissed(transientBottomBar, @event);
        }
    }
}