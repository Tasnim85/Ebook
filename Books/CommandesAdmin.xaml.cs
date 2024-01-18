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
    public partial class CommandesAdmin : ContentPage
    {
        public CommandesAdmin()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            listeCommande.ItemsSource = await App.Database.ObtenirToutCommande();
        }



        private async void Button_Clicked(object sender, EventArgs e)
        {
            var image = sender as Image;
            var grid = image?.Parent as Grid;
            var commande = grid.BindingContext as Commande;

            string msg = "";
            List<LigneCommande> lc = new List<LigneCommande>(await App.Database.GetLigneCommandesByIds(commande.LignesCommande.Select(c => int.Parse(c.ToString())).ToList()));
            try
            {

                if (commande.LignesCommande != null)
                {
                    foreach (var lignecommande in lc)
                    {
                        msg = msg + "Produit: " + lignecommande.NomProduit + " Quantité: " + lignecommande.Quantite + "\n";
                    }
                }
                else
                {
                    Console.WriteLine("CommandParameter is null");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("wrong 2");
            }
            await DisplayAlert("List de Produit Commandé ", msg, "OK");
        }
    }
}