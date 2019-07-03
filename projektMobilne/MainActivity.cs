using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;

namespace projektMobilne
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private List<string> mItems;
        private ListView mListView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            mListView = FindViewById<ListView>(Resource.Id.friendsView);

            mItems = new List<string>();
            mItems.Add("Marian");
            mItems.Add("Janusz");
            mItems.Add("Wioletta");

            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);

            ListViewAdapter adapter = new ListViewAdapter(this, mItems);

            mListView.Adapter = adapter;
        }
    }
}