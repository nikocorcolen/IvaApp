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
    
    
    public partial class DetailSellP : ContentPage {
        
        private Label productName;
        
        private Label factura;
        
        private Label dateProduct;
        
        private Label neto;
        
        private Label iva;
        
        private Label total;
        
        private Button eliminarButton;
        
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(DetailSellP));
            productName = this.FindByName<Label>("productName");
            factura = this.FindByName<Label>("factura");
            dateProduct = this.FindByName<Label>("dateProduct");
            neto = this.FindByName<Label>("neto");
            iva = this.FindByName<Label>("iva");
            total = this.FindByName<Label>("total");
            eliminarButton = this.FindByName<Button>("eliminarButton");
        }
    }
}
