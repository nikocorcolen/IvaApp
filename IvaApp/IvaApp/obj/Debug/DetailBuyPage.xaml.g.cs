//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IvaApp.Pages {
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    
    public partial class DetailBuyPage : ContentPage {
        
        private Entry nameEntry;
        
        private Entry facturaEntry;
        
        private DatePicker dateEntry;
        
        private Entry priceEntry;
        
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(DetailBuyPage));
            nameEntry = this.FindByName<Entry>("nameEntry");
            facturaEntry = this.FindByName<Entry>("facturaEntry");
            dateEntry = this.FindByName<DatePicker>("dateEntry");
            priceEntry = this.FindByName<Entry>("priceEntry");
        }
    }
}
