using System;

namespace WIF.PropertySerialization.Models
{
    public class User
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime LastLoggedIn { get; set; }
        public bool IsBlocked { get; set; }
        public string InternalNotesAboutUser { get; set; }
    }
}