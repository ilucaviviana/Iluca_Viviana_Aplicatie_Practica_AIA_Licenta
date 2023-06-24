using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class Locatii
    {
        public int IdLocatii { get; set; }
        public int IdUser { get; set; }
        public string Judet { get; set; }
        public string Oras { get; set; }
        public string Strada { get; set; }
        public string Bloc { get; set; }
        public int? Apartament { get; set; }
        public int? Nrlocatari { get; set; }
        public int? Nrapometre { get; set; }

        public virtual Utilizator IdUserNavigation { get; set; }
    }
}
