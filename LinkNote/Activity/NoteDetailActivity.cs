using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using LinkNote.OtherClass;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace LinkNote.Activity
{
    [Activity(Label = "NoteDetailActivity")]
    public class NoteDetailActivity : AppCompatActivity
    {
        public const string EXTRA_NAME = "note_name";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NoteDetail);

            var cheeseName = Intent.GetStringExtra(EXTRA_NAME);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
            collapsingToolbar.SetTitle(cheeseName);

            loadBackdrop();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        void loadBackdrop()
        {
            var imageView = FindViewById<ImageView>(Resource.Id.backdrop);

            var r = Notes.GetRandomNoteBackground();
            imageView.SetImageResource(r);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.SampleActions, menu);
            return true;
        }
    }
}