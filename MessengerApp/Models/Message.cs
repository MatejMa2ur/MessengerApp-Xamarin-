using System;
using System.Collections.Generic;
using System.Text;

namespace MessengerApp.Models
{
    class Message
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string message { get; set; }
    }
}
