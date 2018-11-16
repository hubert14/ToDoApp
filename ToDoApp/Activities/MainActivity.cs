using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ToDoApp.Common.Models;
using ToDoApp.Fragments;
using ToDoApp.Interfaces.Fragments;
using ToDoApp.Interfaces.Views;
using ToDoApp.Presenters;
using ToDoApp.TaskListView;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace ToDoApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, IMainView, ITaskListDialogListener, IUserTaskDialogListener
    {
        private MainPresenter _presenter;

        private TextView _headerEmail;
        private TextView _headerName;

        private DrawerLayout _drawer;
        private Toolbar _toolbar;
        private NavigationView _navigationView;

        private RecyclerView _taskListView;
        private TaskListAdapter _taskListAdapter;
        private FloatingActionButton _createTaskFAB;
        private TextView _emptyTaskListsText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Initialize();
        }

        #region Initialization
        private void Initialize()
        {
            InitTaskListView();
            InitToolbar();
            InitFab();
            InitDrawer();
            InitNavigationView();

            _emptyTaskListsText = FindViewById<TextView>(Resource.Id.main_create_list_text);

            _presenter = new MainPresenter(this);
        }

        private void InitTaskListView()
        {
            _taskListView = FindViewById<RecyclerView>(Resource.Id.main_task_list);

            var layoutManager = new LinearLayoutManager(this);
            _taskListView.SetLayoutManager(layoutManager);
        }

        private void InitNavigationView()
        {
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            var header = _navigationView.GetHeaderView(0);
            _headerEmail = header.FindViewById<TextView>(Resource.Id.nav_header_email);
            _headerName = header.FindViewById<TextView>(Resource.Id.nav_header_userName);

            _navigationView.SetNavigationItemSelectedListener(this);
        }

        private void InitToolbar()
        {
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
        }

        private void InitDrawer()
        {
            _drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, _drawer, _toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            _drawer.AddDrawerListener(toggle);
            toggle.SyncState();

        }

        private void InitFab()
        {
            _createTaskFAB = FindViewById<FloatingActionButton>(Resource.Id.fab);
            _createTaskFAB.Click += FabOnClick;
        }

        private void InitAdapterHandlers()
        {
            _taskListAdapter.CheckboxClickHandler += (s, e) =>
            {
                if (e < 0) return;
                var item = _taskListAdapter.TaskList[e];
                _presenter.ChangeTaskCompleted(item);
            };
            _taskListAdapter.EditHandler += (s, e) =>
            {
                if (e < 0) return;
                var item = _taskListAdapter.TaskList[e];
                _presenter.EditTaskRequest(item);
            };
            _taskListAdapter.DeleteHandler += (s, e) =>
            {
                if (e < 0) return;
                var item = _taskListAdapter.TaskList[e];
                var snack = Snackbar.Make(CurrentFocus, Resource.String.taskHasBeenDeleted, Snackbar.LengthLong);
                var callback = new SnackbarUndoCallback(item, _presenter);
                snack.AddCallback(callback);
                snack.SetAction(Resource.String.undo, view =>
                {
                    snack.RemoveCallback(callback);
                    _taskListAdapter.NotifyDataSetChanged();
                });
                snack.Show();
            };
        }

        #endregion

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            _presenter.CreateTaskRequest();
        }

        #region Show info methods

        public void ShowUserInfo(string email, string name)
        {
            _headerEmail.Text = email;
            _headerName.Text = name;
        }

        public void ShowTaskLists(List<UserTaskListModel> list)
        {
            _navigationView.Menu.Clear();

            foreach (var taskList in list)
            {
                var icon = taskList.IsCompleted
                    ? Resource.Drawable.ic_check_box_green_200_24dp
                    : Resource.Drawable.ic_check_box_outline_blank_red_400_24dp;

                _navigationView.Menu.Add(taskList.Name).SetIcon(icon);
            }
            _createTaskFAB.Visibility = _navigationView.Menu.HasVisibleItems ? ViewStates.Visible : ViewStates.Gone;
            _emptyTaskListsText.Visibility = _navigationView.Menu.HasVisibleItems ? ViewStates.Gone : ViewStates.Visible;

            _navigationView.Menu.Add(Resource.String.newList).SetIcon(Resource.Drawable.ic_playlist_add_black_24dp);
        }

        public void ShowTasks(UserTaskListModel list)
        {
            if (list == null)
            {
                return;
            }
            
            Title = list.Name;

            if (_taskListAdapter != null)
            {
                _taskListAdapter.TaskList = list.UserTasks;
                _taskListAdapter.NotifyDataSetChanged();
            }
            else
            {
                _taskListAdapter = new TaskListAdapter(_taskListView, this, list.UserTasks);
                _taskListView.SetAdapter(_taskListAdapter);
                InitAdapterHandlers();
            }
        }

        #endregion

        #region Show popups

        public void ShowDeleteListAlert()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(Resource.String.areYouSure);
            builder.SetPositiveButton(Resource.String.yes, (sender, args) => { _presenter.DeleteList(); });
            builder.SetNegativeButton(Resource.String.no, (sender, args) => { });
            builder.Create().Show();
        }

        public void ShowEditTaskDialog(UserTaskModel model)
        {
            var dialog = new UserTaskDialogFragment(model);
            dialog.Show(FragmentManager, "editTask");
        }

        public void ShowCreateTaskDialog()
        {
            var dialog = new UserTaskDialogFragment();
            dialog.Show(FragmentManager, "createTask");
        }

        public string GetStringFromResourceId(int resId)
        {
            return GetString(resId);
        }

        public void ShowCreateListDialog()
        {
            var dialog = new TaskListDialogFragment();
            dialog.Show(FragmentManager, "createList");
        }

        public void ShowEditListDialog(UserTaskListModel model)
        {
            var dialog = new TaskListDialogFragment(model);
            dialog.Show(FragmentManager, "editList");
        }
        
        #endregion

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (drawer.IsDrawerOpen(GravityCompat.Start))
                drawer.CloseDrawer(GravityCompat.Start);
            else
                base.OnBackPressed();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var id = item.ItemId;

            if (id == Resource.Id.action_settings)
            {
                StartActivity(typeof(SettingsActivity));
                return true;
            }

            if (id == Resource.Id.action_editList)
            {
                _presenter.EditListRequest();
                return true;
            }

            if (id == Resource.Id.action_deleteList)
            {
                _presenter.DeleteListRequest();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            _presenter.ItemPressed(item);
            _drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        #region Response methods from dialogs

        /// <summary>
        /// Send request to presenter. Used in popup fragment
        /// </summary>
        public void OnConfirmListCreate(string listName)
        {
            _presenter.CreateList(listName);
        }

        /// <summary>
        /// Send request to presenter. Used in popup fragment
        /// </summary>
        public void OnConfirmListEdit(UserTaskListModel taskList)
        {
            _presenter.EditTaskList(taskList);
        }

        /// <summary>
        /// Send request to presenter. Used in popup fragment
        /// </summary>
        public void OnConfirmTaskCreate(UserTaskModel taskModel)
        {
            _presenter.CreateTask(taskModel);
        }

        /// <summary>
        /// Send request to presenter. Used in popup fragment
        /// </summary>
        public void OnConfirmTaskEdit(UserTaskModel taskModel)
        {
            _presenter.EditTask(taskModel);
        }

        #endregion
        
        protected override void OnResume()
        {
            base.OnResume();
            _presenter.UpdateViewRequest();
        }
    }
}