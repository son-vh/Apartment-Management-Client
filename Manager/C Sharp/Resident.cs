using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Manager.C_Sharp
{
    public class Resident
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public string Block { get; set; }

        public string Area { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }
    }
}