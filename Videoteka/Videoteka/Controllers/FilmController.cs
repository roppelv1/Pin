using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Videoteka.Models;

namespace Videoteka.Controllers
{
    public class FilmController : Controller
    {
        private readonly VideotekaContext _context;

        public FilmController(VideotekaContext context)
        {
            _context = context;
        }

        // GET: Film
        public async Task<IActionResult> Index()
        {
            return View(await _context.Film.ToListAsync());
        }

        public async Task<IActionResult> Posudi(int FilmId)
        {
            var posudba = new Posudbe();
            posudba.FilmId = FilmId;
            posudba.KorisnikId = Convert.ToInt32( Request.Cookies["ID"]);
            posudba.DatumPosudbe = DateTime.Now;
            _context.Add(posudba);
            await _context.SaveChangesAsync();
            return View("Index",await _context.Film.ToListAsync());
        }
        public ActionResult PosudeniFilmovi()
        {
            var korisnikID = Convert.ToInt32(Request.Cookies["ID"]);
            return View(_context.Posudbe.Include(m => m.Korisnik).Include(m => m.Film).Where(x=> x.KorisnikId == korisnikID).ToList());
        }
    }
}
