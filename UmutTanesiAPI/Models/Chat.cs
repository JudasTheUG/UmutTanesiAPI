using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmutTanesiAPI.Models
{
    public class Chat
    {
        public int messageId { get; set; }
        public int senderId { get; set; }
        public System.DateTime messageTime { get; set; }
        public string currentMessage { get; set; }
        public int reciverId { get; set; }

        public int otherPerson { get; set; }
    }
}