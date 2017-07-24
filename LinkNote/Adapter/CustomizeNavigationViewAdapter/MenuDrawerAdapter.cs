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
using LinkNote.Model.CustomizeNavigationView;

namespace LinkNote.Adapter.CustomizeNavigationViewAdapter
{

    public class MenuDrawerAdapter : RecyclerView.Adapter
    {

        const int TYPE_BODY = 0;
        const int TYPE_CONTENT = 1;

        private List<MenuDrawerItem> dataList;

        public MenuDrawerAdapter(List<MenuDrawerItem> dataList,int height)
        {
            this.dataList = dataList;
        }

        public override int ItemCount
        {
            get
            {
                return dataList.Count;
            }
        }

        public override int GetItemViewType(int position)
        {
            MenuDrawerItem menuDrawerItem = dataList[position];
            if (menuDrawerItem is MenuDrawerItemBody)
            {
                return TYPE_BODY;
            }
            return base.GetItemViewType(position);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MenuDrawerItem item = dataList[position];
            // 菜单主体内容
            if (holder is BodyViewHolder)
            {
                BodyViewHolder bodyViewHolder = (BodyViewHolder)holder;
                MenuDrawerItemBody itemBody = (MenuDrawerItemBody)item;
                bodyViewHolder.listView.Adapter = itemBody.adapter;
                bodyViewHolder.synchronizeView.SetOnClickListener(new SynchronizeViewClick(this, itemBody));
                bodyViewHolder.upgradeView.SetOnClickListener(new UpgradeViewClick(this, itemBody));
            }
        }

        #region 升级账号点击事件

        public class UpgradeViewClick : Java.Lang.Object, View.IOnClickListener
        {
            MenuDrawerAdapter _this;
            MenuDrawerItemBody itemUpgrade;

            public UpgradeViewClick(MenuDrawerAdapter _this, MenuDrawerItemBody itemUpgrade)
            {
                this._this = _this;
                this.itemUpgrade = itemUpgrade;
            }

            public void OnClick(View v)
            {
                if (_this.listener != null)
                {
                    _this.listener.itemClick(itemUpgrade);
                }
            }
        }

        public OnItemClickListener listener;

        public void SetOnItemClickListener(OnItemClickListener listener)
        {
            this.listener = listener;
        }

        public interface OnItemClickListener
        {
            void itemClick(MenuDrawerItemBody drawerItemNormal);
        }

        #endregion

        #region 同步点击事件

        public class SynchronizeViewClick : Java.Lang.Object, View.IOnClickListener
        {
            MenuDrawerAdapter _this;
            MenuDrawerItemBody itemSynchronize;

            public SynchronizeViewClick(MenuDrawerAdapter _this, MenuDrawerItemBody itemSynchronize)
            {
                this._this = _this;
                this.itemSynchronize = itemSynchronize;
            }

            public void OnClick(View v)
            {
                if (_this.listenerSynchronize != null)
                {
                    _this.listenerSynchronize.itemClick(itemSynchronize);
                }
            }
        }

        public OnSynchronizeItemClickListener listenerSynchronize;

        public void SetSynchronizeOnItemClickListener(OnSynchronizeItemClickListener listenerSynchronize)
        {
            this.listenerSynchronize = listenerSynchronize;
        }

        public interface OnSynchronizeItemClickListener
        {
            void itemClick(MenuDrawerItemBody drawerItemNormal);
        }

        #endregion

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            MenuDrawerViewHolder viewHolder = null;
            LayoutInflater inflater = LayoutInflater.From(parent.Context);

            switch (viewType)
            {
                case TYPE_BODY: // 内容
                    viewHolder = new BodyViewHolder(inflater
                                .Inflate(Resource.Layout
                                .CustomizeNavigationViewItemDrawer, parent, false));
                    break;
            }
            return viewHolder;
        }
    }
}