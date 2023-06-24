using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class Apometre
    {
        public int IdApometre { get; set; }
        public int Iduser { get; set; }
        public string Ap1 { get; set; }
        public string Ap2 { get; set; }
        public string Ap3 { get; set; }
        public string Ap4 { get; set; }
        public DateTime? TransmitereData { get; set; }

        public virtual Utilizator IduserNavigation { get; set; }
    }
}
