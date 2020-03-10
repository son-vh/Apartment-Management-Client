using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbcPlaza.Activities;
using AbcPlaza.Adapter;
using AbcPlaza.Api.Response;
using AbcPlaza.Listener;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using AbcPlaza.Api;
using Newtonsoft.Json;
using Android.Util;
using AbcPlaza.Api.Request;
using System.Threading.Tasks;
using Android.Support.V4.Widget;
using AbcPlaza.Constant;

namespace AbcPlaza.Fragments
{
    public class EquipmentFragment : Fragment, IRecycleViewOnItemClickListener
    {

        private FloatingActionButton fabAddEquipment;
        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        EquipmentAdapter equipmentAdapter;
        List<EquipmentResponse> equipments = new List<EquipmentResponse>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public static EquipmentFragment NewInstance()
        {
            var frag = new EquipmentFragment { Arguments = new Bundle() };
            return frag;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 100)
            {
                if (resultCode == -1)
                {
                    String result = data.GetStringExtra(AddEquipmentActivity.EXTRA_DATA);
                    GetListEquipment();
                }
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_equipment, container, false);
            mRecycleView = v.FindViewById<RecyclerView>(Resource.Id.recyclerView);


            fabAddEquipment = v.FindViewById<FloatingActionButton>(Resource.Id.fab_add_equipment);
            fabAddEquipment.Click += (sender, e) =>
            {
                Intent addIntent = new Intent(Context, typeof(AddEquipmentActivity));
                StartActivityForResult(addIntent, 100);
            };
            mLayoutManager = new LinearLayoutManager(Context);
            mRecycleView.SetLayoutManager(mLayoutManager);
            equipmentAdapter = new EquipmentAdapter(equipments, Context);
            mRecycleView.SetAdapter(equipmentAdapter);
            equipmentAdapter.SetRecycleViewOnItemClickListener(this);
            GetListEquipment();
            return v;

        }

        private void GetListEquipment()
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = Url.BASE_URL + "GetEquipmentByResident(Id=3)";
                var uri = new Uri(url);
                Task<HttpResponseMessage> message = client.GetAsync(uri);
                if (message.Result.IsSuccessStatusCode)

                {
                    var content = message.Result.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<ValueResponse>(content.Result);
                    int count = response.value.Count();
                    if (equipments != null && equipments.Count() != 0)
                    {
                        equipments.Clear();
                    }
                    for (int i = 0; i < count; i++)
                    {
                        EquipmentResponse equipment = new EquipmentResponse();
                        equipment.Id = response.value.ElementAt(i).Id;
                        equipment.EquipmentName = response.value.ElementAt(i).EquipmentName;
                        equipment.PurchaseDate = response.value.ElementAt(i).PurchaseDate;
                        equipment.WarrantyPeriod = response.value.ElementAt(i).WarrantyPeriod;
                        equipment.EquipmentImage = response.value.ElementAt(i).EquipmentImage;
                        equipments.Add(equipment);
                        equipmentAdapter.NotifyDataSetChanged();
                    }
                }
                else
                {
                    Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(Context);
                    Android.App.AlertDialog alert = dialog.Create();
                    alert.SetTitle("Thông báo");
                    alert.SetMessage("Something went wrong ");
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
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(Context);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Thông báo");
                alert.SetMessage("Something went wrong ");
                alert.SetButton("OK", (c, ev) =>
                {
                    // Ok button click task  
                });
                alert.SetButton2("CANCEL", (c, ev) => { });
                alert.Show();
            }

        }

        public void OnClick(View view, int position)
        {
            Intent updateIntent = new Intent(Context, typeof(UpdateEquipmentActivity));
            updateIntent.PutExtra("abc", equipments[position].PurchaseDate);
            updateIntent.PutExtra("id", equipments[position].Id);
            StartActivityForResult(updateIntent, 100);
        }

        public void OnLongClick(View view, int position)
        {

        }

    }

}