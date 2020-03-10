using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AbcPlaza.Api.Response;
using AbcPlaza.Constant;
using AbcPlaza.Listener;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Square.Picasso;

namespace AbcPlaza.Adapter
{
    public class EquipmentAdapter : RecyclerView.Adapter, IItemClickListener
    {
        public IRecycleViewOnItemClickListener recycleViewOnItemClickListener;

        public void SetRecycleViewOnItemClickListener(IRecycleViewOnItemClickListener recycleViewOnItemClickListener)
        {
            this.recycleViewOnItemClickListener = recycleViewOnItemClickListener;
        }
        public List<EquipmentResponse> data { get; set; }
        private Context mCtx;


        public EquipmentAdapter(List<EquipmentResponse> data, Context mCtx)
        {
            this.data = data;
            this.mCtx = mCtx;
        }

        public EquipmentAdapter()
        {
        }

        public override int ItemCount
        {
            get
            {
                return data.Count;
            }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolder viewHolder = holder as ViewHolder;
            viewHolder.tv_Name.Text = data[position].EquipmentName;
            viewHolder.tv_Purchase.Text = data[position].PurchaseDate;
            viewHolder.tv_Expiration.Text = data[position].WarrantyPeriod.ToString();
            Picasso.With(mCtx)
           .Load(data[position].EquipmentImage)
           .Resize(90, 90)
           .Into(viewHolder.img_abc);

            viewHolder.buttonOptions_abc.Click += (sender, e) =>
            {
                Android.Support.V7.Widget.PopupMenu popup = new Android.Support.V7.Widget.PopupMenu(mCtx, viewHolder.buttonOptions_abc);
                popup.Inflate(Resource.Menu.popup_menu);
                popup.SetOnMenuItemClickListener(new MyOnMenuItemClickListener(this, position, mCtx));
                popup.Show();
            };
            viewHolder.SetItemClickListener(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.adapter_equipment, parent, false);
            return new ViewHolder(view);
        }

        public void OnClick(View view, int position)
        {
            if (recycleViewOnItemClickListener != null)
            {
                recycleViewOnItemClickListener.OnClick(view, position);
            }
        }

        public void OnLongCLick(View view, int position)
        {
            if (recycleViewOnItemClickListener != null)
            {
                recycleViewOnItemClickListener.OnLongClick(view, position);
            }

        }
    }

    public class MyOnMenuItemClickListener : Java.Lang.Object, Android.Support.V7.Widget.PopupMenu.IOnMenuItemClickListener
    {
        readonly EquipmentAdapter adapter;
        private int position;
        private Context _mCtx;

        public MyOnMenuItemClickListener(EquipmentAdapter adapter, int position, Context mCtx)
        {
            this.adapter = adapter;
            this.position = position;
            _mCtx = mCtx;
        }

        public bool OnMenuItemClick(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_delete:
                    {
                        try
                        {
                            string id = adapter.data.ElementAt(position).Id;
                            HttpClient client = new HttpClient();
                            string url = Url.BASE_URL + "Equipment/" + id;                         
                            var uri = new Uri(url);
                            Task<HttpResponseMessage> message = client.DeleteAsync(uri);
                            if (message.Result.IsSuccessStatusCode)
                            {
                                adapter.data.Remove(adapter.data[position]);
                                adapter.NotifyItemRemoved(position);
                                adapter.NotifyDataSetChanged();
                            }
                            else
                            {
                                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(_mCtx);
                                Android.App.AlertDialog alert = dialog.Create();
                                alert.SetTitle("Thông báo");
                                alert.SetMessage("Xóa thiết bị thất bại");
                                alert.SetButton("OK", (c, ev) =>
                                {
                                    // Ok button click task  
                                });
                                alert.SetButton2("CANCEL", (c, ev) => { });
                                alert.Show();
                            }
                        }
                        catch (Exception ex)
                        {
                            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(_mCtx);
                            Android.App.AlertDialog alert = dialog.Create();
                            alert.SetTitle("Thông báo");
                            alert.SetMessage("Xóa thiết bị thất bại");
                            alert.SetButton("OK", (c, ev) =>
                            {
                                // Ok button click task  
                            });
                            alert.SetButton2("CANCEL", (c, ev) => { });
                            alert.Show();
                        }
                        break;
                    }
            }
            return false;
        }
    }

    public class ViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        // https://www.youtube.com/watch?v=CN66PE1j7yw
        private IItemClickListener itemClickListener;


        public TextView tv_Name { get; set; }
        public TextView tv_Purchase { get; set; }
        public TextView tv_Expiration { get; set; }
        public ImageView img_abc { get; set; }
        public Button buttonOptions_abc { get; set; }


        public ViewHolder(View itemView) : base(itemView)
        {
            tv_Name = (TextView)itemView.FindViewById(Resource.Id.tv_name);
            tv_Purchase = (TextView)itemView.FindViewById(Resource.Id.tv_purchase);
            tv_Expiration = (TextView)itemView.FindViewById(Resource.Id.tv_expiration);
            img_abc = (ImageView)itemView.FindViewById(Resource.Id.img);
            buttonOptions_abc = (Button)itemView.FindViewById(Resource.Id.buttonOptions);
            itemView.SetOnClickListener(this);
            itemView.SetOnLongClickListener(this);

        }

        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }

        public void OnClick(View v)
        {
            itemClickListener.OnClick(v, AdapterPosition);
        }

        public bool OnLongClick(View v)
        {
            itemClickListener.OnLongCLick(v, AdapterPosition);
            return true;
        }
    }
}