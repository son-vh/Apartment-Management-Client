﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Manager.Api.Request
{
    public class ConfirmationRequest
    {
        public string body { get; set; }

        public string title { get; set; }
    }
}