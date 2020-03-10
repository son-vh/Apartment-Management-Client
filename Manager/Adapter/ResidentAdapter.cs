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
    public class ResidentAdapter : RecyclerView.Adapter, IItemClickListener
    {
        public List<ResidentResponse> data { get; set; }
        private Context context;
        public IRecycleViewOnItemClickListener recycleViewOnItemClickListener;

        public void SetRecycleViewOnItemClickListener(IRecycleViewOnItemClickListener recycleViewOnItemClickListener)
        {
            this.recycleViewOnItemClickListener = recycleViewOnItemClickListener;
        }

        public ResidentAdapter(List<ResidentResponse> data, Context context)
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
            ViewHolder viewHolder = holder as ViewHolder;
            viewHolder.residentName.Text = data[position].ResidentName;
            viewHolder.room.Text = data[position].Room;
            viewHolder.floor.Text = data[position].Floor;
             Picasso.With(context)
            .Load(data[position].ResidentImage)
            .Resize(90, 90)
            .Into(viewHolder.residentImage);

            viewHolder.SetItemClickListener(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.custom_resident, parent, false);
            return new ViewHolder(view);
        }

        public void OnClick(View view, int position)
        {
            if(recycleViewOnItemClickListener != null)
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

    public class ViewHolder : RecyclerView.ViewHolder,View.IOnClickListener, View.IOnLongClickListener
    {
        // https://www.youtube.com/watch?v=CN66PE1j7yw
        private IItemClickListener itemClickListener;


        public TextView residentName { get; set; }
        public TextView room { get; set; }
        public TextView floor { get; set; }
        public ImageView residentImage { get; set; }
        public Button residentHandler { get; set; }


        public ViewHolder(View itemView) : base(itemView)
        {
            residentName = (TextView)itemView.FindViewById(Resource.Id.tv_resident_name);
            room = (TextView)itemView.FindViewById(Resource.Id.tv_room);
            floor = (TextView)itemView.FindViewById(Resource.Id.tv_floor);
            residentImage = (ImageView)itemView.FindViewById(Resource.Id.img_resident);
            residentHandler = (Button)itemView.FindViewById(Resource.Id.btn_resident_handler);
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