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
    public partial class AjouterCategorie : ContentPage
    {
        private int id;
        private string nom;

        public AjouterCategorie()
        {
            InitializeComponent();
        }

        public AjouterCategorie(Categorie categorie)
        {


            InitializeComponent();
            if (categorie != null)
            {
                txtId.Text = categorie.Id.ToString();
                txtNom.Text = categorie.Nom;
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {


            Categorie categorie = new Categorie();
            if (txtId.Text == null)
            {
                {
                    categorie.Nom = txtNom.Text;
                };
            }
            else
            {
                {
                    categorie.Id = int.Parse(txtId.Text);
                    categorie.Nom = txtNom.Text;
                };
            }

            DisplayAlert("Bien!", "Categorie sauvegardée", "ok");
            await App.Database.SaveCategorieAsync(categorie);

        }
    }
}