using System;

using MonoTouch.Dialog;
using UIKit;
using Foundation;

namespace ComponentProSamples
{
	public class Program
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			UIApplication.Main(args, null, "AppDelegate");
		}
	}

	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			var window = new AppWindow();

			// create and start the main view controller.
			window.Start(new MessageListView(window));
			return true;
		}
	}
}
