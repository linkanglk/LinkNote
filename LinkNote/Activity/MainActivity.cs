using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using System.Collections.Generic;
using LinkNote.Fragment;
using Android.Views;
using System;

namespace LinkNote
{
    [Activity(Label = "LinkNote")]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            if (navigationView != null)
                setupDrawerContent(navigationView);

            var viewPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewpager);
            if (viewPager != null)
                setupViewPager(viewPager);

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += (sender, e) => {
                Snackbar.Make(fab, "Here's a snackbar!", Snackbar.LengthLong).SetAction("Action",
                    new ClickListener(v => {
                        Console.WriteLine("Action handler");
                    })).Show();
            };

            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewPager);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.SampleActions, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        void setupDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) => {
                e.MenuItem.SetChecked(true);
                drawerLayout.CloseDrawers();
            };
        }

        void setupViewPager(Android.Support.V4.View.ViewPager viewPager)
        {
            var adapter = new Adapter(SupportFragmentManager);
            adapter.AddFragment(new NoteListFragment(), "Category 1");
            adapter.AddFragment(new NoteListFragment(), "Category 2");
            adapter.AddFragment(new NoteListFragment(), "Category 3");
            viewPager.Adapter = adapter;
        }

        class Adapter : Android.Support.V4.App.FragmentPagerAdapter
        {
            List<V4Fragment> fragments = new List<V4Fragment>();
            List<string> fragmentTitles = new List<string>();

            public Adapter(V4FragmentManager fm) : base(fm)
            {
            }

            public void AddFragment(V4Fragment fragment, string title)
            {
                fragments.Add(fragment);
                fragmentTitles.Add(title);
            }

            public override V4Fragment GetItem(int position)
            {
                return fragments[position];
            }

            public override int Count
            {
                get { return fragments.Count; }
            }

            public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
            {
                return new Java.Lang.String(fragmentTitles[position]);
            }

        }
    }

    public class ClickListener : Java.Lang.Object, View.IOnClickListener
    {
        public ClickListener(Action<View> handler)
        {
            Handler = handler;
        }

        public Action<View> Handler { get; set; }

        public void OnClick(View v)
        {
            var h = Handler;
            if (h != null)
                h(v);
        }
    }
}

