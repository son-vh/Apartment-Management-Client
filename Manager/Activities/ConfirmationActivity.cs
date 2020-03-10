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
using Android.Util;
using Android.Views;
using Android.Widget;
using Manager.Api.Request;
using Manager.Api.Response;
using Manager.Constant;
using Newtonsoft.Json;
using Square.Picasso;

namespace Manager.Activities
{
    [Activity(Label = "Xác nhận hỗ trợ")]
    public class ConfirmationActivity : AppCompatActivity
    {
        private TextView typeSupport;
        private TextView dateSupport;
        private TextView addressSupport;
        private ImageView billSupport;
        private Button confirm;
        private EditText editConfirmation;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_confirmation);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_main);
            typeSupport = FindViewById<TextView>(Resource.Id.tv_type_support);
            dateSupport= FindViewById<TextView>(Resource.Id.tv_date_support);
            addressSupport = FindViewById<TextView>(Resource.Id.tv_address_support);
            billSupport = FindViewById<ImageView>(Resource.Id.img_bill_support);
            confirm = FindViewById<Button>(Resource.Id.btn_confirm);
            editConfirmation = FindViewById<EditText>(Resource.Id.edt_confirmation);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            try
            {
                HttpClient client = new HttpClient();
                string supportId = Intent.GetStringExtra("id");
                string url = Url.BASE_URL + "FindSupportById" + "(" + "Id=" + supportId + ")";
                var uri = new Uri(url);
                Task<HttpResponseMessage> message = client.GetAsync(uri);
                if (message.Result.IsSuccessStatusCode)
                {
                    var content = message.Result.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Confirms>(content.Result);
                    int count = response.value.Count();
                    for (int i = 0; i < count; i++)
                    {
                        ConfirmResponse confirmResponse = new ConfirmResponse();
                        typeSupport.Text = response.value.ElementAt(0).SupportType;
                        dateSupport.Text = response.value.ElementAt(0).SupportDate;
                        addressSupport.Text = response.value.ElementAt(0).SupportAddress;
                        Picasso.With(this)
                        .Load(response.value.ElementAt(0).SupportImage)
                        .Resize(90, 90)
                        .Into(billSupport);
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

            confirm.Click += (sender, e) =>
            {
                try
                {
                    HttpClient client = new HttpClient();
                    string url = Url.BASE_URL + "api/Confirmation";
                    var uri = new Uri(url);
                    ConfirmationRequest request = new ConfirmationRequest();
                    request.title = "Xác nhận";
                    request.body = editConfirmation.Text.ToString();
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    Task<HttpResponseMessage> message = client.PostAsync(uri, content);
                    if (message.Result.IsSuccessStatusCode)
                    {
                        Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                        Android.App.AlertDialog alert = dialog.Create();
                        alert.SetTitle("Thông báo");
                        alert.SetMessage("Gửi xác nhận thành công");
                        alert.SetButton("OK", (c, ev) => {
                            // Ok button click task  
                        });

                        alert.SetButton2("CANCEL", (c, ev) => { });
                        alert.Show();

                    }
                    else
                    {
                        Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                        Android.App.AlertDialog alert = dialog.Create();
                        alert.SetTitle("Thông báo");
                        alert.SetMessage("Gửi xác nhận thất bại");
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
                    alert.SetMessage("Gửi xác nhận thất bại");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        // Ok button click task  
                    });
                    alert.SetButton2("CANCEL", (c, ev) => { });
                    alert.Show();
                }
        };



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

    }
}