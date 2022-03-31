#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoWebApp.DBContexts;
using DemoWebApp.Models;
using Newtonsoft.Json;


namespace DemoWebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly StudentDbContext _context;

        public BooksController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            if (_context.Books.ToList().Count == 0)
            {
                await LoadBookData();
            }
            return View(await _context.Books.ToListAsync());
        }



        // GET: Info Of Author
        [HttpGet]
        public async Task<IActionResult> SearchByBook(string author)
        {
            ViewData["GetBooksDetails"] = author;

            var books = from m in _context.Books select m;

            if (!String.IsNullOrEmpty(author))
            {
                books = books.Where(q => q.Author.ToLower().Contains(author.ToLower()));
            }

            return View("Index", await books.AsNoTracking().ToListAsync());
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookNumber,BookName,Author,lending_Duration")] Books books)
        {
            if (ModelState.IsValid)
            {
                _context.Add(books);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookNumber,BookName,Author,lending_Duration")] Books books)
        {
            if (id != books.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(books);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = await _context.Books.FindAsync(id);
            _context.Books.Remove(books);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        private async Task LoadBookData()
        {
            StreamReader r = new StreamReader("wwwroot/JsonData/Book.json");

            string jsonString = r.ReadToEnd();
            List<Books> books = JsonConvert.DeserializeObject<List<Books>>(jsonString);
            foreach (Books book in books)
            {
                _context.Add(new Books
                {
                    Id = book.Id,
                    BookNumber = book.BookNumber,
                    BookName = book.BookName,
                    Author = book.Author,
                    lending_Duration = book.lending_Duration,
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
