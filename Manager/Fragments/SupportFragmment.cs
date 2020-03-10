using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Manager.Activities;
using Manager.Adapter;
using Manager.Api.Response;
using Manager.Constant;
using Manager.Listener;
using Newtonsoft.Json;

namespace Manager.Fragments
{
    public class SupportFragmment : Fragment, IRecycleViewOnItemClickListener
    {
        private RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        SupportAdapter supportAdapter;
        List<SupportResponse> supports = new List<SupportResponse>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Create your fragment here
        }

        public static SupportFragmment NewInstance()
        {
            var frag = new SupportFragmment { Arguments = new Bundle() };
            return frag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_support, container, false);
            mRecycleView = v.FindViewById<RecyclerView>(Resource.Id.rc_support);
            mLayoutManager = new LinearLayoutManager(Context);
            mRecycleView.SetLayoutManager(mLayoutManager);
            supportAdapter = new SupportAdapter(supports, Context);
            mRecycleView.SetAdapter(supportAdapter);
            supportAdapter.SetRecycleViewOnItemClickListener(this);
            GetListSupport();
            return v;

        }
        public void OnClick(View view, int position)
        {
            Intent updateIntent = new Intent(Context, typeof(ConfirmationActivity));
            updateIntent.PutExtra("id", (position+1).ToString());
            StartActivityForResult(updateIntent, 100);
        }

        public void OnLongClick(View view, int position)
        {

        }

        private void GetListSupport()
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = Url.BASE_URL + "FindResidentBySupport";
                var uri = new Uri(url);
                Task<HttpResponseMessage> message = client.GetAsync(uri);
                if (message.Result.IsSuccessStatusCode)
                {
                    var content = message.Result.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Supports>(content.Result);
                    int count = response.value.Count();
                    for (int i = 0; i < count; i++)
                    {
                        SupportResponse supportResidentResponse = new SupportResponse();
                        supportResidentResponse.ResidentName = response.value.ElementAt(i).ResidentName;
                        supportResidentResponse.ResidentImage = response.value.ElementAt(i).ResidentImage;
                        supports.Add(supportResidentResponse);
                        supportAdapter.NotifyDataSetChanged();
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
                    alert.Show(); ;
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
    }
}