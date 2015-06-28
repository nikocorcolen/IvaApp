using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ComponentPro;
using ComponentPro.Net;

namespace ComponentProSamples
{
	/// <summary>
	/// Main activity of the app.
	/// </summary>
	[Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/logo")]
	public class MainActivity : Activity
	{
		bool _firstLoad = true;

		#region Properties

		/// <summary>
		/// Gets or sets the server port.
		/// </summary>
		public int Port 
		{
			get
			{
				TextView txtPort = FindViewById<TextView>(Resource.Id.txtPort);
				int port;
				if (!int.TryParse(txtPort.Text, out port))
					port = -1;

				return port;
			}
			set
			{
				TextView port = FindViewById<TextView>(Resource.Id.txtPort);
				port.Text = value.ToString();
			}
		}

		/// <summary>
		/// Gets or sets the security mode.
		/// </summary>
		public SecurityMode SslMode
		{
			get
			{
				Spinner sslMode = FindViewById<Spinner>(Resource.Id.spSslMode);
				return (SecurityMode)sslMode.SelectedItemPosition;
			}
			set
			{
				int selectedItemPosition = (int)value;
				Spinner sslMode = FindViewById<Spinner>(Resource.Id.spSslMode);				
				sslMode.SetSelection(selectedItemPosition, false);
			}
		}

		#endregion

		/// <summary>
		/// Prepares main activity.
		/// </summary>
		protected override void OnCreate(Bundle bundle)
		{


			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			// Set button click handlers
			var btnConnect = (Button)FindViewById(Resource.Id.btnConnect);
			btnConnect.Click += ConnectClicked;

			Spinner spMode = FindViewById<Spinner>(Resource.Id.spSslMode);
			spMode.Adapter = new EnumListAdapter<SecurityMode>(this);

			spMode.ItemSelected += (s, e) =>
			{
				if (!_firstLoad) // Ignore the first call by the Activity. We capture the user's clicks only.
				{
					if (SslMode == SecurityMode.Implicit)
						Port = ComponentPro.Net.Mail.Pop3.DefaultImplicitSslPort;
					else
						Port = ComponentPro.Net.Mail.Pop3.DefaultPort;
				}
				else
					_firstLoad = false;
			};

			spMode.SetSelection(1);
		}

		/// <summary>
		/// Handler of Run button click. Starts ResultActivity activity.
		/// </summary>
		private void ConnectClicked(object sender, EventArgs e)
		{
			// Get the hostname.
			var txtServerName = (EditText)FindViewById(Resource.Id.txtServerName);
			var txtPort = (EditText)FindViewById(Resource.Id.txtPort);
			var security = (Spinner)FindViewById (Resource.Id.spSslMode);
			var txtUserName = (EditText)FindViewById(Resource.Id.txtUserName);
			var txtPassword = (EditText)FindViewById(Resource.Id.txtPassword);

			// Check parameters.
			if (string.IsNullOrEmpty(txtServerName.Text))
			{
				Util.ShowMessage("Server address cannot be empty.", "Please specify server to connect to.", this);
				return;
			}

			int port = int.Parse (txtPort.Text);

			if (port <= 0)
			{
				Util.ShowMessage("Invalid port.", "Please specify a valid server port.", this);
				return;
			}

			// Prepare and start Result activity
			var result = new Intent(this, typeof(ResultActivity));
			result.PutExtra("server", txtServerName.Text);
			result.PutExtra("port", port);
			result.PutExtra("security", security.SelectedItemPosition);
			result.PutExtra("username", txtUserName.Text);
			result.PutExtra("password", txtPassword.Text);

			StartActivity(result);
		}
	}
}

