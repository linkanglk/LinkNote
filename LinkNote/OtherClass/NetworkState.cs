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
using Android.Net;

namespace LinkNote.OtherClass
{
    public class NetworkState
    {
        Context _context;

        public NetworkState(Context _context)
        {
            this._context = _context;
        }

        public bool Check()
        {
            bool flag = false;
            //得到网络连接信息  
            ConnectivityManager manager = (ConnectivityManager)_context.GetSystemService(Context.ConnectivityService);
            //去进行判断网络是否连接  
            if (manager.ActiveNetworkInfo != null)
            {
                flag = manager.ActiveNetworkInfo.IsAvailable;
            }
            return flag;
        }
    }
}