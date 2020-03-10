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
    public class ResidentFragment : Fragment, IRecycleViewOnItemClickListener
    {
        private RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        ResidentAdapter residentAdapter;
        List<ResidentResponse> residents = new List<ResidentResponse>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static ResidentFragment NewInstance()
        {
            var frag = new ResidentFragment { Arguments = new Bundle() };
            return frag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_equipment, container, false);
            mRecycleView = v.FindViewById<RecyclerView>(Resource.Id.rc_resident);
            mLayoutManager = new LinearLayoutManager(Context);
            mRecycleView.SetLayoutManager(mLayoutManager);
            residentAdapter = new ResidentAdapter(residents, Context);
            mRecycleView.SetAdapter(residentAdapter);
            residentAdapter.SetRecycleViewOnItemClickListener(this);
            GetListResident();
            return v;

        }
        private void GetListResident()
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = Url.BASE_URL + "Resident";
                var uri = new Uri(url);
                Task<HttpResponseMessage> message = client.GetAsync(uri);
                if (message.Result.IsSuccessStatusCode)

                {
                    var content = message.Result.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Residents>(content.Result);
                    int count = response.value.Count();
                    for (int i = 0; i < count; i++)
                    {
                        ResidentResponse resident = new ResidentResponse();
                        resident.Id = response.value.ElementAt(i).Id;
                        resident.ResidentName = response.value.ElementAt(i).ResidentName;
                        resident.Room = response.value.ElementAt(i).Room;
                        resident.Floor = response.value.ElementAt(i).Floor;
                        resident.ResidentImage = response.value.ElementAt(i).ResidentImage;
                        residents.Add(resident);
                        residentAdapter.NotifyDataSetChanged();
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
            Intent updateIntent = new Intent(Context, typeof(EquipmentActivity));
            updateIntent.PutExtra("id", residents[position].Id);
            StartActivityForResult(updateIntent, 100);
        }

        public void OnLongClick(View view, int position)
        {

        }
    }
}
