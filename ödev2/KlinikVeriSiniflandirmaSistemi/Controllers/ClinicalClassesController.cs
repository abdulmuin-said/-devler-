using KlinikVeriSiniflandirmaSistemi.Data;
using KlinikVeriSiniflandirmaSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KlinikVeriSiniflandirmaSistemi.Controllers;

public class ClinicalClassesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClinicalClassesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var classes = await _context.ClinicalClasses
            .Include(c => c.ClinicalRecords)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(classes);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var clinicalClass = await _context.ClinicalClasses
            .Include(c => c.ClinicalRecords)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (clinicalClass is null)
        {
            return NotFound();
        }

        return View(clinicalClass);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClinicalClass clinicalClass)
    {
        if (ModelState.IsValid)
        {
            _context.Add(clinicalClass);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(nameof(ClinicalClass.Name), "Bu sınıf adı zaten kullanılıyor.");
            }
        }

        return View(clinicalClass);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var clinicalClass = await _context.ClinicalClasses.FindAsync(id);
        if (clinicalClass is null)
        {
            return NotFound();
        }

        return View(clinicalClass);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ClinicalClass clinicalClass)
    {
        if (id != clinicalClass.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(clinicalClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicalClassExists(clinicalClass.Id))
                {
                    return NotFound();
                }

                throw;
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(nameof(ClinicalClass.Name), "Bu sınıf adı zaten kullanılıyor.");
            }
        }

        return View(clinicalClass);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var clinicalClass = await _context.ClinicalClasses
            .Include(c => c.ClinicalRecords)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (clinicalClass is null)
        {
            return NotFound();
        }

        return View(clinicalClass);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var clinicalClass = await _context.ClinicalClasses
            .Include(c => c.ClinicalRecords)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (clinicalClass is null)
        {
            return RedirectToAction(nameof(Index));
        }

        if (clinicalClass.ClinicalRecords.Any())
        {
            TempData["Error"] = "Bu kategoriye bağlı klinik kayıt olduğu için silinemez.";
            return RedirectToAction(nameof(Index));
        }

        _context.ClinicalClasses.Remove(clinicalClass);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool ClinicalClassExists(int id)
    {
        return _context.ClinicalClasses.Any(e => e.Id == id);
    }
}
