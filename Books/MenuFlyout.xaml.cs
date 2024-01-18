using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Books
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuFlyout : ContentPage
    {
        public ListView ListView;

        public MenuFlyout()
        {
            InitializeComponent();

            BindingContext = new MenuFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MenuFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuFlyoutMenuItem> MenuItems { get; set; }

            public MenuFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MenuFlyoutMenuItem>(new[]
                {
                    new MenuFlyoutMenuItem { Id = 0, Title = "Nos livres",TargetType=typeof(ProduitsClient), Icon="book.png" },
                    new MenuFlyoutMenuItem { Id = 1, Title = "Panier",TargetType=typeof(CommandeClient), Icon="buy.png" },
                    new MenuFlyoutMenuItem { Id = 2, Title = "Administration",TargetType=typeof(Admin), Icon="admiin.png" },
                   

                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}