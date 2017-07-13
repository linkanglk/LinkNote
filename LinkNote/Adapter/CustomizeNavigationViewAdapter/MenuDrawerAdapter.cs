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

        const int TYPE_CONTENT = 0;
        const int TYPE_UPGRADE = 1;
        const int TYPE_SYNCHRONIZE = 2;
        const int TYPE_DIVIDER = 3;

        private List<MenuDrawerItem> dataList;
        int height;

        public MenuDrawerAdapter(List<MenuDrawerItem> dataList,int height)
        {
            this.dataList = dataList;
            this.height = height;
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
            if (menuDrawerItem is MenuDrawerItemContent)
            {
                return TYPE_CONTENT;
            }
            else if (menuDrawerItem is MenuDrawerItemUpgrade)
            {
                return TYPE_UPGRADE;
            }
            else if (menuDrawerItem is MenuDrawerItemSynchronize)
            {
                return TYPE_SYNCHRONIZE;
            }
            else if (menuDrawerItem is MenuDrawerItemDivider)
            {
                return TYPE_DIVIDER;
            }
            return base.GetItemViewType(position);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MenuDrawerItem item = dataList[position];
            if (holder is ContentViewHolder) // 内容
            {
                ContentViewHolder contentViewHolder = (ContentViewHolder)holder;
                MenuDrawerItemContent itemContent = (MenuDrawerItemContent)item;
                contentViewHolder.listView.Adapter = itemContent.adapter;

                ViewGroup.LayoutParams lp;
                lp = contentViewHolder.rootView.LayoutParameters;
                lp.Height = height - 47 * 9;
                contentViewHolder.rootView.LayoutParameters = lp;
            }
            else if (holder is UpgradeViewHolder) // 升级账号
            {
                UpgradeViewHolder upgradeViewHolder = (UpgradeViewHolder)holder;
                MenuDrawerItemUpgrade itemUpgrade = (MenuDrawerItemUpgrade)item;
                upgradeViewHolder.uiv.SetBackgroundResource(itemUpgrade.iconRes);
                upgradeViewHolder.utv.SetText(itemUpgrade.titleRes);
                // 点击
                upgradeViewHolder.view.SetOnClickListener(new UpgradeViewClick(this, itemUpgrade));
            }
            else if (holder is SynchronizeViewHolder) // 同步
            {
                SynchronizeViewHolder synchronizeViewHolder = (SynchronizeViewHolder)holder;
                if (item is MenuDrawerItemSynchronize)
                {
                    MenuDrawerItemSynchronize itemSynchronize = (MenuDrawerItemSynchronize)item;
                    synchronizeViewHolder.siv.SetBackgroundResource(itemSynchronize.iconRes);
                    synchronizeViewHolder.stv.SetText(itemSynchronize.titleRes);
                    // 点击
                    synchronizeViewHolder.view.SetOnClickListener(new SynchronizeViewClick(this, itemSynchronize));
                }
            }
        }

        #region 升级账号点击事件

        public class UpgradeViewClick : Java.Lang.Object, View.IOnClickListener
        {
            MenuDrawerAdapter _this;
            MenuDrawerItemUpgrade itemUpgrade;

            public UpgradeViewClick(MenuDrawerAdapter _this, MenuDrawerItemUpgrade itemUpgrade)
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
            void itemClick(MenuDrawerItemUpgrade drawerItemNormal);
        }

        #endregion

        #region 同步点击事件

        public class SynchronizeViewClick : Java.Lang.Object, View.IOnClickListener
        {
            MenuDrawerAdapter _this;
            MenuDrawerItemSynchronize itemSynchronize;

            public SynchronizeViewClick(MenuDrawerAdapter _this, MenuDrawerItemSynchronize itemSynchronize)
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
            void itemClick(MenuDrawerItemSynchronize drawerItemNormal);
        }

        #endregion


        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            MenuDrawerViewHolder viewHolder = null;
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            switch (viewType)
            {
                case TYPE_CONTENT: // 内容
                    viewHolder = new ContentViewHolder(inflater
                        .Inflate(Resource.Layout
                        .CustomizeNavigationViewItemDrawerContent, parent, false));
                    break;
                case TYPE_UPGRADE: // 升级
                    viewHolder = new UpgradeViewHolder(inflater
                        .Inflate(Resource.Layout
                        .CustomizeNavigationViewItemDrawerUpgrade, parent, false));
                    break;
                case TYPE_SYNCHRONIZE: // 同步
                    viewHolder = new SynchronizeViewHolder(inflater
                        .Inflate(Resource.Layout
                        .CustomizeNavigationViewItemDrawerSynchronize, parent, false));
                    break;
                case TYPE_DIVIDER: // 分割线
                    viewHolder = new SynchronizeViewHolder(inflater
                        .Inflate(Resource.Layout
                        .CustomizeNavigationViewItemDrawerDivider, parent, false));
                    break;
            }
            return viewHolder;
        }
    }
}