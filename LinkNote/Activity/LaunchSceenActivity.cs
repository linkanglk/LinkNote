using Android.App;
using Android.Content;
using Android.OS;
using LinkNote.Activity;

/// <summary>
/// 程序启动页
/// </summary>

namespace LinkNote.Activitys
{
    [Activity(Label = "链接笔记", Icon = "@drawable/icon", MainLauncher = true, Theme = "@style/LaunchScreen")]
    public class LaunchSceenActivity : Android.App.Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LaunchSceen); // 设置启动layout
            InitView(); // 初始化页面
        }

        private void InitView()
        {
            Waitting(); // 页面加载等待
        }

        private void Waitting()
        {
            new Handler().PostDelayed(delegate
            {
                Intent i = new Intent();
                i.SetClass(this, typeof(LoginMainActivity));
                StartActivity(i);
                OverridePendingTransition(Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out); // 淡入淡出效果
                Finish();
            }, 2000); // 进入页面前需要等待两秒
        }
    }
}