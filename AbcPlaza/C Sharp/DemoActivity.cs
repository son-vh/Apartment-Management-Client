using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AbcPlaza.Api;
using AbcPlaza.Api.Request;
using AbcPlaza.Api.Response;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace AbcPlaza.C_Sharp
{
    [Activity(Label = "DemoActivity")]
    public class DemoActivity : Activity
    {
        Button button;
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                SetContentView(Resource.Layout.demo);
                button = FindViewById<Button>(Resource.Id.btn_demo);
                button.Click += async delegate
                {
                    var model = new RestApi();
                    var request = new DemoRequest();
                    request.Name = "morpheus";
                    request.Job = "leader";
                    HttpResponseMessage message = await model.PostAsync("https://reqres.in/api/users", request);
                    if (message.IsSuccessStatusCode)
                    { 
                        var content = await message.Content.ReadAsStringAsync();
                        var info = JsonConvert.DeserializeObject<DemoResponse>(content);
                    }
                    else
                    {
                        Log.Error("Some errors"," errors");
                    }
                };
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex.Message);
            }
        }
    }
}