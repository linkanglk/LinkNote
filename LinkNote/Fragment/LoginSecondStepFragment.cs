using Android.OS;
using Android.Views;

namespace LinkNote.Fragment
{
    public class LoginSecondStepFragment : Android.Support.V4.App.Fragment
    {
        public delegate void CreateViewCallBack(View view);
        CreateViewCallBack createViewCallBack;

        public LoginSecondStepFragment(CreateViewCallBack createViewCallBack)
        {
            this.createViewCallBack = createViewCallBack;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var v = inflater.Inflate(
                Resource.Layout.LoginSecondStepPage, container, false);
            createViewCallBack(v);
            return v;
        }
    }
}