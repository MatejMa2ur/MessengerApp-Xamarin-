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

namespace MessengerApp
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "https://xamarinfinal.azurewebsites.net/api/Messages";
        private readonly HttpClient _client = new HttpClient();
        private IEnumerable<Message> _messages;

        public MainPage()
        {
            InitializeComponent();
            Send.Clicked += async (sender, e) =>
            {
                string url = $"https://xamarinfinal.azurewebsites.net/api/SendMessage?name={meno.Text}&message={text.Text}";
                await _client.PostAsync(url,null);
                MyListView.ItemsSource = null;
                OnAppearing();
                text.Text = null;
            };
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                OnAppearing();
                return true;
            });
        }

        protected override async void OnAppearing()
        {
            string content = await _client.GetStringAsync(Url);
            IEnumerable<Message> Messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(content);
            //_messages = new IEnumerable<Message>(Messages);
            MyListView.ItemsSource = Messages;
            base.OnAppearing();
        }

    }
}