using TP3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TP3.Controllers
{
    public class MembershiptypesController : Controller
    {
        private readonly AppDbContext _context;

        public MembershiptypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Membershiptypes
        public async Task<IActionResult> Index()
        {
              return _context.Membershiptypes != null ? 
                          View(await _context.Membershiptypes.ToListAsync()) :
                          Problem("Entity set 'GL3FrameworksContext.Membershiptypes'  is null.");
        }

        // GET: Membershiptypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Membershiptypes == null)
            {
                return NotFound();
            }

            var membershiptype = await _context.Membershiptypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershiptype == null)
            {
                return NotFound();
            }

            return View(membershiptype);
        }

        // GET: Membershiptypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Membershiptypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddMembershipDTO membershiptype)
        {
            if (ModelState.IsValid)
            {
                var membershiptypeToAdd = new Membershiptype {
                    Name = membershiptype.Name,
                    SignUpFee = membershiptype.SignUpFee,
                    DiscountRate = membershiptype.DiscountRate,
                    DurationInMonth = membershiptype.DurationInMonth,
                };

                _context.Add(membershiptypeToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membershiptype);
        }

        // GET: Membershiptypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Membershiptypes == null)
            {
                return NotFound();
            }

            var membershiptype = await _context.Membershiptypes.FindAsync(id);
            if (membershiptype == null)
            {
                return NotFound();
            }
            var membershipDto = new AddMembershipDTO {
                Name = membershiptype.Name,
                SignUpFee = membershiptype.SignUpFee,
                DiscountRate = membershiptype.DiscountRate,
                DurationInMonth = membershiptype.DurationInMonth,
            };
            return View(membershipDto);
        }

        // POST: Membershiptypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddMembershipDTO membershiptype)
        {
            if (id != membershiptype.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var membershiptypeInDb = await _context.Membershiptypes
                        .FirstOrDefaultAsync(m => m.Id == membershiptype.ID);
                    
                    if (membershiptypeInDb == null)
                    {
                        return NotFound();
                    }
                    membershiptypeInDb.Name = membershiptype.Name;
                    membershiptypeInDb.SignUpFee = membershiptype.SignUpFee;
                    membershiptypeInDb.DiscountRate = membershiptype.DiscountRate;
                    membershiptypeInDb.DurationInMonth = membershiptype.DurationInMonth;

                    _context.Update(membershiptypeInDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershiptypeExists(membershiptype.ID))
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
            return View(membershiptype);
        }

        // GET: Membershiptypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Membershiptypes == null)
            {
                return NotFound();
            }

            var membershiptype = await _context.Membershiptypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershiptype == null)
            {
                return NotFound();
            }

            return View(membershiptype);
        }

        // POST: Membershiptypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Membershiptypes == null)
            {
                return Problem("Entity set 'GL3FrameworksContext.Membershiptypes'  is null.");
            }
            var membershiptype = await _context.Membershiptypes.FindAsync(id);
            if (membershiptype != null)
            {
                _context.Membershiptypes.Remove(membershiptype);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershiptypeExists(int id)
        {
          return (_context.Membershiptypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
