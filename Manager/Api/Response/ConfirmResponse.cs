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
    public class ConfirmResponse
    {

        public string SupportType { get; set; }

        public string SupportDate { get; set; }

        public string SupportAddress { get; set; }

        public string SupportImage { get; set; }


    }

    public class Confirms
    {
        public List<ConfirmResponse> value { get; set; }
    }
}