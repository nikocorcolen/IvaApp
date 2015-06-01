using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IvaApp.CustomViews
{
    class BuyAndSellCell : ViewCell
    {
        public BuyAndSellCell()
        {
            var IDLabel = new Label
            {
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Start
            };
            IDLabel.SetBinding(Label.TextProperty, new Binding("IDLabel"));

            var AmountLabel = new Label
            {
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill
            };
            AmountLabel.SetBinding(Label.TextProperty, new Binding("AmountLabel"));

            View = new StackLayout
            {
                Children = { IDLabel, AmountLabel },
                Orientation = StackOrientation.Horizontal
            };
        }
    }
}
