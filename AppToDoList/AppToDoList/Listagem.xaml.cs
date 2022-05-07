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
    }
}