using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

using MonoTouch.Dialog;
using Foundation;
using UIKit;

using ComponentPro.Net.Mail;
using ComponentPro.Net;

namespace ComponentProSamples
{
	/// <summary>
	/// Class representing Message List GUI.
	/// </summary>
	public class MessageListView: DialogViewController
	{
		AppWindow _window;

		const string MailsDir = "Mails";

		/// <summary>
		/// Gets the documents dir.
		/// </summary>
		/// <value>The documents dir.</value>
		public static string DocumentsDir
		{
			get 
			{
				return Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			}
		}

		/// <summary>
		/// Gets the directory path that store the temporary messages.
		/// </summary>
		public static string MyMailsDir
		{
			get 
			{
				return Path.Combine (DocumentsDir, MailsDir);
			}
		}

		static UIImage _imgMsg = UIImage.FromFile ("Msg.png");
		static UIImage _imgEml = UIImage.FromFile ("Eml.png");

		public MessageListView (AppWindow window):
		base(new RootElement("Select"), true)
		{
			_window = window;

			CopyMessages ();
			ListMessages ();
		}

		void CopyMessages()
		{
			try
			{
				string[] names = Directory.GetFiles("./Mails");

				// Create the temp mails dir if it does not exist.
				if (!Directory.Exists(MyMailsDir))
				{
					Directory.CreateDirectory(MyMailsDir);
				}

				for (int i = 0; i < names.Length; i++)
				{
					string fileName = Path.GetFileName(names[i]);
					string newPath = Path.Combine(MyMailsDir, fileName);

					File.Copy(names[i], newPath, true);
				}
			}
			catch (Exception ex) 
			{
				Util.ShowException (ex);
			}
		}

		void ListMessages()
		{
			string[] messages = Directory.GetFiles (MyMailsDir);

			Section mails = new Section ("Select a template");

			ImageStringElement item = new ImageStringElement ("New Message...", () => ItemClicked(null), _imgEml);
			item.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			mails.Add (item);

			for (int i = 0; i < messages.Length; i++) 
			{
				string fileName = Path.GetFileName (messages [i]);
				UIImage img = Path.GetExtension (fileName) == ".msg" ? _imgMsg : _imgEml;
				item = new ImageStringElement (fileName, () => ItemClicked(fileName), img);												
				item.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				mails.Add (item);
			}

			Root.Add (mails);
		}

		void ItemClicked(string fileName)
		{
			var messageDetails = new MessageDetailsView(_window, fileName);
			_window.PushViewController(messageDetails, true);
		}
	}
}

