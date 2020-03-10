using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System;

namespace Manager
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]

    public class MainActivity : AppCompatActivity
    {
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ListOfResident);
            var lstData = FindViewById<ListView>(Resource.Id.listView_resident);
           
            List<C_Sharp.Resident> lstSource = new List<C_Sharp.Resident>();

            //Set cứng data
            for (int i = 0; i < 20; i++)
            {
                string gd;
                string n;
                if (i % 4 == 0)
                {
                    n = "Nguyễn Thị ";
                    gd = "Nữ";
                }
                else
                {
                    n = "Trần Văn ";
                    gd = "Nam";
                }

                n = n + Convert.ToChar(i + 64);

                C_Sharp.Resident resident = new C_Sharp.Resident()
                {
                
                    Id = i,
                    Name = n,
                    Age = 20 + i%3,
                    Gender = gd,
                    Block = "A11",
                    Area = "A",
                    Floor = "3",
                    Apartment = "P20" + (i % 3).ToString(),

                };
                lstSource.Add(resident);

            }
            var adapter = new Manager.Resources.CustomAdapter(this, lstSource);
            lstData.Adapter = adapter;  
            


        }
    }
}