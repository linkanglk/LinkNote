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
        LinkNoteViewPager vpBody;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // 设置页面
            SetContentView(Resource.Layout.LoginMain);
            _context = this;
            header = (RelativeLayout)FindViewById(Resource.Id.lyHeader);
            InitView(); // 初始化页面
        }

        private void InitView()
        {
            CoordinatorLayout rootView = (CoordinatorLayout)FindViewById(Resource.Id.clRootActivity);
            rootView.ViewTreeObserver.AddOnGlobalLayoutListener(new RootViewOnGlobalLayoutListener(rootView, _context));

            vpBody = (LinkNoteViewPager)FindViewById(Resource.Id.vpBody);
            if (vpBody != null)
            {
                vpBody.PageSelected += VpBody_PageSelected;
                SetViewPageBodyAdapter(vpBody);
            }
        }

        private void SetViewPageBodyAdapter(LinkNoteViewPager vpBody)
        {
            LinkNoteViewPageAdapter adapter = new LinkNoteViewPageAdapter(SupportFragmentManager);
            adapter.AddFragment(new LoginFristStepFragment(FristViewCreateViewCallBack));
            adapter.AddFragment(new LoginSecondStepFragment(SecondViewCreateViewCallBack));
            vpBody.Adapter = adapter;
        }

        private void VpBody_PageSelected(object sender, Android.Support.V4.View.ViewPager.PageSelectedEventArgs e)
        {
            vpBody.SetFragmentIndex(e.Position);
        }

        private void FristViewCreateViewCallBack(View fristView)
        {
            Button btnContinue = (Button)fristView.FindViewById(Resource.Id.btnContinue);
            btnContinue.Click += delegate
            {
                EditText txtUserName = (EditText)fristView.FindViewById(Resource.Id.txtUserName);
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
                        // 跳转到下一页
                        vpBody.SetCurrentItem(1, true);
                    }
                }
                else
                {
                    new LinkNoteAlertDialog(this)
                        .CreateShow(Resource.String.LoginErrorTitle,
                        Resource.String.NetworkStateErrorMessage, null);
                }
            };
        }

        private void SecondViewCreateViewCallBack(View secondView)
        {
            Button btnLogin = (Button)secondView.FindViewById(Resource.Id.btnLogin);
            btnLogin.Click += delegate
            {
                EditText txtUserName = (EditText)secondView.FindViewById(Resource.Id.txtUserName);
                EditText txtUserPassWord = (EditText)secondView.FindViewById(Resource.Id.txtUserPassWord);
                if (txtUserName.Text == "")
                {
                    new LinkNoteAlertDialog(this).CreateShow(Resource.String.LoginErrorTitle,
                        Resource.String.LoginNameSecondErrorMessage, null);
                }
                else if (txtUserPassWord.Text == "")
                {
                    new LinkNoteAlertDialog(this).CreateShow(Resource.String.LoginErrorTitle,
                        Resource.String.LoginPassWordSecondErrorMessage, null);
                }
                else
                {
                    // 验证用户名和密码
                }
            };
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