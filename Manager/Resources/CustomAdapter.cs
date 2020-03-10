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

namespace Manager.Resources
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtName { get; set; }
        public TextView txtGender { get; set; }
        public TextView txtAge { get; set; }     
        public TextView txtBlock { get; set; }
        public TextView txtArea { get; set; }
        public TextView txtFloor { get; set; }
        public TextView txtApartment { get; set; }

    }

    public class CustomAdapter : BaseAdapter
    {
        private Activity activity;
        private List<C_Sharp.Resident> residents;

        public CustomAdapter(Activity activity, List<C_Sharp.Resident> residents)
        {
            this.activity = activity;
            this.residents = residents;
        }

        public override int Count
        {
            get
            {
                return residents.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return residents[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate (Resource.Layout.ListViewDataTemplate, parent, false);
            //textView1, textView2 la Id cua layout
            var txtName = view.FindViewById<TextView>(Resource.Id.textView1);
            var txtGender = view.FindViewById<TextView>(Resource.Id.textView2);
            var txtAge = view.FindViewById<TextView>(Resource.Id.textView3);
            var txtBlock = view.FindViewById<TextView>(Resource.Id.textView4);
            var txtArea = view.FindViewById<TextView>(Resource.Id.textView5);
            var txtFloor = view.FindViewById<TextView>(Resource.Id.textView6);
            var txtApartment = view.FindViewById<TextView>(Resource.Id.textView7);

            txtName.Text = residents[position].Name;
            txtGender.Text = residents[position].Gender;
            //Gán int sang string
            txtAge.Text = residents[position].Age.ToString();
            txtBlock.Text = residents[position].Block;
            txtArea.Text = residents[position].Area;
            txtFloor.Text = residents[position].Floor;
            txtApartment.Text = residents[position].Apartment;

            return view;
        }
    }

}