using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ToDoApp.Activities.Interfaces;
using ToDoApp.Common.Models;
using ToDoApp.Fragments;
using ToDoApp.Presenters;
using ToDoApp.TaskListView;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace ToDoApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, IMainView, ICreateListDialogListener, IUserTaskDialogListener
    {
        private MainPresenter _presenter;

        private TextView _headerEmail;
        private TextView _headerName;

        private DrawerLayout _drawer;
        private Toolbar _toolbar;
        private NavigationView _navigationView;

        private RecyclerView _taskListView;
        private TaskListAdapter _taskListAdapter;

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
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        #endregion

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            UserTaskDialogFragment dialog = new UserTaskDialogFragment();
            dialog.Show(FragmentManager, "createTask");
        }

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
                var isSuccess = taskList.UserTasks.FirstOrDefault(x => x.Checked == false) == null;

                var icon = isSuccess
                    ? Resource.Drawable.ic_check_box_green_200_24dp
                    : Resource.Drawable.ic_check_box_outline_blank_red_400_24dp;

                _navigationView.Menu.Add(taskList.Name).SetIcon(icon);
            }

            _navigationView.Menu.Add("New list").SetIcon(Resource.Drawable.ic_playlist_add_black_24dp);
        }

        public void ShowTasks(List<UserTaskModel> list)
        {
            _taskListAdapter = new TaskListAdapter(list);

            _taskListAdapter.TouchHandler += (s, e) =>
            {
                var item = _taskListAdapter.TaskList[e];
                _presenter.SendChangeCheckRequest(item); 
            };

            _taskListView.SetAdapter(_taskListAdapter);
            _taskListAdapter.NotifyDataSetChanged();
        }

        public void StartCreateListActivity()
        {
            CreateTaskListFragment dialog = new CreateTaskListFragment();
            dialog.Show(FragmentManager, "createList");
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
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

            return base.OnOptionsItemSelected(item);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            _presenter.ItemPressed(item);
            _drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public void OnConfirmListCreate(string listName)
        {
            _presenter.CreateListRequest(listName);
        }

        public void OnConfirmTaskCreate(UserTaskModel taskModel)
        {
            _presenter.CreateTaskRequest(taskModel);
        }

        public void OnConfirmTaskEdit(UserTaskModel taskModel)
        {
            _presenter.EditTaskRequest(taskModel);
        }
    }
}



