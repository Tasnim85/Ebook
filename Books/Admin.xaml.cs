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
    public partial class Admin : ContentPage
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuDetail());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (txtUsername.Text == "admin" && txtPassword.Text == "admin")
            {
                Navigation.PushAsync(new HomeAdmin());
            }
            else
            {
                DisplayAlert("Ops..", "Nom d'utilisateur ou mot de passe incorrect!", "ok");
            }
        }
    }
}