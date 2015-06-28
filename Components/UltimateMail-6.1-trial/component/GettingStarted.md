There are two Ultimate Mail components for use with Xamarin, UltimateMail for iOS and UltimateMail for Android. Both use the same API.

UltimateMail for .NET is a 100%-managed C# class library consisting feature-rich and easy-to-use IMAP, POP3, and SMTP classes for creating, sending, receiving, and processing e-mails. It provides access to the complete functionality of the IMAP, POP3, and SMTP protocols through a straightforward, intuitive object model. The library offers the flexibility, ease of use and rapid development features of a component without the complexities of working with the native socket class or in-depth knowledge of how the protocols are implemented.

## Generating your Trial License Key
To use the component, please generate your 30-day trial key at: (http://www.componentpro.com/download/trial.aspx?product=Mail)

This document provides a short guide to getting started.

Xamarin.iOS
-----------

<b>Download mail message from a POP3 server in an iOS application</b> 

	using System;
	using ComponentPro.Net;
	using ComponentPro.Net.Mail;

	...

	// POP3 server information. 
	const string serverName = "myserver";
	const string user = "name@domain.com";
	const string password = "mytestpassword";
	const int port = 995;
	const SecurityMode securityMode = SecurityMode.Implicit;

	// Create a new instance of the Pop3 class.
	Pop3 client = new Pop3();

	// Connect to the server.
	client.Connect(serverName, port, securityMode);

	// Login to the server.
	client.Authenticate(user, password);

	// Get the message list.
	Console.WriteLine("Getting message list...");
	Pop3MessageCollection list = client.ListMessages(Pop3EnvelopeParts.MessageInboxIndex | Pop3EnvelopeParts.Size);

	// Get messages. 
	for (int i = 0; i < list.Count; i++)
	{
		Pop3Message pop3Message = list[i];

		// Download the message to an instance of the MailMessage class. 
		MailMessage msg = client.DownloadMailMessage(pop3Message.MessageInboxIndex);

		// Display some information about it. 
		Console.WriteLine("Size: " + pop3Message.Size);
		Console.WriteLine("Number of attachments: " + msg.Attachments.Count);
		Console.WriteLine("Number of header name value pairs: " + msg.Headers.Count);
	}

	// Close the connection.
	client.Disconnect();


Xamarin.Android
---------------

<b>Send a mail message in an Android application</b> 

	using System;
	using System.Windows.Forms;
	using ComponentPro.Net;
	using ComponentPro.Net.Mail;

	...

	const string serverName = "myserver";
	const string user = "name@domain.com";
	const string password = "mytestpassword";
	const int port = 465;
	const SecurityMode securityMode = SecurityMode.Implicit;

	Smtp client = new Smtp();
	try
	{
		MailMessage mmMessage = new MailMessage();
		mmMessage.From.Add("from@thedomain.com");
		mmMessage.To.Add("name@domain.com");
		mmMessage.To.Add("someone@domain.com");
		mmMessage.Cc.Add("someone2@domain.com");
		mmMessage.Bcc.Add("someone3@domain.com");
		mmMessage.Subject = "Test Subject";
		mmMessage.BodyText = "Test Content";

		Console.WriteLine("Connecting SMTP server: {0}:{1}...", serverName, port);
		// Connect to the server. 
		client.Connect(serverName, port, securityMode);

		// Login to the server. 
		Console.WriteLine("Logging in as {0}...", user);
		client.Authenticate(user, password);

		Console.WriteLine("Sending mail message...");
		client.Send(mmMessage);
		Console.WriteLine("Message sent...");

		// Disconnect. 
		Console.WriteLine("Disconnecting...");
		client.Disconnect();
	}
	catch (SmtpException smtpExc)
	{
		MessageBox.Show(string.Format("An SMTP error occurred: {0}, ErrorStatus: {1}", smtpExc.Message, smtpExc.Status));
	}
	catch (Exception exc)
	{
		MessageBox.Show(string.Format("An error occurred: {0}", exc.Message));
	}


## Other Resources

* [ComponentPro Support forums](http://www.componentpro.com/forums/)
* [Product Documentation](http://www.componentpro.com/doc/mail/)
* [UltimateMail Product Page](http://www.componentpro.com/mail.netcf/)
* [IMAP Features Page](http://www.componentpro.com/imap.netcf/)
* [POP3 Features Page](http://www.componentpro.com/pop3.netcf/)
* [SMTP Features Page](http://www.componentpro.com/smtp.netcf/)
