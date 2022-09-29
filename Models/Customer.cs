using System;
using System.Collections.Generic;

#nullable disable

namespace PsychologicalCounseling.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            Orders = new HashSet<Order>();
            Profiles = new HashSet<Profile>();
            Wallets = new HashSet<Wallet>();
        }

        public int Id { get; set; }
        public string Fullname { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? Dob { get; set; }
        public string HourBirth { get; set; }
        public string MinuteBirth { get; set; }
        public string SecondBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
