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

namespace Manager.Listener
{
    public interface IRecycleViewOnItemClickListener
    {
        void OnClick(View view, int position);

        void OnLongClick(View view, int position);
    }
}