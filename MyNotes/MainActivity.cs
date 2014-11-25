using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace MyNotes
{
	[Activity (Label = "MyNotes", MainLauncher = false, Icon = "@drawable/ic_launcher")]
	public class MainActivity : ListActivity
	{
		private static int EDITOR_ACTIVITY_REQUEST = 1001;
		private static int MENU_DELETE_ID = 1002;
		private int currentNoteId;
		private List<NoteItem> notesList;
		private NotesDataSource datasource;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			RegisterForContextMenu (ListView);
			datasource = new NotesDataSource (this);
			refresh ();
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			this.MenuInflater.Inflate(Resource.Menu.main, menu);
			return true;
		}
			
		public override Boolean OnOptionsItemSelected(IMenuItem item) {

			if (item.ItemId == Resource.Id.action_create) {
				createNote();
			}

			return base.OnOptionsItemSelected(item);
		}

		private void refresh(){
			notesList = datasource.GetAll ();
			ArrayAdapter adapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, notesList);
			ListAdapter = adapter;
		}

		private void createNote() {
			NoteItem note = new NoteItem ();
			Intent intent = new Intent(this, typeof(NoteEditorActivity));
			intent.PutExtra(NoteItem.KEY, note.Key.ToString());
			intent.PutExtra(NoteItem.TEXT, note.Text);
			StartActivityForResult(intent, EDITOR_ACTIVITY_REQUEST);
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			NoteItem noteItem = notesList [position];
			Intent intent = new Intent (this, typeof(NoteEditorActivity));
			intent.PutExtra (NoteItem.KEY, noteItem.Key.ToString());
			intent.PutExtra (NoteItem.TEXT, noteItem.Text);
			StartActivityForResult(intent, EDITOR_ACTIVITY_REQUEST);
		}

		public override void OnCreateContextMenu (IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo)menuInfo;
			currentNoteId = (int)info.Id;
			menu.Add (0, MENU_DELETE_ID, 0, Resource.String.delete);
		}

		public override bool OnContextItemSelected (IMenuItem item)
		{
			if (item.ItemId == MENU_DELETE_ID) {
				NoteItem note = notesList [currentNoteId];
				datasource.Remove (note);
				refresh ();
			}

			return base.OnContextItemSelected (item);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == EDITOR_ACTIVITY_REQUEST && resultCode == Result.Ok) {
				NoteItem note = new NoteItem ();
				note.Key = new Guid(data.GetStringExtra(NoteItem.KEY));
				note.Text = data.GetStringExtra(NoteItem.TEXT);
				datasource.Update (note);
				refresh ();
			}
		}
	}
}