using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Util;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using AbcPlaza.Api.Response;
using Android.Util;
using AbcPlaza.Adapter;
using AbcPlaza.Constant;
using Refractored.Controls;
using Plugin.Media.Abstractions;
using FR.Ganfra.Materialspinner;

namespace AbcPlaza.Activities
{
    [Activity(Label = "Thêm thiết bị")]
    public class AddEquipmentActivity : AppCompatActivity
    {
        public static String EXTRA_DATA = "EXTRA_DATA";
        public static readonly int PickImageId = 1000;

        private EditText addEquipmentName;
        private EditText addPurchaseDate;
        private EditText addWarrantyPeriod;
        private Button addEquipment;
        private ImageView imageAddEquipment;
        private Button addEquipmentImage;
        private MediaFile _mediaFile;
        private MaterialSpinner spEquipment;
        private MaterialSpinner spWarrantyPeriod;
        private ArrayAdapter equipmentAdapter;
        private ArrayAdapter warrantyPeriodAdapter;
        private List<int> listItem = new List<int>();
        private string equipmentName;
        private int warrantyPeriod;
        //private ImageView addImage;

        private void InitItems()
        {
            for(int i = 1; i <= 48; i++)
            {
                listItem.Add(i);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_equipment);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_main);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            addPurchaseDate = (EditText)FindViewById(Resource.Id.edt_add_purchase_date);
            addEquipment = (Button)FindViewById(Resource.Id.btn_add_equipment);
            addEquipmentImage = (Button)FindViewById(Resource.Id.btn_add_equipment_image);
            imageAddEquipment = (ImageView)FindViewById(Resource.Id.img_add_equipment_image);
            spEquipment = (MaterialSpinner)FindViewById(Resource.Id.sp_add_equipment);
            spWarrantyPeriod = (MaterialSpinner)FindViewById(Resource.Id.sp_add_warranty_period);
            //addImage = (ImageView)FindViewById(Resource.Id.img_add_equipment_image);
            imageAddEquipment.SetImageResource(Resource.Drawable.fan);

            string[] equipments = { "Máy quạt", "Lò vi sóng", "Điều hòa", "Tủ lạnh", "Bếp điện", "Nồi cơm điện" };
            equipmentAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, equipments);
            equipmentAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spEquipment.Adapter = equipmentAdapter;

            spEquipment.ItemSelected += (s, e) =>
            {
                if (e.Position != -1)
                {
                    equipmentName = spEquipment.GetItemAtPosition(e.Position).ToString();
                }
            };

            InitItems();
            warrantyPeriodAdapter = new ArrayAdapter<int>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, listItem);
            warrantyPeriodAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spWarrantyPeriod.Adapter = warrantyPeriodAdapter;

            spWarrantyPeriod.ItemSelected += (s, e) =>
            {
                if (e.Position != -1)
                {
                    warrantyPeriod = int.Parse(spWarrantyPeriod.GetItemAtPosition(e.Position).ToString());
                }
              
                Console.WriteLine(warrantyPeriod);
            };


            addPurchaseDate.Click += (sender, e) =>
            {
                Calendar cldr = Calendar.Instance;
                int day = cldr.Get(CalendarField.DayOfMonth);
                int month = cldr.Get(CalendarField.Month);
                int year = cldr.Get(CalendarField.Year);
                // date picker dialog
                DatePickerDialog picker = new DatePickerDialog(this, OnDateSet, year, month, day);
                picker.Show();
            };
        
            addEquipmentImage.Click += (sender, e) =>
            {
                //var uri = new Uri(string.Format(Constants.UsersRestUrl + "/Files/Upload/", string.Empty));
                //var content = new MultipartFormDataContent();
                //_mediaFile = CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                //{
                //    Directory = "Sample",
                //    Name = "myImage.jpg"
                //});

                ////_mediaFile = new MediaFile("cdccc",);
                //content.Add(new StreamContent(_mediaFile.GetStream()),
                //    "\"file\"",
                //    $"\"{_mediaFile.Path}\"");

                //var httpClient = new HttpClient();
                //var httpResponseMessage = await httpClient.PostAsync(uri, content);
                //Intent = new Intent();
                //Intent.SetType("image/*");
                //Intent.SetAction(Intent.ActionGetContent);
                //StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
            };
            addEquipment.Click += (sender, e) =>
             {
                 try
                 {
                         //var content = new MultipartFormDataContent();
                         //_mediFile = new MediaFile("sdsd");
                         //content.Add(new StreamContent(_mediFile.GetStream()),
                         //    "\"file\"",
                         //    $"\"{_mediFile.Path}\"");
                         //var uri = new Uri("http://192.168.1.233:45459/api/FileUploading/UploadFile");
                         //using (var client = new HttpClient())
                         //{
                         //    Task<HttpResponseMessage> message = client.PostAsync(uri, content);
                         //    if (message.Result.IsSuccessStatusCode)
                         //    {
                         //        Console.WriteLine("Thanh cong");
                         //    }

                         //}
                     HttpClient client = new HttpClient();
                     string url = Url.BASE_URL + "Equipment";
                     var uri = new Uri(url);
                     EquipmentResponse equipment = new EquipmentResponse();
                     equipment.Id = "1";
                     equipment.EquipmentName = equipmentName;
                     string add = addPurchaseDate.Text.ToString();
                     DateTime dt = DateTime.ParseExact(add, "dd/MM/yyyy", null);
                     equipment.PurchaseDate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
                     equipment.ResidentId = 3;
                     equipment.WarrantyPeriod = warrantyPeriod;
                     var json = JsonConvert.SerializeObject(equipment);
                     var content = new StringContent(json, Encoding.UTF8, "application/json");
                     Task<HttpResponseMessage> message = client.PostAsync(uri, content);
                     if (message.Result.IsSuccessStatusCode)
                     {
                         Intent data = new Intent();
                         data.PutExtra(EXTRA_DATA, "Some interesting data!");
                         SetResult(Result.Ok, data);
                         Finish();
                     }
                     else
                     {
                         Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                         Android.App.AlertDialog alert = dialog.Create();
                         alert.SetTitle("Thông báo");
                         alert.SetMessage("Thêm thiết bị thất bại");
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
                     alert.SetMessage("Thêm thiết bị thất bại");
                     alert.SetButton("OK", (c, ev) =>
                     {
                         // Ok button click task  
                     });
                     alert.SetButton2("CANCEL", (c, ev) => { });
                     alert.Show();
                 }

             };


        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.popup_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                imageAddEquipment.SetImageURI(uri);

            }
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            if (e.Date.Day.ToString().Length < 2 && e.Date.Month.ToString().Length < 2)
            {
                addPurchaseDate.Text = "0" + e.Date.Day.ToString() + "/" + "0" + e.Date.Month.ToString() + "/" + e.Date.Year.ToString();
            }
            else
            {
                if (e.Date.Day.ToString().Length < 2)
                {
                    addPurchaseDate.Text = "0" + e.Date.Day.ToString() + "/" + e.Date.Month.ToString() + "/" + e.Date.Year.ToString();
                }
                else if (e.Date.Month.ToString().Length < 2)
                {
                    addPurchaseDate.Text = e.Date.Day.ToString() + "/" + "0" + e.Date.Month.ToString() + "/" + e.Date.Year.ToString();
                }
                else
                {
                    addPurchaseDate.Text = e.Date.Day.ToString() + "/" + e.Date.Month.ToString() + "/" + e.Date.Year.ToString();
                }
            }
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

    }
}