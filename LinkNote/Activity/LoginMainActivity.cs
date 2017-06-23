using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Graphics;
using Android.Util;
using LinkNote.OtherClass;
using Java.Lang;
using System.Collections;
using LinkNote.ViewPager;
using LinkNote.Adapter.ViewPagerAdapter;
using Android.Support.V7.App;
using LinkNote.Fragment;

namespace LinkNote.Activity
{
    [Activity(Label = "LoginMainActivity")]
    public class LoginMainActivity : AppCompatActivity
    {
        LoginMainActivity _context;
        public RelativeLayout header;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // 设置页面
            SetContentView(Resource.Layout.LoginMain);
            _context = this;
            int statusBarHeight = 0, totalHeight = 0, contentHeight = 0;
            int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                statusBarHeight = Resources.GetDimensionPixelSize(resourceId);
                totalHeight = Resources.DisplayMetrics.HeightPixels;
                contentHeight = totalHeight - statusBarHeight;
            }
            header = (RelativeLayout)FindViewById(Resource.Id.lyHeader);
            FindViewById(Resource.Id.ilBody);
            InitView(); // 初始化页面
        }

        private void InitView()
        {
            CoordinatorLayout rootView = (CoordinatorLayout)FindViewById(Resource.Id.clRootActivity);
            rootView.ViewTreeObserver.AddOnGlobalLayoutListener(new RootViewOnGlobalLayoutListener(rootView, _context));

            LinkNoteViewPager vpBody =(LinkNoteViewPager)FindViewById(Resource.Id.vpBody);
            if (vpBody != null)
                SetViewPageBodyAdapter(vpBody);

            //Button btnContinue = (Button)FindViewById(Resource.Id.btnContinue); // 继续按钮
            //btnContinue.Click += BtnContinue_Click; // 继续按钮点击事件
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            EditText txtUserName = (EditText)FindViewById(Resource.Id.txtUserName);
            // 判断网络状态
            if (new NetworkState(this).Check())
            {
                if (txtUserName.Text == "")
                {
                    new LinkNoteAlertDialog(this)
                        .CreateShow(Resource.String.LoginErrorTitle,
                        Resource.String.LoginNameErrorMessage, null);
                }
                else
                {
                    Intent mainIntent = new Intent();
                    mainIntent.SetClass(this, typeof(MainActivity));
                    StartActivity(mainIntent);
                }
            }
            else
            {
                new LinkNoteAlertDialog(this)
                    .CreateShow(Resource.String.LoginErrorTitle,
                    Resource.String.NetworkStateErrorMessage, null);
            }
        }

        private void SetViewPageBodyAdapter(LinkNoteViewPager vpBody)
        {
            LinkNoteViewPageAdapter adapter = new LinkNoteViewPageAdapter(SupportFragmentManager);
            adapter.AddFragment(new LoginFristStepFragment());
            adapter.AddFragment(new LoginSecondStepFragment());
            vpBody.Adapter = adapter;
        }
    }
    
    public class RootViewOnGlobalLayoutListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        CoordinatorLayout rootView;
        LoginMainActivity _context;

        public RootViewOnGlobalLayoutListener(CoordinatorLayout rootView, LoginMainActivity _context)
        {
            this.rootView = rootView;
            this._context = _context;
        }

        public void OnGlobalLayout()
        {
            int softKeyboardHeight = 100;
            Rect r = new Rect();
            rootView.GetWindowVisibleDisplayFrame(r);
            DisplayMetrics dm = rootView.Resources.DisplayMetrics;
            int heightDiff = rootView.Bottom - r.Bottom;
            if (heightDiff > softKeyboardHeight * dm.Density)
            {
                // 弹出软键盘
                _context.header.Visibility = ViewStates.Gone;
            }
            else
            {
                // 没有弹出软键盘
                _context.header.Visibility = ViewStates.Visible;
            }
        }
    }
}