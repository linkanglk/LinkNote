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
using Android.Util;

namespace LinkNote.ViewPager
{
    public class LinkNoteViewPager : Android.Support.V4.View.ViewPager
    {
        int fragmentIndex = 0;

        public LinkNoteViewPager(Context context) : base(context) { }

        public LinkNoteViewPager(Context context, IAttributeSet attrs) : base(context, attrs) { }

        public void SetFragmentIndex(int fragmentIndex)
        {
            this.fragmentIndex = fragmentIndex;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (fragmentIndex == 0)
                return false;
            else
                return base.OnTouchEvent(e);
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (fragmentIndex == 0)
                return false;
            else
                return base.OnInterceptTouchEvent(ev);
        }
    }
}