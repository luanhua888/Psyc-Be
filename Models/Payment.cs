using System;
using System.Collections.Generic;

#nullable disable

namespace PsychologicalCounseling.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Bookings = new HashSet<Booking>();
            Orders = new HashSet<Order>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public double? Among { get; set; }
        public string Status { get; set; }
        public int? CustomerId { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
