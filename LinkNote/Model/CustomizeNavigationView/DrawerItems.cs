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
using Android.Support.V7.Widget;

namespace LinkNote.Model.CustomizeNavigationView
{

    // -----------------------------item数据模型-----------------------------

    /// <summary>
    /// drawerlayout item统一的数据模型
    /// </summary>
    public interface DrawerItem
    {

    }

    /// <summary>
    /// 有图片和文字的item
    /// </summary>
    public class DrawerItemNormal : DrawerItem
    {
        public int iconRes;
        public int titleRes;

        public DrawerItemNormal(int iconRes, int titleRes)
        {
            this.iconRes = iconRes;
            this.titleRes = titleRes;
        }
    }


    /// <summary>
    /// 头部item
    /// </summary>
    public class DrawerItemHeader : DrawerItem
    {
        public DrawerItemHeader()
        {
        }
    }


    /// <summary>
    /// 首页菜单
    /// </summary>
    public interface MenuDrawerItem
    {

    }

    /// <summary>
    /// 选择内容
    /// </summary>
    public class MenuDrawerItemContent : MenuDrawerItem
    {
        public BaseAdapter adapter;

        public MenuDrawerItemContent(BaseAdapter adapter)
        {
            this.adapter = adapter;
        }
    }

    

    /// <summary>
    /// 分割线item
    /// </summary>
    public class MenuDrawerItemDivider : MenuDrawerItem
    {
        public MenuDrawerItemDivider()
        {
        }
    }

    /// <summary>
    /// 升级账号
    /// </summary>
    public class MenuDrawerItemUpgrade : MenuDrawerItem
    {
        public int iconRes;
        public int titleRes;

        public MenuDrawerItemUpgrade(int iconRes, int titleRes)
        {
            this.iconRes = iconRes;
            this.titleRes = titleRes;
        }
    }

    /// <summary>
    /// 同步账号
    /// </summary>
    public class MenuDrawerItemSynchronize : MenuDrawerItem
    {
        public int iconRes;
        public int titleRes;

        public MenuDrawerItemSynchronize(int iconRes, int titleRes)
        {
            this.iconRes = iconRes;
            this.titleRes = titleRes;
        }
    }

    // -----------------------------ViewHolder数据模型-----------------------------


    public class MenuDrawerViewHolder : RecyclerView.ViewHolder
    {
        public MenuDrawerViewHolder(View itemView) : base(itemView)
        {

        }
    }

    public class DividerViewHolder : MenuDrawerViewHolder
    {
        public DividerViewHolder(View itemView) : base(itemView)
        {

        }
    }

    /// <summary>
    /// 内容
    /// </summary>
    public class ContentViewHolder : MenuDrawerViewHolder
    {
        public ListView listView;
        public LinearLayout rootView;

        public ContentViewHolder(View itemView) : base(itemView)
        {
            listView = (ListView)itemView.FindViewById(Resource.Id.listViewMenu);
            rootView = (LinearLayout)itemView;
        }
    }

    public class UpgradeViewHolder : MenuDrawerViewHolder
    {
        public View view;
        public TextView utv;
        public ImageView uiv;

        public UpgradeViewHolder(View itemView) : base(itemView)
        {
            view = itemView;
            utv = (TextView)itemView.FindViewById(Resource.Id.utv);
            uiv = (ImageView)itemView.FindViewById(Resource.Id.uiv);
        }
    }

    public class SynchronizeViewHolder : MenuDrawerViewHolder
    {
        public View view;
        public TextView stv;
        public ImageView siv;

        public SynchronizeViewHolder(View itemView) : base(itemView)
        {
            view = itemView;
            stv = (TextView)itemView.FindViewById(Resource.Id.stv);
            siv = (ImageView)itemView.FindViewById(Resource.Id.siv);
        }
    }


    /// <summary>
    /// 抽屉ViewHolder模型
    /// </summary>
    public class DrawerViewHolder : View
    {
        public DrawerViewHolder(Context context) : base(context)
        {
        }
    }

    /// <summary>
    /// 有图标有文字ViewHolder
    /// </summary>
    public class NormalViewHolder : DrawerViewHolder
    {
        public TextView tv;
        public ImageView iv;

        public NormalViewHolder(Context context) : base(context)
        {

        }
    }

    public class HeaderViewHolder: DrawerViewHolder
    {
        public ImageView sdv_icon;
        public TextView tv_login;

        public HeaderViewHolder(Context context) : base(context)
        {

        }
    }
}