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

namespace AbcPlaza.Api.Request
{
    public class SupportRequest
    {      
        public string SupportType { get; set; }

        public string SupportDate { get; set; }

        public string SupportAddress { get; set; }

        public string SupportImage { get; set; }

        public int ResidentId { get; set; }
    }
}