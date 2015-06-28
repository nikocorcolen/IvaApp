using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ComponentProSamples
{
	/// <summary>
	/// This class provides data for enumeration list.
	/// </summary>
	public class EnumListAdapter<T> : ArrayAdapter<T>		
	{
		public EnumListAdapter(Context context) : base(context, Android.Resource.Layout.SimpleSpinnerItem, (T[])Enum.GetValues(typeof(T)))
		{
			SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
		}		
	}
}

