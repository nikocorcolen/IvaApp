using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MonoTouch.Dialog;
using Foundation;
using UIKit;

using ComponentPro.Net.Mail;
using ComponentPro.Net;

namespace ComponentProSamples
{
	/// <summary>
	/// Class representing an IMAP Mailbox Connection GUI.
	/// </summary>
	public class ConnectView : DialogViewController
	{
        // main container for the application
		private AppWindow _window;

		private EntryElement _serverName;
		private EntryElement _serverPort;
		private EntryElement _username;
		private EntryElement _password;
		private RootElement _mode;
		private RadioGroup _sslModes;

		private UIButton _connectButton;
		private Section _propertiesSection;

		public string ServerName
		{
			get { return _serverName.Value; }
		}

		public SecurityMode Mode
		{
			get { return (SecurityMode)_mode.RadioSelected; } 
		}

		public int ServerPort
		{
			get
			{
				ushort value;
				if (!ushort.TryParse(_serverPort.Value, out value))
					return -1;

				return value;
			}
			private set
			{
				_serverPort.Value = value.ToString();
			}
		}

		public string Password
		{
			get { return _password.Value; }
		}

		public string UserName
		{
			get { return _username.Value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConnectView"/> class.
		/// </summary>
		/// <param name="siteName">The remote site name.</param>
		/// <param name="navigation">AppWindow.</param>
		public ConnectView(AppWindow window):
		base(new RootElement("Get IMAP Mailbox Stat"), true)
		{
			// Initialize controller's elements
			_window = window;

			_serverName = new EntryElement("Hostname: ", "Enter server address", "imap.gmail.com");
			Util.DisableAutoCorrectionAndAutoCapitalization(_serverName);

			var noneRadio = new RadioElement ("None", "mode");
			var implicitRadio = new RadioElement ("Implicit", "mode");
			var explicitRadio = new RadioElement ("Explicit", "mode");

			// automatically update port number to SSL mode default
			noneRadio.Tapped += () => { ServerPort = Imap.DefaultPort; };
			explicitRadio.Tapped += () => { ServerPort = Imap.DefaultPort; };
			implicitRadio.Tapped += () => { ServerPort = Imap.DefaultImplicitSslPort; };

			_sslModes = new RadioGroup ("mode", 1);
			_mode = new RootElement ("TLS/SSL mode:", _sslModes) {
				new Section () {
					noneRadio,
					implicitRadio,
					explicitRadio
				}
			};

			string defaultPort = "993";
			_serverPort = new EntryElement("Port: ", "Enter port", defaultPort);
			_serverPort.KeyboardType = UIKit.UIKeyboardType.NumberPad;

			_username = new EntryElement("Username: ", "Enter username", "");
			Util.DisableAutoCorrectionAndAutoCapitalization(_username);

			_password = new EntryElement("Password: ", "Enter password", "", true);
			Util.DisableAutoCorrectionAndAutoCapitalization(_password);

			_propertiesSection = new Section()
			{
				_serverName,
				_mode,
				_serverPort,
				_username,
				_password
			};

			Root.Add(_propertiesSection);

			_connectButton = UIButton.FromType (UIButtonType.System);
			_connectButton.SetTitle("Get Mailbox Info", UIControlState.Normal);
			_connectButton.Frame = new CoreGraphics.CGRect (0, 0, _window.Screen.Bounds.Width - 20, 40);
			_connectButton.TouchUpInside += Connect;

			Section buttonSection = new Section ();
			buttonSection.Add (_connectButton);

			Root.Add (buttonSection);
		}

		/// <summary>
		/// Connect to the server and display mailbox statistics.
		/// </summary>
		private async void Connect(object sender, EventArgs e)
		{
			_window.Busy = true;
			Imap client = null;
			try
			{
				client = new Imap ();
				await client.ConnectAsync(ServerName, ServerPort, Mode);
			}
			catch (Exception ex)
			{
				_window.Busy = false;
				Util.ShowException(ex);
				return;
			}

			if (!string.IsNullOrEmpty(UserName))
			{
				try
				{
					await client.AuthenticateAsync(UserName, Password);
				}
				catch (Exception ex)
				{
					_window.Busy = false;
					Util.ShowException(ex);
					return;
				}
			}

			StringBuilder output = new StringBuilder ();

			try
			{
				await client.SelectAsync("INBOX");

				FolderCollection rootFolders = await client.ListFoldersAsync ();

				output.AppendFormat ("{0} folders in the root folder.\n", rootFolders.Count);
				ImapMessageCollection msgs = await client.ListMessagesAsync (ImapEnvelopeParts.Size);
				long size = 0;
				for (int i = 0; i < msgs.Count; i++) {
					size += msgs [i].Size;
				}
				output.AppendFormat ("{0} messages in INBOX folder. Total size: {1} bytes.\n", msgs.Count, size);
			}
			catch (Exception ex) 
			{
				_window.Busy = false;
				Util.ShowException(ex);
				return;
			}
			_window.Busy = false;
			Util.ShowMessage ("Mailbox Info", output.ToString ());
		}
	}
}
