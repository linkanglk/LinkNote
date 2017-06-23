using Android.OS;
using Android.Views;
using Android.Widget;
using LinkNote.OtherClass;

namespace LinkNote.Fragment
{
    public class LoginFristStepFragment: Android.Support.V4.App.Fragment
    {
        public delegate void CreateViewCallBack(View view);
        CreateViewCallBack createViewCallBack;

        public LoginFristStepFragment(CreateViewCallBack createViewCallBack)
        {
            this.createViewCallBack = createViewCallBack;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var v = inflater.Inflate(
                Resource.Layout.LoginFristStepPage, container, false);
            createViewCallBack(v);
            return v;
        }
    }
}