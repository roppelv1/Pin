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
    }
}
