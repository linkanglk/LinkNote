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
using Java.Lang;
using LinkNote.Model.CustomizeNavigationView;

namespace LinkNote.Adapter.CustomizeNavigationViewAdapter
{
    public class ListViewAdapter : BaseAdapter
    {
        const int TYPE_DIVIDER = 0;
        const int TYPE_NORMAL = 1;
        const int TYPE_HEADER = 2;

        private List<DrawerItem> dataList = new List<DrawerItem>() {
            new DrawerItemHeader(),
            new DrawerItemNormal(Resource.Drawable.ic_dashboard, Resource.String.drawer_menu_home),
            new DrawerItemNormal(Resource.Drawable.ic_event, Resource.String.drawer_menu_rank),
            new DrawerItemNormal(Resource.Drawable.ic_forum, Resource.String.drawer_menu_column),
            new DrawerItemNormal(Resource.Drawable.ic_headset, Resource.String.drawer_menu_search),
            new DrawerItemNormal(Resource.Drawable.ic_update, Resource.String.drawer_menu_trash),
            new DrawerItemNormal(Resource.Drawable.upgrade, Resource.String.drawer_menu_night),
            new DrawerItemNormal(Resource.Drawable.ic_update, Resource.String.drawer_menu_setting)
        };

        private Context context;

        public ListViewAdapter(Context context)
        {
            this.context = context;
        }

        public override int Count
        {
            get
            {
                return (dataList == null || dataList.Count() == 0) ? 0 : dataList.Count();
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            DrawerItem item = dataList[position];
            DrawerViewHolder holder = null;
            if (convertView == null)
            {
                if (item is DrawerItemHeader) // 头部
                {
                    HeaderViewHolder headerHolder = new HeaderViewHolder(context);
                    convertView = LayoutInflater.From(context)
                                .Inflate(Resource.Layout.CustomizeNavigationViewItemDrawerHeader, null);
                    headerHolder.sdv_icon = (ImageView)convertView.FindViewById(Resource.Id.sdv_icon);
                    headerHolder.tv_login = (TextView)convertView.FindViewById(Resource.Id.tv_login);
                    holder = headerHolder;
                }
                else if (item is DrawerItemNormal) // 选项
                {
                    NormalViewHolder normalHolder = new NormalViewHolder(context);
                    convertView = LayoutInflater.From(context)
                                .Inflate(Resource.Layout.CustomizeNavigationViewItemDrawerNormal, null);
                    normalHolder.tv = (TextView)convertView.FindViewById(Resource.Id.tv);
                    normalHolder.iv = (ImageView)convertView.FindViewById(Resource.Id.iv);
                    holder = normalHolder;
                }
                convertView.Tag = holder;
            }
            else
            {
                if (item is DrawerItemHeader) // 头部
                {
                    holder = (HeaderViewHolder)convertView.Tag;
                }
                else if (item is DrawerItemNormal) // 选项
                {
                    holder = (NormalViewHolder)convertView.Tag;
                }
            }

            if (item is DrawerItemHeader) // 头部
            {
                HeaderViewHolder headerHolder = (HeaderViewHolder)holder;
                DrawerItemHeader headerItem = (DrawerItemHeader)item;
            }
            else if (item is DrawerItemNormal) // 选项
            {
                NormalViewHolder normalHolder = (NormalViewHolder)holder;
                DrawerItemNormal normalItem = (DrawerItemNormal)item;
                normalHolder.tv.SetText(normalItem.titleRes);
                normalHolder.iv.SetBackgroundResource(normalItem.iconRes);
            }
            return convertView;
        }
    }
}