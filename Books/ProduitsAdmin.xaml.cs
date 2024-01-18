using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Books
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProduitsAdmin : ContentPage
    {
        public ProduitsAdmin()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            listProduits.ItemsSource = await App.Database.ObtenirToutProduits();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AjouterProduit());

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var image = sender as Image;
            var parentLayout = image?.Parent as StackLayout; // Assuming the parent is a StackLayout

            if (parentLayout != null)
            {
                var produit = parentLayout.BindingContext as Produit;

                if (produit != null)
                {

                    Navigation.PushAsync(new AjouterProduit(produit));
                }
            }
        }

        private void SwipeItem_Clicked(object sender, EventArgs e)
        { 
            var swipeItem = sender as SwipeItem;
            var produit = swipeItem.CommandParameter as Produit;
            DisplayAlert("Alert!!", "Vous etes sure de supprimer ce produit!", "oui");
            App.Database.SupprimerProduit(produit.Id);
            this.OnAppearing();
        }
    }
}