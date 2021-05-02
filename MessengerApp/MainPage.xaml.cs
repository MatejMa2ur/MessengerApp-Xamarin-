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

        public MainPage()
        {
            InitializeComponent();
            
            Login.Clicked += (sender, e) =>
            {
                App.Current.MainPage = new Pages.NavigationPage();
            };
        }
    }
}