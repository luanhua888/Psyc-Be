using System;
using System.Collections.Generic;

#nullable disable

namespace PsychologicalCounseling.Models
{
    public partial class SlotBooking
    {
        public int SlotId { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public DateTime? DateBooking { get; set; }
        public string Status { get; set; }
        public int? BookingId { get; set; }
        public int? ConsultantId { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Consultant BookingNavigation { get; set; }
    }
}
