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
    public class StudentsController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string searchtext = "")
        {
            if (_context.Students.ToList().Count == 0)
            {
                await LoadStudentData();
            }

            List<Student> students = new List<Student>();
            if (searchtext != "" && searchtext != null)
            {
                students = await _context.Students.Where(_context => _context.Name.Contains(searchtext)).ToListAsync();
            }
            else
            {
                students = await _context.Students.ToListAsync();
            }
            return View(students);
        }

        // GET: Info for specific section details
        [HttpGet]
        public async Task<IActionResult> SearchBySection(string section)
        {
            ViewData["GetStudentsDetails"] = section;

            var students = from m in _context.Students select m;

            if (!String.IsNullOrEmpty(section))
            {
                students = students.Where(s => s.Section.Contains(section));
            }

            return View("Index", await students.AsNoTracking().ToListAsync());
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Birthdate,Email,Section,CGPA")] Student student)
        {
            if (ModelState.IsValid)
            {
                var isIdAlreadyExists = _context.Students.Any(e => e.Id == student.Id);
                if (isIdAlreadyExists)
                {
                    ModelState.AddModelError("Id", "Sorry, there already exists a student with this id. Please select a different id.");
                    return View(student);
                }
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Birthdate,Email,Section,CGPA")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        private async Task LoadStudentData()
        {
            StreamReader r = new StreamReader("wwwroot/JsonData/Student.json");

            string jsonString = r.ReadToEnd();
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(jsonString);
            foreach (Student student in students)
            {
                _context.Add(new Student
                {
                    Id = student.Id,
                    Name = student.Name,
                    //Birthdate = student.Birthdate,
                    Email = student.Email,
                    Section = student.Section,
                    CGPA = student.CGPA,
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
