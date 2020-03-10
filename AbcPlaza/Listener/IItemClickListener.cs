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

namespace AbcPlaza.Listener
{
    public interface IItemClickListener
    {
        void OnClick(View view, int position);

        void OnLongCLick(View view, int position);
    }
}