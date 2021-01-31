using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmutTanesiAPI.Models
{
    public class Message
    {
        public int sender { get; set; }
        public int reciver { get; set; }
        public string message { get; set; }

    }
}