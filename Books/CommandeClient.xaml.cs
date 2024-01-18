using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Books
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommandeClient : ContentPage

    {
        private decimal totalPrice;
        private decimal totalFacture;

        private bool _isRefreshing;

        public ICommand RefreshCommand { get; set; }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }

        public CommandeClient()
        {
           
            InitializeComponent();
            RefreshCommand = new Command(() => _ = RefreshData());
            foreach (var ap in Panier.Articles)
            {
                totalPrice = totalPrice + ap.PrixTotale;
                
            }
            totalFacture = totalPrice + 10;
        }

        private async Task RefreshData()
        {
            try
            {
                IsRefreshing = true;
                // Convert the List<ArticlePanier> to ObservableCollection<ArticlePanier>
                var observableCollection = new ObservableCollection<ArticlePanier>(Panier.Articles);
                lvPanier.ItemsSource = observableCollection;
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary
                Console.WriteLine($"Error during refresh: {ex.Message}");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        protected override void OnAppearing()
        {
            Total.Text = totalPrice.ToString("C2");
            TotalFacture.Text = totalFacture.ToString("C2");
            _ = RefreshData();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Confirmation", "Voulez-vous vraiment Vider votre panier!", "Oui", "Non");

            if (result)
            {
                Panier.ViderPanier();
                DisplayAlert("", "Votre panier est maintenant vide", "ok");
                _ = RefreshData();
            }
            else
            {
                _ = RefreshData();
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var tappedItem = (ArticlePanier)((TappedEventArgs)e).Parameter;

            bool result = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer cet article!", "Oui", "Non");

            if (result)
            {
                Panier.RetirerArticle(tappedItem.IdProduit);
                DisplayAlert("", "Produit supprimé", "ok");
                _ = RefreshData();
            }
            else
            {
                _ = RefreshData();
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string nomClient = await DisplayPromptAsync("", "Veuillez saisir votre nom:");

            if (string.IsNullOrWhiteSpace(nomClient))
            {
                await DisplayAlert("Erreur", "Le nom ne peut pas être vide.", "OK");
                return;
            }
            bool result = await DisplayAlert("Confirmation", "Voulez-vous vraiment Confirmer votre achat!", "Oui", "Non");
            if (result) 
            {
                List<int> i = new List<int>();
                Commande commande = new Commande();
                foreach (var ArticlePanier in Panier.Articles)
                {
                    LigneCommande ligneCommande = new LigneCommande
                    {
                        IdProduit = ArticlePanier.IdProduit,
                        NomProduit = ArticlePanier.NomProduit,
                        Prix = ArticlePanier.PrixTotale,
                        Quantite = ArticlePanier.Quantite
                    };

                    i.Add(await App.Database.AjouterLigneCommandewithId(ligneCommande));
                }
                string resultString = string.Join("", i);
                commande.LignesCommande = resultString;
                commande.NomClient = nomClient;
                int j = await App.Database.AjouterCommande(commande);


                Panier.ViderPanier();
                await DisplayAlert("", "Succeés d'achat", "OK");
                _ = RefreshData();
            }
            else
            {
                _ = RefreshData();
            }
        }
    }
    
}
