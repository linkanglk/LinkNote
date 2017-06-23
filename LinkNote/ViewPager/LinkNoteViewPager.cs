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
        bool disabledScroll = true;

        public LinkNoteViewPager(Context context) : base(context) { }

        public LinkNoteViewPager(Context context, IAttributeSet attrs) : base(context, attrs) { }

        public void DisabledScroll(bool disabled)
        {
            this.disabledScroll = disabled;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (disabledScroll)
                return false;
            else
                return base.OnTouchEvent(e);
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            switch (ev.Action)
            {
                case MotionEventActions.Down:
                    // 向下滑动禁止
                    break;
                case MotionEventActions.Up:
                    disabledScroll = false; // 向上滑动，允许
                    break;
                default:
                    break;
            }
            if (disabledScroll)
                return false;
            else
                return base.OnInterceptTouchEvent(ev);
        }
    }
}