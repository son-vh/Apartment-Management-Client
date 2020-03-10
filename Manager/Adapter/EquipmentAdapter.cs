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
using Square.Picasso;

namespace Manager.Adapter
{
    public class EquipmentAdapter:RecyclerView.Adapter
    {
        public List<EquipmentResponse> data { get; set; }

        public override int ItemCount
        {
            get
            {
                return data.Count;
            }
        }

        private Context context;


        public EquipmentAdapter(List<EquipmentResponse> data, Context context)
        {
            this.data = data;
            this.context = context;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            EquipmentViewHolder viewHolder = holder as EquipmentViewHolder;
            viewHolder.equipmentName.Text = data[position].EquipmentName;
            viewHolder.purchase.Text = data[position].PurchaseDate;
            viewHolder.expiration.Text = data[position].WarrantyPeriod.ToString();
            Picasso.With(context)
            .Load(data[position].EquipmentImage)
            .Resize(90, 90)
            .Into(viewHolder.equipmentImage);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.custom_equipment, parent, false);
            return new EquipmentViewHolder(view);
        }
    }

    public class EquipmentViewHolder : RecyclerView.ViewHolder
    {
        // https://www.youtube.com/watch?v=CN66PE1j7yw
        //private IItemClickListener itemClickListener;


        public TextView equipmentName { get; set; }
        public TextView purchase { get; set; }
        public TextView expiration { get; set; }
        public ImageView equipmentImage { get; set; }
        public Button equipmentHandler { get; set; }


        public EquipmentViewHolder(View itemView) : base(itemView)
        {
           
            equipmentName = (TextView)itemView.FindViewById(Resource.Id.tv_equipment_name);
            purchase = (TextView)itemView.FindViewById(Resource.Id.tv_purchase);
            expiration = (TextView)itemView.FindViewById(Resource.Id.tv_expiration);
            equipmentImage = (ImageView)itemView.FindViewById(Resource.Id.img_equipment);
            equipmentHandler = (Button)itemView.FindViewById(Resource.Id.btn_equipment_handler);
            //itemView.SetOnClickListener(this);
            //itemView.SetOnLongClickListener(this);

        }

        //public void SetItemClickListener(IItemClickListener itemClickListener)
        //{
        //    this.itemClickListener = itemClickListener;
        //}

        //public void OnClick(View v)
        //{
        //    itemClickListener.OnClick(v, AdapterPosition);
        //}

        //public bool OnLongClick(View v)
        //{
        //    itemClickListener.OnLongCLick(v, AdapterPosition);
        //    return true;
        //}
    }
}