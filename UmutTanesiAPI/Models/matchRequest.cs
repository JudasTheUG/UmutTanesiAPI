using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmutTanesiAPI.Models
{
    public class matchRequest
    {
        public int requestId { get; set; }
        public int reqTypeId { get; set; }
        public string reqType { get; set; }
        public int helpId { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string userSurname { get; set; }
        public Nullable<int> personCount { get; set; }
        public Nullable<bool> moneyStatus { get; set; }
        public Nullable<bool> ibanStatus { get; set; }
        public Nullable<bool> mobileStatus { get; set; }
        public Nullable<System.DateTime> beginDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }

    }
}