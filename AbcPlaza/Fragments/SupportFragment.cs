using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using AbcPlaza.Api.Request;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Android.Util;
using FR.Ganfra.Materialspinner;
using AbcPlaza.Constant;
using Android.Icu.Util;
using Android.App;

namespace AbcPlaza.Fragments
{
    public class SupportFragment:Android.Support.V4.App.Fragment
    {

        private Button register;
        private MaterialSpinner spTypeSupport;
        private EditText dateSupport;
        private EditText timeSupport;
        private EditText addressSupport;
        private ArrayAdapter supportAdapter;
        private string type;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var v = inflater.Inflate(Resource.Layout.fragment_support, container, false);
            register = v.FindViewById<Button>(Resource.Id.btn_register);
            spTypeSupport = v.FindViewById<MaterialSpinner>(Resource.Id.sp_type_support);
            dateSupport = v.FindViewById<EditText>(Resource.Id.support_date);
            timeSupport = v.FindViewById<EditText>(Resource.Id.support_time);
            addressSupport = v.FindViewById<EditText>(Resource.Id.support_address);
            string[] supports = { "Lắp đặt", "Vận chuyển", "Sửa chữa" };
            supportAdapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, supports);
            supportAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spTypeSupport.Adapter = supportAdapter;

            spTypeSupport.ItemSelected += (s, e) =>
            {
                if (e.Position != -1)
                {
                    type = spTypeSupport.GetItemAtPosition(e.Position).ToString();
                }
            };

            dateSupport.Click += (sender, e) =>
            {
                Calendar cldr = Calendar.Instance;
                int day = cldr.Get(CalendarField.DayOfMonth);
                int month = cldr.Get(CalendarField.Month);
                int year = cldr.Get(CalendarField.Year);
                // date picker dialog
                DatePickerDialog picker = new DatePickerDialog(Context, OnDateSet, year, month, day);
                picker.Show();
            };

            timeSupport.Click += (sender, e) =>
            {
                Calendar cldr = Calendar.Instance;
                int hour = cldr.Get(CalendarField.HourOfDay);
                int minute = cldr.Get(CalendarField.Minute);
                int year = cldr.Get(CalendarField.Year);
                // date picker dialog
                TimePickerDialog picker = new TimePickerDialog(Context, OnTimeSet, hour, minute, true);
                picker.Show();
            };


            register.Click += (sender, e) =>
            {
                try
                {
                    HttpClient client = new HttpClient();
                    string url = Url.BASE_URL + "Support";
                    var uri = new Uri(url);
                    SupportRequest supportRequest = new SupportRequest();
                    supportRequest.SupportType = type;
                    string add = dateSupport.Text.ToString();
                    DateTime dt = DateTime.ParseExact(add, "dd/MM/yyyy", null);
                    supportRequest.SupportDate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
                    supportRequest.SupportAddress = addressSupport.Text.ToString();
                    supportRequest.SupportImage = "http://192.168.1.118:45457/Static/Equipment/washing_machine.png";
                    supportRequest.ResidentId = 3;
                    var json = JsonConvert.SerializeObject(supportRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    Task<HttpResponseMessage> message = client.PostAsync(uri, content);
                    if (message.Result.IsSuccessStatusCode)
                    {
                        Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(Context);
                        Android.App.AlertDialog alert = dialog.Create();
                        alert.SetTitle("Thông báo");
                        alert.SetMessage("Đăng kí hỗ trợ thành công ");
                        alert.SetButton("OK", (c, ev) =>
                        {
                            // Ok button click task  
                        });
                        alert.SetButton2("CANCEL", (c, ev) => { });
                        alert.Show();
                    }
                    else
                    {
                        Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(Context);
                        Android.App.AlertDialog alert = dialog.Create();
                        alert.SetTitle("Thông báo");
                        alert.SetMessage("Đăng kí hỗ trợ thất bại");
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
                    alert.SetMessage("Đăng kí hỗ trợ thất bại");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        // Ok button click task  
                    });
                    alert.SetButton2("CANCEL", (c, ev) => { });
                    alert.Show();
                }

            };
            return v;
        }

        private void OnTimeSet(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            timeSupport.Text = e.HourOfDay.ToString() + ":" + e.Minute.ToString();
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            if (e.Date.Day.ToString().Length < 2 && e.Date.Month.ToString().Length < 2)
            {
                dateSupport.Text = "0" + e.Date.Day.ToString() + "/" + "0" + e.Date.Month.ToString() + "/" + e.Date.Year.ToString();
            }
            else
            {
                if (e.Date.Day.ToString().Length < 2)
                {
                    dateSupport.Text = "0" + e.Date.Day.ToString() + "/" + e.Date.Month.ToString() + "/" + e.Date.Year.ToString();
                }
                else if (e.Date.Month.ToString().Length < 2)
                {
                    dateSupport.Text = e.Date.Day.ToString() + "/" + "0" + e.Date.Month.ToString() + "/" + e.Date.Year.ToString();
                }
                else
                {
                    dateSupport.Text = e.Date.Day.ToString() + "/" + e.Date.Month.ToString() + "/" + e.Date.Year.ToString();
                }
            }
        }

        public static SupportFragment NewInstance()
        {
            var frag = new SupportFragment { Arguments = new Bundle() };
            return frag;
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("The Language is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
        }
    }
}