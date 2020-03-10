using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Manager.Api.Response;
using Manager.Fragments;
using Manager.Listener;
using Square.Picasso;

namespace Manager.Adapter
{
    public class SupportAdapter : RecyclerView.Adapter, IItemClickListener
    {
        public List<SupportResponse> data { get; set; }
        private Context context;
        public IRecycleViewOnItemClickListener recycleViewOnItemClickListener;

        public void SetRecycleViewOnItemClickListener(IRecycleViewOnItemClickListener recycleViewOnItemClickListener)
        {
            this.recycleViewOnItemClickListener = recycleViewOnItemClickListener;
        }

        public SupportAdapter(List<SupportResponse> data, Context context)
        {
            this.data = data;
            this.context = context;
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
            SupportViewHolder viewHolder = holder as SupportViewHolder;
            viewHolder.supportNumber.Text = (position + 1).ToString();
            viewHolder.supportResident.Text = data[position].ResidentName;
            Picasso.With(context)
           .Load(data[position].ResidentImage)
           .Resize(90, 90)
           .Into(viewHolder.residentSupportImage);

            viewHolder.SetItemClickListener(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.custom_support, parent, false);
            return new SupportViewHolder(view);
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

    public class SupportViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        // https://www.youtube.com/watch?v=CN66PE1j7yw
        private IItemClickListener itemClickListener;


        public TextView supportNumber { get; set; }
        public TextView supportResident { get; set; }
        public ImageView residentSupportImage { get; set; }


        public SupportViewHolder(View itemView) : base(itemView)
        {
            supportNumber = (TextView)itemView.FindViewById(Resource.Id.tv_support_number);
            supportResident = (TextView)itemView.FindViewById(Resource.Id.tv_support_resident);
            residentSupportImage = (ImageView)itemView.FindViewById(Resource.Id.img_resident_support);
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