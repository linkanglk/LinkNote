

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Widget;

namespace LinkNote.OtherClass
{
    public class LinkNoteAlertDialog
    {
        Context _context;
        public delegate void ClickCallBack();

        public LinkNoteAlertDialog(Context _context)
        {
            this._context = _context;
        }

        public AlertDialog CreateShow(string title, string message, ClickCallBack clickCallBack)
        {
            AlertDialog alert = new AlertDialog.Builder(_context).SetTitle(title)
                    .SetMessage(message).SetNegativeButton(Resource.String.OneButtonText, delegate
                    {
                        if (clickCallBack != null)
                            clickCallBack();
                    }).Show();
            TextView txtMessage = (TextView)alert.FindViewById(Android.Resource.Id.Message); // alertDialog 的显示内容
            txtMessage.SetTextColor(Color.Rgb(123, 123, 123));
            return alert;
        }

        public AlertDialog CreateShow(int title, int message, ClickCallBack clickCallBack)
        {
            AlertDialog alert = new AlertDialog.Builder(_context).SetTitle(title)
                    .SetMessage(message).SetNegativeButton(Resource.String.OneButtonText, delegate
                    {
                        if (clickCallBack != null)
                            clickCallBack();
                    }).Show();
            TextView txtMessage = (TextView)alert.FindViewById(Android.Resource.Id.Message); // alertDialog 的显示内容
            txtMessage.SetTextColor(Color.Rgb(123, 123, 123));
            return alert;
        }
    }
}