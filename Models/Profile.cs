using System;
using System.Collections.Generic;

#nullable disable

namespace PsychologicalCounseling.Models
{
    public partial class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public string HourBirth { get; set; }
        public string MinuteBirth { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public int? CustomerId { get; set; }
        public int? ZodiacId { get; set; }
        public int? PlanetId { get; set; }
        public int? HouseId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual House House { get; set; }
        public virtual Planet Planet { get; set; }
        public virtual Zodiac Zodiac { get; set; }
    }
}
