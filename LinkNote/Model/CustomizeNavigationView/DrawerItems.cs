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
    /// 菜单
    /// </summary>
    public class MenuDrawerItemBody : MenuDrawerItem
    {
        public BaseAdapter adapter;

        public MenuDrawerItemBody(BaseAdapter adapter)
        {
            this.adapter = adapter;
        }
    }

    // -----------------------------ViewHolder数据模型-----------------------------

    public class MenuDrawerViewHolder : RecyclerView.ViewHolder
    {
        public MenuDrawerViewHolder(View itemView) : base(itemView)
        {

        }
    }

    /// <summary>
    /// 主体
    /// </summary>
    public class BodyViewHolder : MenuDrawerViewHolder
    {
        // 菜单list内容信息
        public ListView listView;
        
        // 升级账号菜单
        public View upgradeView;
        public TextView upgradetv;
        public ImageView upgradeiv;

        // 同步内容
        public View synchronizeView;
        public TextView synchronizetv;
        public ImageView synchronizeiv;

        public BodyViewHolder(View itemView) : base(itemView)
        {
            listView = (ListView)itemView.FindViewById(Resource.Id.listViewMenu);

            upgradeView = (LinearLayout)itemView.FindViewById(Resource.Id.UpgradeItemView);
            upgradetv = (TextView)itemView.FindViewById(Resource.Id.utv);
            upgradeiv = (ImageView)itemView.FindViewById(Resource.Id.uiv);

            synchronizeView = (LinearLayout)itemView.FindViewById(Resource.Id.SynchronizeItemView);
            synchronizetv = (TextView)itemView.FindViewById(Resource.Id.stv);
            synchronizeiv = (ImageView)itemView.FindViewById(Resource.Id.siv);
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