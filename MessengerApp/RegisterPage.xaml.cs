using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessengerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            Register.Clicked += Register_Clicked;
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            string IsDone = "";
            if (pas.Text != sp.Text)
            {

            }
            else
            {
                string url = "https://xamarinfinal.azurewebsites.net/api/Register";
                HttpClient _client = new HttpClient();
                IsDone = _client.GetStringAsync(url + $"?name={usr.Text}&pass={pas.Text}").Result;

                var person = new MessengerApp.Models.User();
                person.id = Guid.NewGuid().ToString();
                person.username = usr.Text;
                person.password = pas.Text;

                labl.Text = "|" + IsDone + "|";

                if (IsDone == "true")
                {
                    await App.Database.SavePersonAsync(person);
                    App.Current.MainPage = new Pages.NavigationPage();
                }
            }
            Register.Text = IsDone;
        }
    }
}