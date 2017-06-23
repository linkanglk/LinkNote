using Android.OS;
using Android.Views;

namespace LinkNote.Fragment
{
    public class LoginSecondStepFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var v = inflater.Inflate(
                Resource.Layout.LoginSecondStepPage, container, false);
            return v;
        }
    }
}