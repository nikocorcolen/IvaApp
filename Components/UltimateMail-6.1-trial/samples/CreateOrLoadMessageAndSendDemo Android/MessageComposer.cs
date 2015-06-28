using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ComponentPro;
using ComponentPro.Net.Mail;
using ComponentPro.Net;

namespace ComponentProSamples
{
	/// <summary>
	/// Activity to compose a mail message.
	/// </summary>
	[Activity(Label = "Compose Message")]
	public class MessageComposerActivity : Activity
	{
		/// <summary>
		/// Prepares the activity.
		/// </summary>
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			if (Intent == null)
			{
				Util.ShowMessage("Error", "Cant find data for the activity.", this);
				Finish();
			}
		}

		MailMessage _message;

		/// <summary>
		/// Retrieves the DNS query result and displays it.
		/// </summary>
		protected override void OnResume()
		{
			base.OnResume();

			// Switch to 'Progress' layout
			SetContentView(Resource.Layout.Progress);

			// Get message file name.
			var filename = Intent.GetStringExtra("filename");

			try
			{
				if (!string.IsNullOrEmpty (filename)) {
					// Show progress information
					var lbl = (TextView)FindViewById (Resource.Id.lblDescription);
					lbl.Text = string.Format ("Loading message '{0}'...", filename);

					// Load the message.
					_message = new MailMessage ();

					// Retrieve the message data as a byte array
					using (var asset = Assets.Open(filename))
					{
						var ms = new MemoryStream();
						asset.CopyTo(ms);
						ms.Seek(0, SeekOrigin.Begin);
						_message.Load (ms);
					}
				} 
				else
					// Create a new message.
					_message = new MailMessage ();

				// Switch to 'MessageComposer' layout
				SetContentView(Resource.Layout.MessageComposer);

				var txtSubject = (EditText)FindViewById(Resource.Id.txtSubject);
				txtSubject.Text = _message.Subject;
				txtSubject.Enabled = true;

				var txtFrom = (EditText)FindViewById(Resource.Id.txtFrom);
				txtFrom.Text = string.Join(",", _message.From);
				txtFrom.Enabled = true;

				var txtTo = (EditText)FindViewById(Resource.Id.txtTo);
				txtTo.Text = string.Join(",", _message.To);
				txtTo.Enabled = true;

				var txtDate = (EditText)FindViewById(Resource.Id.txtDate);
				txtDate.Text = (_message.Date != null) ? _message.Date.ToString() : string.Empty;
				txtDate.Enabled = false;

				var txtBody = (EditText)FindViewById(Resource.Id.txtBody);
				txtBody.Text = _message.BodyText;
				txtBody.Enabled = true;
			}
			catch (Exception ex) 
			{
				Util.ShowError ("Error", ex, this);
				Finish ();
				return;
			}

			// Register the event handler for the "Config and Send" button.
			var btnSend = (TextView)FindViewById(Resource.Id.btnSend);
			btnSend.Click += ConfigClicked;
		}

		void ConfigClicked (object sender, EventArgs e)
		{
			// Start SendMailActivity activity and pass the message data to send.
			var sendIntent = new Intent(this, typeof(SendMailActivity));
			MemoryStream ms = new MemoryStream ();
			byte[] messageData = _message.ToByteArray ();
			sendIntent.PutExtra("message", messageData);
			StartActivity(sendIntent);
		}
	}
}