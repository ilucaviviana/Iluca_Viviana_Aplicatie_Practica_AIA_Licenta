using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class Tarife
    {
        public int IdTarife { get; set; }
        public int Id { get; set; }
        public string PretRetim { get; set; }
        public string PretCuratenie { get; set; }
        public string PretAdmin { get; set; }
        public string PretApa { get; set; }
        public DateTime? TransmitereData { get; set; }

        public virtual Utilizator IdNavigation { get; set; }
    }
}
