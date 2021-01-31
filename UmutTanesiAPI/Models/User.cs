using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmutTanesiAPI.Models
{
    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string userSurname { get; set; }
        public string email { get; set; }
        public string iban { get; set; }
        public string mobile { get; set; }
        public Nullable<System.DateTime> birthdate { get; set; }
        public string userPassword { get; set; }
    }
}