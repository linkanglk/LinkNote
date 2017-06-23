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
using Android.Support.V7.App;
using Android.Transitions;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using LinkNote.OtherClass;
using Android.Graphics;

namespace LinkNote.Activity
{
    [Activity(Label = "ForgetPassWordActivity")]
    public class ForgetPassWordActivity : AppCompatActivity
    {
        public const string EXTRA_NAME = "note_name";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ForgetPassWord);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetTitle(Resource.String.ForgetPassWordActionBarTitle);

            Button btnResetPassWord = (Button)FindViewById(Resource.Id.btnResetPassWord);
            btnResetPassWord.Click += BtnResetPassWord_Click;
        }

        private void BtnResetPassWord_Click(object sender, EventArgs e)
        {
            LoginMainActivity._context.ShowSnack();
            Finish();
            OverridePendingTransition(Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out); // 淡入淡出效果
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    OverridePendingTransition(Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out); // 淡入淡出效果
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            Finish();
            OverridePendingTransition(Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out); // 淡入淡出效果
        }
    }
}