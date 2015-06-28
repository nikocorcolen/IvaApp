using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ComponentPro;

namespace ComponentProSamples
{
	/// <summary>
	/// Main activity of the app.
	/// </summary>
	[Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/logo")]
	public class MainActivity : Activity
	{
		/// <summary>
		/// Prepares main activity.
		/// </summary>
		protected override void OnCreate(Bundle bundle)
		{


			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			// Set button click handlers
			var btnNew = (Button)FindViewById(Resource.Id.btnNew);
			btnNew.Click += NewMessageClicked;

			BuildMessageList ();
		}

		void BuildMessageList()
		{
			var list = (ListView)FindViewById(Resource.Id.lwFiles);
			var messageAdapter = new CustomImageListItemAdapter(this, Resource.Layout.IconWithTextListViewItem);

			string[] messageFiles = new string[] {
				"Outlook Hello HTML.msg",
				"Outlook Hello plain text.msg",
				"Outlook Hello rich text.msg",
				"Simple MIME HTML.eml",
				"Simple MIME Plain Text.eml",
			};

			// add sample mail messages from assets to list
			foreach (var file in messageFiles)
			{
				if (System.IO.Path.GetExtension(file) == ".eml")
					messageAdapter.Add(new CustomImageListItem(file, Resource.Drawable.eml));
				else
					messageAdapter.Add(new CustomImageListItem(file, Resource.Drawable.Msg));
			}

			list.Adapter = messageAdapter;
			list.ItemClick += MessageListItemClicked;
		}

		/// <summary>
		/// Handler of New Message button click. Starts ResultActivity activity.
		/// </summary>
		private void NewMessageClicked(object sender, EventArgs e)
		{
			// Start the MessageComposer activity.
			var result = new Intent(this, typeof(MessageComposerActivity));

			StartActivity(result);
		}

		/// <summary>
		/// Raised when user clicks on an item of the message list.
		/// </summary>
		private void MessageListItemClicked(object sender, AdapterView.ItemClickEventArgs e)
		{
			// Get the message name.
			var messageName = ((TextView)e.View.FindViewById(Resource.Id.lblName)).Text;

			// Start the MessageComposer activity and pass the name of the message to compose and send later.
			var intent = new Intent(this, typeof(MessageComposerActivity));
			intent.PutExtra("filename", messageName);
			StartActivity(intent);
		}
	}
}

