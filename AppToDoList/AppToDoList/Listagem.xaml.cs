using AppToDoList.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppToDoList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {
        public Listagem()
        {
            InitializeComponent();          
        }

        protected override void OnAppearing()
        {
            ObservableCollection<Tarefa> tarefas = new ObservableCollection<Tarefa>();

            System.Threading.Tasks.Task.Run(async () =>
            {
                List<Tarefa> temp = await App.Database.GetAllRows();

                foreach (Tarefa item in temp)
                {
                    tarefas.Add(item);
                }
                
                atualizando.IsRefreshing = false;
            });

            lista.ItemsSource = tarefas;
        }
        
        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<Tarefa> tarefas = new ObservableCollection<Tarefa>();

            MenuItem disparador = (MenuItem)sender;

            Tarefa tarefa_selecionada = (Tarefa)disparador.BindingContext;

            bool confirmacao = await DisplayAlert("Tem ctza?", "Remover a Tarefa?", "Sim", "Não");

            if (confirmacao)
            {
                await System.Threading.Tasks.Task.Run(async () =>
                {
                    await App.Database.Delete(tarefa_selecionada.Id);

                    List<Tarefa> temp = await App.Database.GetAllRows();

                    foreach (Tarefa item in temp)
                    {
                        tarefas.Add(item);
                    }

                    atualizando.IsRefreshing = false;
                });

                lista.ItemsSource = tarefas;
            }
        }

        private void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Tarefa tarela_selecionada = (Tarefa)e.SelectedItem;

            Navigation.PushAsync(new AbrirTarefa
            {
                BindingContext = tarela_selecionada
            });
        }

        private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Tarefa> tarefas = new ObservableCollection<Tarefa>();

            string q = e.NewTextValue;

            Task.Run(async () =>
            {
                List<Tarefa> temp = await App.Database.Search(q);

                foreach (Tarefa item in temp)
                {
                    tarefas.Add(item);
                }

                atualizando.IsRefreshing = false;
            });

            lista.ItemsSource = tarefas;
        }
    }
}
