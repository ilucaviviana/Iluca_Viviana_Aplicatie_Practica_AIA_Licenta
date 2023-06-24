using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class Utilizator
    {
        public Utilizator()
        {
            Apometre = new HashSet<Apometre>();
            Factura = new HashSet<Factura>();
            Locatii = new HashSet<Locatii>();
            Tarife = new HashSet<Tarife>();
        }

        public int Id { get; set; }
        public int? Tip { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public string Cnp { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Apometre> Apometre { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<Locatii> Locatii { get; set; }
        public virtual ICollection<Tarife> Tarife { get; set; }
    }
}
