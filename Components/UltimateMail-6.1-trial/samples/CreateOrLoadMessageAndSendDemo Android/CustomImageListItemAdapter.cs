using System;
using System.Collections.Generic;
using System.Text;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace ComponentProSamples
{
	/// <summary>
	/// Represents an adapter item with an icon and text.
	/// </summary>
	public class CustomImageListItem
	{
		/// <summary>
		/// Id of icon resource.
		/// </summary>
		public int IconResourceId { get; private set; }

		/// <summary>
		/// Display text for item.
		/// </summary>
		public string Text { get; private set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="text">Display text for item.</param>
		/// <param name="iconResourceId">Id of icon resource.</param>
		public CustomImageListItem(string text, int iconResourceId)
		{
			Text = text;
			IconResourceId = iconResourceId;
		}
	}

	/// <summary>
	/// Adapter for displaying items with an icon and text.
	/// </summary>
	public class CustomImageListItemAdapter : ArrayAdapter<CustomImageListItem>
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public CustomImageListItemAdapter(Context context, int textViewResourceId)
			: base(context, textViewResourceId) { }

		/// <summary>
		/// Gets view for current item. Sets icon and text based on item position.
		/// </summary>
		public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			// get container item based on position
			var currentItem = (CustomImageListItem)GetItem(position);

			// create view for the current item
			var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
			var view = inflater.Inflate(Resource.Layout.IconWithTextListViewItem, parent, false);

			// set icon and text for the current item
			var imgItemIcon = view.FindViewById<ImageView>(Resource.Id.imgIcon);
			var txtItemName = view.FindViewById<TextView>(Resource.Id.lblName);

			imgItemIcon.SetImageResource(currentItem.IconResourceId);
			txtItemName.Text = currentItem.Text;

			// store the item text in a view tag
			view.SetTag(Resource.Id.ItemName, new Java.Lang.String(currentItem.Text));

			// disable long clicks
			view.LongClickable = false;

			return view;
		}
	}
}
