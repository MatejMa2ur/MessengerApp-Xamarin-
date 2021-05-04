using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MessengerApp.Models;

namespace MessengerApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagesPage : ContentPage
    {
        private const string Url = "https://xamarinfinal.azurewebsites.net/api/Messages";
        private readonly HttpClient _client = new HttpClient();
        public MessagesPage()
        {
            InitializeComponent();
            
            Send.Clicked += (sender, e) =>
            {
                string url = $"https://xamarinfinal.azurewebsites.net/api/SendMessage?name={meno.Text}&message={text.Text}";
                _client.PostAsync(url,null);
                MyListView.ItemsSource = null;
                LoadMessages();
                text.Text = null;
            };
            

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                LoadMessages();
                return true;
            });
        }

        protected override async void OnAppearing()
        {
            LoadMessages();
            var b = App.Database.GetPeopleAsync().Result;
            meno.Text = b.FirstOrDefault().username;
            base.OnAppearing();
        }

        protected async void LoadMessages()
        {
            string content = await _client.GetStringAsync(Url);
            IEnumerable<Message> Messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(content);

            //_messages = new IEnumerable<Message>(Messages);
            MyListView.ItemsSource = Messages;
        }
    }
}