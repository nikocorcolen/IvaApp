using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ComponentPro;
using ComponentPro.Net;
using ComponentPro.Net.Mail;

namespace ComponentProSamples
{
	/// <summary>
	/// Output activity presents results of 'Run' button click on the main activity.
	/// </summary>
	[Activity(Label = "Mailbox Statistics")]
	public class ResultActivity : Activity
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

		/// <summary>
		/// Handles the exception.
		/// </summary>
		/// <param name="ex">The error object.</param>
		void HandleException(Exception ex, string additionalMessage, bool terminateOnError)
		{
			if (ex.InnerException != null)
				ex = ex.InnerException;

			bool terminate = terminateOnError;
			string msg = null;
			if (ex is ObjectDisposedException)
				terminate = true;
			else 
			{
				NetworkException ne = ex as NetworkException;
				if (ne != null) 
				{
					if (ne.Status == NetworkExceptionStatus.ProtocolError)
						msg = "Error while transferring data between the client and server. ";
					else
						terminate = true;
				}
			}

			EventHandler ev;

			if (terminate)
				ev = (s, e) => {
					SetResult (Result.Ok);
					Finish ();
				};
			else
				ev = null;

			Util.ShowMessage ("IMAP Demo", msg + additionalMessage + "Error: " + ex.Message, this, ev);
		}

		/// <summary>
		/// Retrieves the DNS query result and displays it.
		/// </summary>
		protected override async void OnResume()
		{
			base.OnResume();

			string server = Intent.GetStringExtra ("server");
			int port = Intent.GetIntExtra ("port", 110);
			SecurityMode mode = (SecurityMode)Intent.GetIntExtra ("security", 0);
			string username = Intent.GetStringExtra ("username");
			string password = Intent.GetStringExtra ("password");

			// switch to 'show output' layout
			SetContentView(Resource.Layout.Progress);
			var lblDesc = (TextView)FindViewById(Resource.Id.lblDescription);
			lblDesc.Text = string.Format ("Connecting to POP3 server {0}:{1}...", server, port);

			Pop3 client = null;

			try
			{
				client = new Pop3 ();
				await client.ConnectAsync(server, port, mode);
			}
			catch (Exception ex) 
			{
				HandleException (ex, "Error occurred while connecting to the server. ", true);
				return;
			}

			if (!string.IsNullOrEmpty (username)) 
			{
				try 
				{
					await client.AuthenticateAsync (username, password);
				} 
				catch (Exception ex) 
				{
					HandleException (ex, "Error occurred while authenticating the user. ", true);
					return;
				}
			}

			StringBuilder output = new StringBuilder ();

			try
			{
				Pop3MailboxStat stat = await client.GetMailboxStatAsync();

				output.AppendFormat ("There are {0} message(s). Total size: {1} bytes.\n", stat.MessageCount, stat.Size);
			}
			catch (Exception ex) 
			{
				HandleException (ex, "Error while obtaining mailbox information. ", true);
				return;
			}

			// switch to 'show output' layout
			SetContentView(Resource.Layout.Result);

			// Show output
			var lblResult = (TextView)FindViewById(Resource.Id.lblResult);
			lblResult.Text = output.ToString();
		}
	}
}