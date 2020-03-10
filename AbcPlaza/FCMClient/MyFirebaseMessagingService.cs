﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Util;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Android.Support.V4.App;

namespace AbcPlaza.FCMClient
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);

            var body = message.GetNotification().Body;
            Log.Debug(TAG, "Notification Message Body: " + body);
            SendNotification(body, message.Data);
        }

        void SendNotification(string messageBody, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MainActivity));
            //intent.AddFlags(ActivityFlags.ClearTop);
            //foreach (var key in data.Keys)
            //{
            //    intent.PutExtra(key, data[key]);
            //}

            //var pendingIntent = PendingIntent.GetActivity(this,
            //                                              MainActivity.NOTIFICATION_ID,
            //                                              intent,
            //                                              PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                      .SetSmallIcon(Resource.Drawable.fan)
                                      .SetContentTitle("FCM Message")
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true);
                                      //.SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
        }
    }
}