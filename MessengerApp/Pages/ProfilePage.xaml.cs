using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessengerApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public string username { get; set; }
        public ProfilePage()
        {
            BindingContext = this;
            InitializeComponent();
            

            logout.Clicked += (sender, e) =>
            {
                var a = App.Database.RemoveAll();
                App.Current.MainPage = new MainPage();
            };
        }
        protected override void OnAppearing()
        {
            var person = App.Database.GetPeopleAsync().Result.FirstOrDefault();
            Usename.Text = person.username;
            base.OnAppearing();
        }
    }
}