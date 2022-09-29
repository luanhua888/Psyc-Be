using System;
using System.Collections.Generic;

#nullable disable

namespace PsychologicalCounseling.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ConsultantId { get; set; }

        public virtual Consultant Consultant { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
