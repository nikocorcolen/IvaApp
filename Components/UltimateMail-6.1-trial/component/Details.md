The Ultimate Mail Component offers a comprehensive interface for sending, receiving e-mail messages from a server and managing your mailbox remotely, all from within your application. In addition, it also allows you to compose, decrypt, encrypt, sign, and validate mail messages.

The example below shows how to connect to an IMAP server and download mail messages.

	using System;
	using ComponentPro.Net;
	using ComponentPro.Net.Mail;

	...

	// IMAP server information. 
	const string serverName = "myserver";
	const string user = "name@domain.com";
	const string password = "mytestpassword";
	const int port = 993;
	const SecurityMode securityMode = SecurityMode.Implicit;

	// Create a new instance of the Imap class.
	Imap client = new Imap();

	// Connect to the server.
	client.Connect(serverName, port, securityMode);

	// Login to the server.
	client.Authenticate(user, password);

	// Select 'INBOX' mailbox
	client.Select("INBOX");

	// Get the message list.
	Console.WriteLine("Getting message list...");
	ImapMessageCollection list = client.ListMessages(ImapEnvelopeParts.UniqueId | ImapEnvelopeParts.Size, ImapCriterion.DontHaveFlags(ImapMessageFlags.Seen));

	// Get messages. 
	for (int i = 0; i < list.Count; i++)
	{
		ImapMessage imapMessage = list[i];

		// Download the message to an instance of the MailMessage class. 
		MailMessage msg = client.DownloadMailMessage(imapMessage.UniqueId);

		// Display some information about it. 
		Console.WriteLine("Size: " + imapMessage.Size);
		Console.WriteLine("Number of attachments: " + msg.Attachments.Count);
		Console.WriteLine("Number of header name value pairs: " + msg.Headers.Count);
	}

	// Close the connection.
	client.Disconnect();