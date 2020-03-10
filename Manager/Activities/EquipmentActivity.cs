using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Manager.Adapter;
using Manager.Api.Response;
using Manager.Constant;
using Newtonsoft.Json;

namespace Manager.Activities
{
    [Activity(Label = "Thông tin tài sản")]
    public class EquipmentActivity : AppCompatActivity
    {
        RecyclerView equipmentRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        EquipmentAdapter equipmentAdapter;
        List<EquipmentResponse> equipments = new List<EquipmentResponse>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_equipment);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_main);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            equipmentRecyclerView = FindViewById<RecyclerView>(Resource.Id.rc_equipment);
            mLayoutManager = new LinearLayoutManager(this);
            equipmentRecyclerView.SetLayoutManager(mLayoutManager);
            equipmentAdapter = new EquipmentAdapter(equipments, this);
            equipmentRecyclerView.SetAdapter(equipmentAdapter);
            //equipmentAdapter.SetRecycleViewOnItemClickListener(this);
            GetListEquipment();

            // Create your application here
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.popup_menu, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    {
                        Finish();
                        return true;
                    }

            }
            return base.OnOptionsItemSelected(item);
        }

        private void GetListEquipment()
        {
            try
            {
                HttpClient client = new HttpClient();
                string residentId = Intent.GetStringExtra("id");
                string url = Url.BASE_URL + "GetEquipmentByResident" + "(" + "Id=" + residentId + ")";
                var uri = new Uri(url);
                Task<HttpResponseMessage> message = client.GetAsync(uri);
                if (message.Result.IsSuccessStatusCode)

                {
                    var content = message.Result.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Equipments>(content.Result);
                    int count = response.value.Count();
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
                    Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
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
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
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
    }
}