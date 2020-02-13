using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Videoteka.Models;

namespace Videoteka.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly VideotekaContext _context;

        public KorisnikController(VideotekaContext context)
        {
            _context = context;
        }


        // GET: Korisnik/Signup
        public IActionResult Signup()
        {
            return View();
        }

        // POST: Korisnik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("Id,Email,Lozinka,Ime,Prezime")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                var temp = _context.Korisnik.SingleOrDefault(x => x.Email == korisnik.Email
                                && x.Lozinka == korisnik.Lozinka);
                if(temp != null)
                {
                    ViewBag.greska = "Korisnik već postoji!";
                    return View(korisnik);
                }
                _context.Add(korisnik);
                await _context.SaveChangesAsync();
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddHours(10);

                Response.Cookies.Append("ID", korisnik.Id.ToString(), option);
                return RedirectToAction("Index","Film");
            }
            return View(korisnik);
        }

        // GET: Korisnik/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Korisnik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Lozinka")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                var loginCheack = _context.Korisnik.SingleOrDefault(x => x.Email == korisnik.Email
                                                && x.Lozinka == korisnik.Lozinka);
                if(loginCheack != null)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddHours(10);

                    Response.Cookies.Append("ID", korisnik.Id.ToString(), option);
                    return RedirectToAction("Index", "Film");
                }
            }
            ViewBag.greska = "Krivo korisničko ime ili lozinka!";
            return View(korisnik);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("ID");
            return RedirectToAction("Index", "Home");
        }
        private bool KorisnikExists(int id)
        {
            return _context.Korisnik.Any(e => e.Id == id);
        }

    }
}
