using System;
using System.Collections.Generic;

namespace Videoteka.Models
{
    public partial class Korisnik
    {
        public Korisnik()
        {
            Posudbe = new HashSet<Posudbe>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public virtual ICollection<Posudbe> Posudbe { get; set; }
    }
}
