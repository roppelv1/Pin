using System;
using System.Collections.Generic;

namespace Videoteka.Models
{
    public partial class Posudbe
    {
        public int Id { get; set; }
        public int KorisnikId { get; set; }
        public int FilmId { get; set; }
        public DateTime DatumPosudbe { get; set; }

        public virtual Film Film { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }
}
