using Eje13DatosSqlite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eje13DatosSqlite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPersona : ContentPage
    {
        public ListPersona()
        {
            InitializeComponent();
        }

        private async void Cargar_Registros()

        {
            var registros = await App.DBase.getListPersonas();
            Lista.ItemsSource = registros;
        }


        private async void mItemDelete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmacion", "¿Quiere eliminar el registro?", "Si", "No");
            Debug.WriteLine("Answer: " + answer);
            if (answer == true)
            {
                var id = (Personas)(sender as MenuItem).CommandParameter;
                var result = await App.DBase.DeletePersona(id);

                if (result == 1)
                {
                    await DisplayAlert("Aviso", "Registro Eliminado", "OK");
                    Cargar_Registros();
                }
                else
                {
                    await DisplayAlert("Aviso", "Revisa", "OK");
                }
            };
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();//recargara de nuevo la lista
            Lista.ItemsSource = await App.DBase.getListPersonas();//Espera coleccion de elementos para enumerar en la forma que queramos
        }

        private async void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var registro = (Personas)e.Item;


            bool answer = await DisplayAlert("Confirmacion", "¿Desea editar el registro?", "si", "no");
            Debug.WriteLine("answer: " + answer);

            if (answer == true)
            {

                MainPage pag = new MainPage();
                pag.BindingContext = registro;
                await Navigation.PushAsync(pag);
            };

        }
    }
}