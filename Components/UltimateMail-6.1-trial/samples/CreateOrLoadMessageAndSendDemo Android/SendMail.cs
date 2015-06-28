using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using ComponentPro;
using ComponentPro.Net;
using ComponentPro.Net.Mail;

namespace ComponentProSamples
{
	/// <summary>
	/// Represents the SMTP connect and send activity of the app.
	/// </summary>
	[Activity(Label = "Connect and Send", MainLauncher = true, Icon = "@drawable/logo")]
	public class SendMailActivity : Activity
	{
		bool _firstLoad = true;
		string _serverName;
		int _port;
		SecurityMode _security;
		string _userName;
		string _password;
		Smtp _client;

		/// <summary>
		/// Prepares main activity.
		/// </summary>
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			Initialize ();
		}

		/// <summary>
		/// Initialize this Intent. This method loads the values for the Security spinner and sets port accordingly.
		/// </summary>
		void Initialize()
		{
			SetContentView(Resource.Layout.SmtpSend);

			// Set button click handlers
			var btnSend = (Button)FindViewById(Resource.Id.btnSend);
			btnSend.Click += SendClicked;

			Spinner spMode = FindViewById<Spinner>(Resource.Id.spSslMode);
			spMode.Adapter = new EnumListAdapter<SecurityMode>(this);

			spMode.ItemSelected += (s, e) =>
			{
				if (!_firstLoad) // Ignore the first call by the Activity. We capture the user's clicks only.
				{
					TextView txtPort = FindViewById<TextView>(Resource.Id.txtPort);
					Spinner sslSpinner = FindViewById<Spinner>(Resource.Id.spSslMode);

					if ((SecurityMode)sslSpinner.SelectedItemPosition == SecurityMode.Implicit)
						txtPort.Text = ComponentPro.Net.Mail.Smtp.DefaultImplicitSslPort.ToString();
					else
						txtPort.Text = ComponentPro.Net.Mail.Smtp.DefaultPort.ToString();
				}
				else
					_firstLoad = false;
			};

			spMode.SetSelection(1);
		}

		/// <summary>
		/// Handler of Send button click. This method checks the connection parameters for error. If everything is good, 
		/// it connect to the SMTP server and send the message.
		/// </summary>
		private async void SendClicked(object sender, EventArgs e)
		{
			// Get the hostname.
			_serverName = ((EditText)FindViewById(Resource.Id.txtServerName)).Text;
			var txtPort = (EditText)FindViewById(Resource.Id.txtPort);
			_security = (SecurityMode)((Spinner)FindViewById (Resource.Id.spSslMode)).SelectedItemPosition;
			_userName = ((EditText)FindViewById(Resource.Id.txtUserName)).Text;
			_password = ((EditText)FindViewById(Resource.Id.txtPassword)).Text;

			// Check parameters.
			if (string.IsNullOrEmpty(_serverName))
			{
				Util.ShowMessage("Empty Server address", "Please specify server to connect to.", this);
				return;
			}

			if (!int.TryParse (txtPort.Text, out _port))
				_port = -1;

			if (_port <= 0 || _port > 655535)
			{
				Util.ShowMessage("Invalid port", "Please specify a valid server port.", this);
				return;
			}

			if (!string.IsNullOrEmpty(_userName))
			{
				if (string.IsNullOrEmpty(_password))
				{
					Util.ShowMessage("Password missing", "Please specify password or clear the username field to connect to server without authenticating.", this);
					return;
				}
			}

			if (!string.IsNullOrEmpty(_password))
			{
				if (string.IsNullOrEmpty(_userName))
				{
					Util.ShowMessage("Username missing", "Please specify username or clear the password field to connect to server without authenticating.", this);
					return;
				}
			}

			// Show 'Progress' layout
			SetContentView (Resource.Layout.Progress);

			#region Send

			bool failed = true;

			if (await ConnectAndSend(_serverName, _port, _security, _userName, _password))
				failed = false;

			#endregion

			// Restore the information.
			Initialize ();

			((EditText)FindViewById (Resource.Id.txtServerName)).Text = _serverName;
			((EditText)FindViewById(Resource.Id.txtPort)).Text = _port.ToString();
			((Spinner)FindViewById (Resource.Id.spSslMode)).SetSelection((int)_security);
			((EditText)FindViewById (Resource.Id.txtUserName)).Text = _userName;
			((EditText)FindViewById (Resource.Id.txtPassword)).Text = _password;

			if (!failed)
				Util.ShowMessage ("Message sent", "Mail message has been sent successfully.", this);
		}

		void HandleException(Exception ex, string additionalMessage)
		{
			Util.ShowMessage ("Error", additionalMessage + ex.Message, this);
		}

		void SetStatus(string text, params object[] args)
		{
			var lblDesc = (TextView)FindViewById(Resource.Id.lblDescription);
			lblDesc.Text = string.Format (text, args);
		}

		/// <summary>
		/// Connects to the SMTP server and sends the saved message asynchronously.
		/// </summary>
		async Task<bool> ConnectAndSend(
			string serverName,
			int port,
			SecurityMode security,
			string userName,
			string password
			)
		{
			try
			{
				_client = new Smtp ();

				SetStatus("Connecting to SMTP server {0}:{1}...", serverName, port);
				// Connect to the SMTP server
				await _client.ConnectAsync(serverName, port, security);
			}
			catch (Exception ex) 
			{
				HandleException (ex, string.Format("Error while connecting to the SMTP server {0}. ", serverName));
				return false;
			}

			// Only authenticate when both username and password are specified.
			if (!string.IsNullOrEmpty (userName) &&
			    !string.IsNullOrEmpty (password)) 
			{
				try
				{
					SetStatus("Authenticating user {0}...", userName);
					// Authenticate the user
					await _client.AuthenticateAsync(userName, password);
				}
				catch (Exception ex) 
				{
					HandleException (ex, string.Format("Error while authenticating user {0}. ", userName));
					return false;
				}
			}

			#region SendMessage

			// Load message from the Intent's data.
			byte[] messageData = Intent.GetByteArrayExtra("message");
			var message = new MailMessage ();
			message.Load (messageData);

			try
			{
				SetStatus("Sending mail message...");
				_client.Send(message);
			}
			catch (Exception ex)
			{
				HandleException(ex, "Error while sending message. ");
				return false;
			}
			finally
			{
				// Disconnect from the SMTP server.
				Disconnect();
			}

			#endregion

			return true;
		}

		/// <summary>
		/// Disconnects from the server.
		/// </summary>
		void Disconnect()
		{
			if (_client != null) 
			{
				try
				{
					_client.Disconnect();
				}
				catch 
				{
				}

				_client.Dispose ();
				_client = null;
			}
		}

		/// <summary>
		/// Disconnects when the activity is no longer visible.
		/// </summary>
		protected override void OnStop()
		{
			Disconnect();

			base.OnStop();
		}

		/// <summary>
		/// Disconnects and set result when Android Back button is pressed.
		/// </summary>
		public override void OnBackPressed()
		{
			Disconnect();
			SetResult(Result.Ok);
			base.OnBackPressed();
		}

		/// <summary>
		/// Finishes the activity.
		/// </summary>
		public override void Finish()
		{
			Disconnect ();

			base.Finish();
		}
	}
}