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
using Android.Content.Res;
using Android.Graphics;
using CheeseBind;
using Android.Support.V7.Widget;
using LinkNote.Adapter.CustomizeNavigationViewAdapter;
using LinkNote.Model.CustomizeNavigationView;
using Android.Support.V4.View;
using Android.Content;
using Android.Util;

namespace LinkNote
{
    [Activity(Label = "LinkNote")]
    public class MainActivity : AppCompatActivity
    {
        // 抽屉菜单对象
        [BindView(Resource.Id.nav_view)]
        RecyclerView navigationView;
        // 外层DrawerLayout对象
        [BindView(Resource.Id.drawer_layout)]
        DrawerLayout drawerlayout;

        int height = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Cheeseknife.Bind(this);

            DisplayMetrics metric = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metric);
            height = metric.HeightPixels;   // 屏幕高度（像素）  

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            setupDrawerContent(); // 设置抽屉菜单内容

            var viewPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewpager);
            if (viewPager != null)
                setupViewPager(viewPager);

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += (sender, e) =>
            {
                Snackbar.Make(fab, "Here's a snackbar!", Snackbar.LengthLong).SetAction("Action",
                    new ClickListener(v =>
                    {
                        Console.WriteLine("Action handler");
                    })).Show();
            };
            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            //tabLayout.SetupWithViewPager(viewPager);
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
                    drawerlayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        void setupDrawerContent()
        {
            ListViewAdapter listViewAdapter = new ListViewAdapter(this);
            List<MenuDrawerItem> dateList = new List<MenuDrawerItem>() {
                new MenuDrawerItemBody(listViewAdapter)
            };
            MenuDrawerAdapter menuDrawerAdapter = new MenuDrawerAdapter(dateList, height);
            menuDrawerAdapter.SetOnItemClickListener(new UpgradeOnItemClickListener());
            menuDrawerAdapter.SetSynchronizeOnItemClickListener(new SynchronizeOnItemClickListener());

            CustomLinearLayoutManager linearLayoutManager = new CustomLinearLayoutManager(this);
            linearLayoutManager.setScrollEnabled(false);

            navigationView.SetLayoutManager(linearLayoutManager);
            navigationView.SetAdapter(menuDrawerAdapter);

        }

        void setupViewPager(Android.Support.V4.View.ViewPager viewPager)
        {
            var adapter = new Adapter(SupportFragmentManager);
            adapter.AddFragment(new NoteListFragment(), "所有笔记");
            adapter.AddFragment(new NoteListFragment(), "定时笔记");
            adapter.AddFragment(new NoteListFragment(), "回收站");
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

        public class CustomLinearLayoutManager : LinearLayoutManager
        {
            private bool isScrollEnabled = true;

            public CustomLinearLayoutManager(Context context) : base(context)
            {

            }

            public void setScrollEnabled(bool flag)
            {
                this.isScrollEnabled = flag;
            }


            public override bool CanScrollVertically()
            {
                //Similarly you can customize "canScrollHorizontally()" for managing horizontal scroll
                return isScrollEnabled && this.CanScrollVertically();
            }
        }

        //#region 菜单点击事件

        //public class MyOnItemClickListener : DrawerAdapter.OnItemClickListener
        //{
        //    DrawerLayout drawerlayout;

        //    public MyOnItemClickListener(DrawerLayout drawerlayout)
        //    {
        //        this.drawerlayout = drawerlayout;
        //    }

        //    public void itemClick(DrawerItemNormal drawerItemNormal)
        //    {
        //        switch (drawerItemNormal.titleRes)
        //        {
        //            case Resource.String.drawer_menu_home://首页
        //                break;
        //            case Resource.String.drawer_menu_rank://排行榜
        //                break;
        //            case Resource.String.drawer_menu_column://栏目
        //                break;
        //            case Resource.String.drawer_menu_search://搜索
        //                break;
        //            case Resource.String.drawer_menu_trash://垃圾箱
        //                break;
        //            case Resource.String.drawer_menu_night://夜间模式
        //                break;
        //            case Resource.String.drawer_menu_setting://设置
        //                break;
        //        }
        //        drawerlayout.CloseDrawer(GravityCompat.Start);
        //    }
        //}

        //#endregion

        #region 升级账号点击事件

        public class UpgradeOnItemClickListener : MenuDrawerAdapter.OnItemClickListener
        {
            public void itemClick(MenuDrawerItemBody drawerItemNormal)
            {

            }
        }

        #endregion

        #region 数据同步点击事件

        public class SynchronizeOnItemClickListener : MenuDrawerAdapter.OnSynchronizeItemClickListener
        {
            public void itemClick(MenuDrawerItemBody drawerItemNormal)
            {

            }
        }

        #endregion
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

