
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

namespace MyNotes
{
	[Activity (Label = "Editor", Icon = "@drawable/ic_launcher")]			
	public class NoteEditorActivity : Activity
	{
		private NoteItem noteItem;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.NoteEditor);
			ActionBar.SetDisplayHomeAsUpEnabled (true);
			Intent intent = this.Intent;
			noteItem = new NoteItem ();
			noteItem.Key = new Guid(intent.GetStringExtra ("key"));
			noteItem.Text = intent.GetStringExtra ("text");

			EditText text = FindViewById<EditText> (Resource.Id.noteText);
			text.Text = noteItem.Text;
			text.SetSelection (noteItem.Text.Length);
		}

		private void saveAndFinish()
		{
			EditText text = FindViewById<EditText> (Resource.Id.noteText);
			String noteText = text.Text;
			Intent intent = new Intent ();
			intent.PutExtra ("key", noteItem.Key.ToString());
			intent.PutExtra ("text", noteText);
			SetResult (Result.Ok, intent);
			Finish ();
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home) 
			{
				saveAndFinish ();
			}

			return false;
		}

		protected override void OnResume ()
		{
			base.OnResume ();
		}

		public override void OnBackPressed ()
		{
			saveAndFinish ();
		}
	}
}