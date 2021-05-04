using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using MessengerApp.Models;
using System.Net;
using System.IO;

namespace MessengerApp
{
    public partial class MainPage : ContentPage
    {
        
        public string username { get; set; }
        public string password { get; set; }
        private const string Url = "https://xamarinfinal.azurewebsites.net/api/isreal";
        private readonly HttpClient _client = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
            if (App.Database.GetPeopleAsync().Result.Count != 0)
                App.Current.MainPage = new Pages.NavigationPage();
            BindingContext = this;
            Login.Clicked += (sender, e) =>
            {
                var person = new User();
                person.id = Guid.NewGuid().ToString();
                person.username = username;
                person.password = password;
                bool isReal = JsonConvert.DeserializeObject<bool>(_client.GetStringAsync(Url + $"?name={username}&pass={password}").Result);
                if (isReal)
                {
                    App.Database.SavePersonAsync(person);
                    App.Current.MainPage = new Pages.NavigationPage();
                }
                else
                {
                    pa.Text = "";
                    DisplayAlert("Warning", "Empty or not correct data", "Try Again");
                }
                
            };
        }
        override protected void OnAppearing()
        {
            if(App.Database.GetPeopleAsync().Result.Count != 0)
            {
                App.Current.MainPage = new Pages.NavigationPage();
            }
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new RegisterPage();
        }

    }
}