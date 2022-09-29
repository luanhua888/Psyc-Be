﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PsychologicalCounseling.Models
{
    public partial class Deposit
    {
        public int Id { get; set; }
        public int? Amount { get; set; }
        public int? WalletId { get; set; }
        public int? ReceiveAccountid { get; set; }
        public string Status { get; set; }

        public virtual ReceiveAccount ReceiveAccount { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}
