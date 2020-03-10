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

namespace Manager.Api.Response
{
    public class SupportResidentResponse
    {
        public string Number { get; set; }

        public string ResidentName { get; set; }

        public string ResidentImage { get; set; }
    }


    public class SupportResponse
    {
        public string ResidentName { get; set; }

        public string ResidentImage { get; set; }
    }

    public class Supports
    {
        public List<SupportResponse> value { get; set; }
    }
}