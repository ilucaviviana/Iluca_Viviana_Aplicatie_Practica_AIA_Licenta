using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class Facturi
    {
        public int Iduser { get; set; }
        public int Idloc { get; set; }
        public string TotalRece { get; set; }
        public string TotalRetim { get; set; }
        public string Administrator { get; set; }
        public string Curatenie { get; set; }
        public string Impozit { get; set; }
        public string DataPlata { get; set; }
        public string TotalPlata { get; set; }

        public virtual Locatii IdlocNavigation { get; set; }
    }
}
