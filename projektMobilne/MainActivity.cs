using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;
using System;
using Android.Views;
using Android.Views.Animations;
using Android.Views.InputMethods;
using Android.Text;
using System.Linq;

namespace projektMobilne
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private List<Person> mItems;
        private ListView mListView;
        private EditText mSearch;
        private LinearLayout mContainer;
        private ListViewAdapter adapter;
        private bool mAnimatedDown;
        private bool mIsAnimating;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            mListView = FindViewById<ListView>(Resource.Id.friendsView);
            mSearch = FindViewById<EditText>(Resource.Id.mSearch);
            mContainer = FindViewById<LinearLayout>(Resource.Id.Container);

            mSearch.Alpha = 0;
            mSearch.TextChanged += mSearch_TextChanged;

            mItems = new List<Person>();
            mItems.Add(new Person() { FirstName = "Marian", LastName = "Kowal", Age = "20", Gender = "M" });
            mItems.Add(new Person() { FirstName = "Janusz", LastName = "Liśc", Age = "29", Gender = "M" });
            mItems.Add(new Person() { FirstName = "Grażyna", LastName = "Bąk", Age = "32", Gender = "K" });

            adapter = new ListViewAdapter(this, mItems, Resource.Layout.listview_row);

            mListView.Adapter = adapter;


            mListView.ItemClick += mListView_ItemClick;
        }

        private void mSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Person> searchPerson = (from person in mItems
                                         where person.FirstName.ToLower().Contains(mSearch.Text.ToLower()) || person.Age.Contains(mSearch.Text)
                                        select person).ToList<Person>();
            adapter = new ListViewAdapter(this, searchPerson, Resource.Layout.listview_row);
            mListView.Adapter = adapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch(item.ItemId)
            {
                case Resource.Id.search:
                    if (mIsAnimating)
                    {
                        return true;
                    }

                    if(!mAnimatedDown)
                    {
                        MyAnimation anim = new MyAnimation(mListView, mListView.Height - mSearch.Height);
                        anim.Duration = 500;
                        mListView.StartAnimation(anim);
                        anim.AnimationStart += anim_AnimationStartDown;
                        anim.AnimationEnd += anim_AnimationEndDown;
                        mContainer.Animate().TranslationYBy(mSearch.Height).SetDuration(500).Start();
                    }
                    else
                    {
                        MyAnimation anim = new MyAnimation(mListView, mListView.Height + mSearch.Height);
                        anim.Duration = 500;
                        mListView.StartAnimation(anim);
                        anim.AnimationStart += anim_AnimationStartUp;
                        anim.AnimationEnd += anim_AnimationEndUp;
                        mContainer.Animate().TranslationYBy(-mSearch.Height).SetDuration(500).Start();
                    }

                    mAnimatedDown = !mAnimatedDown;
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void anim_AnimationEndUp(object sender, Animation.AnimationEndEventArgs e)
        {
            mIsAnimating = false;
            mSearch.ClearFocus();
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }

        private void anim_AnimationEndDown(object sender, Animation.AnimationEndEventArgs e)
        {
            mIsAnimating = false;
        }

        private void anim_AnimationStartDown(object sender, Animation.AnimationStartEventArgs e)
        {
            mIsAnimating = true;
            mSearch.Animate().AlphaBy(1.0f).SetDuration(500).Start();
        }

        private void anim_AnimationStartUp(object sender, Animation.AnimationStartEventArgs e)
        {
            mIsAnimating = true;
            mSearch.Animate().AlphaBy(-1.0f).SetDuration(300).Start();
        }

        private void mListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Console.WriteLine(mItems[e.Position].FirstName);
        }
    }
}