using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Manager.Fragments;

namespace Manager.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class UIActivity : AppCompatActivity
    {
        TextView textMessage;
        private Android.Support.V7.Widget.Toolbar toolbar = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ui);
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_main);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            textMessage = FindViewById<TextView>(Resource.Id.message);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
            loadFragment(Resource.Id.navigation_apartments);

            // Create your application here
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.popup_menu, menu);

            return base.OnCreateOptionsMenu(menu);
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
                case Resource.Id.navigation_apartments:
                    fragment = SupportFragmment.NewInstance();
                    break;
                   
                case Resource.Id.navigation_residents:
                    fragment = ResidentFragment.NewInstance();
                    toolbar.SetTitle(Resource.String.title_equipment);
                    break;

                    //case Resource.Id.navigation_bills:
                    //    fragment = BillFragment.NewInstance();
                    //    break;
                    //case Resource.Id.navigation_notifications:
                    //    fragment = NotificationFragment.NewInstance();
                    //    break;
                    //case Resource.Id.navigation_account:
                    //    fragment = AccountFragment.NewInstance();
                    //    break;


            }
            if (fragment == null)
                return;
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
        }
    }
}
