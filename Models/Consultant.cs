using System;
using System.Collections.Generic;

#nullable disable

namespace PsychologicalCounseling.Models
{
    public partial class Consultant
    {
        public Consultant()
        {
            Shops = new HashSet<Shop>();
            SlotBookings = new HashSet<SlotBooking>();
            Specializations = new HashSet<Specialization>();
            Wallets = new HashSet<Wallet>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string AvartarUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public int? Experience { get; set; }
        public int? Rating { get; set; }
        public string Status { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
        public virtual ICollection<SlotBooking> SlotBookings { get; set; }
        public virtual ICollection<Specialization> Specializations { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
