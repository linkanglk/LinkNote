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
using Android.Graphics;
using Android.Util;

namespace LinkNote.OtherClass
{
    public class LinkNoteLoadingDialog
    {
        Context context;
        ProgressDialog progressDialog;

        public LinkNoteLoadingDialog(Context context)
        {
            this.context = context;
        }

        public void CreateShow()
        {
            progressDialog = new ProgressDialog(context);
            progressDialog.SetMessage("请稍候...");
            progressDialog.SetCancelable(false);
            progressDialog.Show();
            TextView txtMessage = (TextView)progressDialog.FindViewById(Android.Resource.Id.Message);
            txtMessage.SetTextColor(Color.Rgb(123, 123, 123));
            txtMessage.SetTextSize(ComplexUnitType.Dip, 13);
        }

        public void DestroyedOff()
        {
            progressDialog.Dismiss();
        }
    }
}