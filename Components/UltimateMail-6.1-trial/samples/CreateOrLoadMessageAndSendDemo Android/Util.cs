using System;
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

namespace ComponentProSamples
{
	/// <summary>
	/// Contains some utility methods.
	/// </summary>
	public class Util
	{
		/// <summary>
		/// Shows an exception to the user.
		/// </summary>
		/// <param name="title">Dialog title.</param>
		/// <param name="error">Exception.</param>
		public static void ShowError(string title, Exception error, Activity context)
		{
			ShowError (title, error, context, null);
		}

		/// <summary>
		/// Displays an exception to the user.
		/// </summary>
		/// <param name="title">Dialog title.</param>
		/// <param name="error">Exception.</param>
		public static void ShowError(string title, Exception error, Activity context, EventHandler dismissHandler)
		{
			if (context.IsFinishing)
				return;

			while (error is AggregateException)
				error = error.InnerException;

			ShowMessage(title, error.ToString(), context, dismissHandler);
		}

		/// <summary>
		/// Displays a message to the user.
		/// </summary>
		public static void ShowMessage(string title, string message, Activity context)
		{
			ShowMessage (title, message, context, null);
		}

		/// <summary>
		/// Displays a message to the user.
		/// </summary>
		public static void ShowMessage(string title, string message, Activity context, EventHandler dismissHandler)
		{
			if (context.IsFinishing)
				return;

			context.RunOnUiThread(() =>
			{
				AlertDialog dialog = new AlertDialog.Builder(context)
					.SetTitle(title)
					.SetMessage(message)
					.Create();

				if (dismissHandler != null)
					dialog.DismissEvent += dismissHandler;

				dialog.Show();
			}
			);
		}
	}
}