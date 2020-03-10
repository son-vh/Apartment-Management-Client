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

namespace AbcPlaza.Api.Response
{
    public class DemoResponse
    {
        private string name;

        public string Name
        {
            get => name;
            set => name = value;
        }

        private string job;

        public string Job
        {
            get => job;
            set => job = value;
        }

        private string id;

        public string Id
        {
            get => id;
            set => id = value;
        }

        private string createdAt;

        public string CreatedAt
        {
            get => createdAt;
            set => createdAt = value;
        }
    }
}