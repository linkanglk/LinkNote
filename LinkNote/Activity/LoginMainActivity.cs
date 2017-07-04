using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Graphics;
using Android.Util;
using LinkNote.OtherClass;
using LinkNote.ViewPager;
using LinkNote.Adapter.ViewPagerAdapter;
using Android.Support.V7.App;
using LinkNote.Fragment;
using System.Text.RegularExpressions;

namespace LinkNote.Activity
{
    [Activity(Label = "LoginMainActivity")]
    public class LoginMainActivity : AppCompatActivity
    {
        public static LoginMainActivity _context;
        public RelativeLayout header;
        LinkNoteViewPager vpBody;
        int fragmentIndex = 0;

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
            fragmentIndex = e.Position;
            vpBody.SetFragmentIndex(e.Position);
        }

        public override void OnBackPressed()
        {
            if (fragmentIndex == 1)
                vpBody.SetCurrentItem(0, true);
            else
                Finish();
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
                        // 可以验证用户是否存在，
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
            LinearLayout lyCreateAccount = (LinearLayout)secondView.FindViewById(Resource.Id.lyCreateAccount);
            LinearLayout lyLoginContent = (LinearLayout)secondView.FindViewById(Resource.Id.lyLoginContent);
            LinearLayout lyRegisterContent = (LinearLayout)secondView.FindViewById(Resource.Id.lyRegisterContent);
            bool check = true;
            btnLogin.Click += delegate
            {
                if (!new NetworkState(this).Check())
                {
                    new LinkNoteAlertDialog(this).CreateShow(Resource.String.LoginErrorTitle,
                        Resource.String.NetworkStateErrorMessage, null);
                    return;
                }
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
                    Intent intentMain = new Intent();
                    intentMain.SetClass(this, typeof(MainActivity));
                    StartActivity(intentMain);
                }
            };
            lyCreateAccount.Click += delegate
            {
                // 跳转到注册页面
                lyLoginContent.Visibility = ViewStates.Gone;
                lyRegisterContent.Visibility = ViewStates.Visible;
            };
            TextView txtForgetPassWord = (TextView)secondView.FindViewById(Resource.Id.txtForgetPassWord);
            txtForgetPassWord.Click += delegate
            {
                // 跳转忘记密码页面
                Intent intentForgetPassWord = new Intent();
                intentForgetPassWord.SetClass(this, typeof(ForgetPassWordActivity));
                StartActivity(intentForgetPassWord);
                OverridePendingTransition(Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out); // 淡入淡出效果
            };
            TextView txtChangeVersion = (TextView)secondView.FindViewById(Resource.Id.txtChangeVersion);
            txtChangeVersion.Click += delegate
            {
                // 改变版本
                if (check)
                {
                    txtChangeVersion.SetText(Resource.String.LoginChangeNormal);
                    check = false;
                }
                else
                {
                    txtChangeVersion.SetText(Resource.String.LoginChangeInternational);
                    check = true;
                }
            };
            LinearLayout lyToLogin = (LinearLayout)secondView.FindViewById(Resource.Id.lyToLogin);
            lyToLogin.Click += delegate
            {
                // 跳转登录页面
                lyRegisterContent.Visibility = ViewStates.Gone;
                lyLoginContent.Visibility = ViewStates.Visible;
            };
            Button btnRegister = (Button)secondView.FindViewById(Resource.Id.btnRegister);
            btnRegister.Click += delegate
            {
                if (!new NetworkState(this).Check())
                {
                    new LinkNoteAlertDialog(this).CreateShow(Resource.String.RegisterErrorTitle,
                        Resource.String.NetworkStateErrorMessage, null);
                    return;
                }
                EditText txtUserEmail = (EditText)secondView.FindViewById(Resource.Id.txtUserEmail);
                EditText txtUserRegisterPassWord = (EditText)secondView.FindViewById(Resource.Id.txtUserRegisterPassWord);
                Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
                if (txtUserEmail.Text == "")
                {
                    new LinkNoteAlertDialog(this).CreateShow(Resource.String.RegisterErrorTitle, 
                        Resource.String.RegisterEmailNullMessage, null);
                }
                else if (!r.IsMatch(txtUserEmail.Text))
                {
                    new LinkNoteAlertDialog(this).CreateShow(Resource.String.RegisterErrorTitle,
                        Resource.String.RegisterEmailErrorMessage, null);
                }
                else if (txtUserRegisterPassWord.Text == "")
                {
                    new LinkNoteAlertDialog(this).CreateShow(Resource.String.RegisterErrorTitle,
                        Resource.String.RegisterPassWordNullMessage, null);
                }
                else if (txtUserRegisterPassWord.Text.Length < 6)
                {
                    new LinkNoteAlertDialog(this).CreateShow(Resource.String.RegisterErrorTitle,
                        Resource.String.RegisterPassWordErrorMessage, null);
                }
                else
                {
                    // 注册用户
                }
            };
        }

        public void ShowSnack()
        {
            Snackbar.Make(vpBody,
                Resource.String.ForgetPassWordPromptMessage, Snackbar.LengthShort).Show();
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