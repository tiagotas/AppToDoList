using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using AppToDoList.Helper;
using AppToDoList.Model;

namespace AppToDoList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Tarefa t = new Tarefa();
            t.Descricao = txt_descricao.Text;
            t.Data_Criacao = dtpck_data_criacao.Date;

            App.Database.Save(t);

            DisplayAlert("Sucesso", "Salvou no SQLite", "OK");
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Listagem());
        }
    }
}
