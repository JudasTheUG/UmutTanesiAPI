//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UmutTanesiAPI.Models
{
    using System;
    
    public partial class sp_getChat_Result
    {
        public int messageId { get; set; }
        public int senderId { get; set; }
        public int reciverId { get; set; }
        public string currentMessage { get; set; }
        public System.DateTime messageTime { get; set; }
    }
}