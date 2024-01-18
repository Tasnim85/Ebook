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
    public partial class CategoriesAdmin : ContentPage
    {
        public CategoriesAdmin()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            listCategories.ItemsSource = await App.Database.ObtenirCategories();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AjouterCategorie());

        }

        

        private void SwipeItem_Clicked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var categorie = swipeItem.CommandParameter as Categorie;
            DisplayAlert("Alert!!", "Vous etes sure de supprimer cette categorie!", "oui");
            App.Database.SupprimerCategorie(categorie.Id);
            this.OnAppearing();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var image = sender as Image;
            var parentLayout = image?.Parent as StackLayout; // Assuming the parent is a StackLayout

            if (parentLayout != null)
            {
                var categorie = parentLayout.BindingContext as Categorie;

                if (categorie != null)
                {
                    
                    Navigation.PushAsync(new AjouterCategorie(categorie));
                }
            }
        }

    }
}