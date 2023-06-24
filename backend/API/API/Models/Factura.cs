using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class Factura
    {
        public int IdFactura { get; set; }
        public int IdUtilizator { get; set; }
        public decimal? TotalApa { get; set; }
        public decimal? TotalRetim { get; set; }
        public decimal? TotalAdmin { get; set; }
        public decimal? TotalCuratenie { get; set; }
        public decimal? ApaUsage { get; set; }
        public decimal? RetimUsage { get; set; }
        public decimal? AdminUsage { get; set; }
        public decimal? CuratenieUsage { get; set; }
        public DateTime? TransmitereData { get; set; }

       // public bool IsPaid { get; set; } // Added field

        public virtual Utilizator IdUtilizatorNavigation { get; set; }
    }
}
