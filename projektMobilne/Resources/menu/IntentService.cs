
using System;

using Android.App;
using Android.Content;
using Android.OS;

namespace projektMobilne.Resources.menu
{
    [Service(Label = "IntentService")]
    [IntentFilter(new String[] { "com.yourname.IntentService" })]
    public class IntentService : IntentService
    {
        IBinder binder;

        protected override void OnHandleIntent(Intent intent)
        {
            // Perform your service logic here
        }

        public override IBinder OnBind(Intent intent)
        {
            binder = new IntentServiceBinder(this);
            return binder;
        }
    }

    public class IntentServiceBinder : Binder
    {
        readonly IntentService service;

        public IntentServiceBinder(IntentService service)
        {
            this.service = service;
        }

        public IntentService GetIntentService()
        {
            return service;
        }
    }
}
