using System.Text;
using System;
using System.Drawing;
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
	class ReadonlyTextElement : EntryElement
	{
		public ReadonlyTextElement(string caption, string value)
			: base(caption, null, value)
		{
		}

		protected override UITextField CreateTextField (CoreGraphics.CGRect frame)
		{
			var textField = base.CreateTextField (frame);
			textField.Enabled = false;
			textField.AdjustsFontSizeToFitWidth = true;

			return textField;
		}
	}


	/// <summary>
	/// Class representing Message Details GUI.
	/// </summary>
	public class MessageDetailsView: DialogViewController
	{
		AppWindow _window;

		EntryElement _date;
		EntryElement _from;
		EntryElement _to;
		EntryElement _subject;

		UITextView _body;

		public MessageDetailsView (AppWindow window, string fileName):
		base(new RootElement(fileName == null ? "New Message" : fileName), true)
		{
			try
			{
				_window = window;

				MailMessage msg = fileName == null ? null : new MailMessage (Path.Combine (MessageListView.MyMailsDir, fileName));

				_date = new ReadonlyTextElement ("Date", msg == null || msg.Date == null ? DateTime.Now.ToString() : msg.Date.ToString());

				_from = new EntryElement ("From", "Enter from email-address", msg == null ? "" : msg.From.ToString());
				_to = new EntryElement ("To", "Enter recipient email-addresses", msg == null ? "" : msg.To.ToString());
				_subject = new EntryElement ("Subject", "Enter subject", msg == null ? "" : msg.Subject);

				Section header = new Section ("Header") {
					_date,
					_from,
					_to,
					_subject
				};

				Root.Add(header);

				Section bodySection = new Section ("Body");
				_body = new UITextView (new CoreGraphics.CGRect (3, 0, _window.Bounds.Width - 26, 80));
				bodySection.Add (_body);

				if (msg != null)
					_body.Text = msg.BodyText;

				Root.Add (bodySection);

				UIButton connectButton = UIButton.FromType (UIButtonType.System);
				connectButton.SetTitle("Configure SMTP and Send", UIControlState.Normal);
				connectButton.Frame = new CoreGraphics.CGRect (0, 0, _window.Screen.Bounds.Width - 20, 40);
				connectButton.TouchUpInside += Send;

				Section buttonSection = new Section ();
				buttonSection.Add (connectButton);

				Root.Add (buttonSection);
			}
			catch (Exception ex)
			{
				Util.ShowException(ex);
			}
		}

		void Send(object sender, EventArgs e)
		{
			MailMessage msg = new MailMessage ();
			msg.From = _from.Value;
			msg.To = _to.Value;
			msg.Subject = _subject.Value;
			msg.BodyText = _body.Text;

			SmtpConnectView smtp = new SmtpConnectView (_window, msg);

			_window.PushViewController (smtp, true);
		}
	}
}

