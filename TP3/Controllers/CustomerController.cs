using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP3.Models;

namespace TP3.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;
        public CustomerController(AppDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.custumors
                .Include(c => c.Membershiptypes)
                .ToListAsync();
            return View(customers);
        }


        public async Task<IActionResult> Create()
        {
            var members = await _context.Membershiptypes.ToListAsync();
            ViewBag.member = members.Select(members => new SelectListItem()
            {
                Text = members.Name,
                Value = members.Id.ToString()
            });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer c)
        {
            if (!ModelState.IsValid)
            {
                var members = await _context.Membershiptypes.ToListAsync();
                ViewBag.member = members.Select(members => new SelectListItem()
                {
                    Text = members.Name,
                    Value = members.Id.ToString()
                });
                return View();
            }

            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

            c.Id = new int();
            await _context.custumors.AddAsync(c);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.custumors.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            // Get the list of membership types
            var members = await _context.Membershiptypes.ToListAsync();

            // Set the selected value based on the customer's membership type
            ViewBag.member = members.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),
            });

            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.custumors.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.custumors.FindAsync(id);

            if (customer == null)
            {
                return NotFound(); 
            }

            _context.custumors.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return Content("unable to find Id");
            var c = _context.custumors.SingleOrDefaultAsync(c => c.Id == id);
            return View(c);
        }



    }
}
