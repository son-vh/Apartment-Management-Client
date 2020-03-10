using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Staff.C_Sharp;

namespace Staff
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView textMessage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            textMessage = FindViewById<TextView>(Resource.Id.message);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
            loadFragment(Resource.Id.navigation_residents);
        }
        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            loadFragment(e.Item.ItemId);
        }

        private void loadFragment(int id)
        {
            //load fragment
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.navigation_residents:
                    fragment = ResidentFragment.NewInstance();
                    break;
                case Resource.Id.navigation_apartments:
                    fragment = ApartmentFragment.NewInstance();
                    break;
                case Resource.Id.navigation_parking:
                    fragment = ParkingFragment.NewInstance();
                    break;
                case Resource.Id.navigation_notifications:
                    fragment = NotificationFragment.NewInstance();
                    break;
            }
            if (fragment == null)
                return;
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
        }
    }
}

