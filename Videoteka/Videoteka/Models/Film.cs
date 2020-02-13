using System;
using System.Collections.Generic;

namespace Videoteka.Models
{
    public partial class Film
    {
        public Film()
        {
            Posudbe = new HashSet<Posudbe>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Posudbe> Posudbe { get; set; }
    }
}
