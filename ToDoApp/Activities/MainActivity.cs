using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ToDoApp.Activities.Interfaces;
using ToDoApp.Common.Models;
using ToDoApp.Presenters;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace ToDoApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, IMainView
    {
        private MainPresenter _presenter;

        private TextView _headerEmail;
        private TextView _headerName;

        private DrawerLayout _drawer;
        private Toolbar _toolbar;
        private NavigationView _navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Initialize();
        }

        #region Initialization
        private void Initialize()
        {
            _presenter = new MainPresenter(this);

            InitToolbar();
            InitNavigationHeader();
            InitFab();
            InitDrawer();
            InitNavigationViewListener();

            _presenter.GetUserInfo();
        }

        private void InitNavigationViewListener()
        {
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
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

        private void InitNavigationHeader()
        {
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            var header = _navigationView.GetHeaderView(0);

            _headerEmail = header.FindViewById<TextView>(Resource.Id.nav_header_email);
            _headerName = header.FindViewById<TextView>(Resource.Id.nav_header_userName);
        }

        private void InitFab()
        {
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        #endregion

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
            // TODO: Create task action
        }

        public void ShowUserInfo(string email, string name)
        {
            _headerEmail.Text = email;
            _headerName.Text = name;
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
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                // Handle the camera action
            }
            else if (id == Resource.Id.nav_gallery)
            {

            }
            else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            _drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
    }
}

