using KlinikVeriSiniflandirmaSistemi.Data;
using KlinikVeriSiniflandirmaSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KlinikVeriSiniflandirmaSistemi.Controllers;

public class PatientsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PatientsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? search)
    {
        ViewData["Search"] = search;

        var query = _context.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(p =>
                p.FirstName.ToLower().Contains(term) ||
                p.LastName.ToLower().Contains(term) ||
                p.PatientNumber.ToLower().Contains(term));
        }

        var patients = await query
            .Include(p => p.ClinicalRecords)
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ToListAsync();

        return View(patients);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patient = await _context.Patients
            .Include(p => p.ClinicalRecords)
            .ThenInclude(r => r.ClinicalClass)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (patient is null)
        {
            return NotFound();
        }

        return View(patient);
    }

    public IActionResult Create()
    {
        return View(new Patient { BirthDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Patient patient)
    {
        if (ModelState.IsValid)
        {
            patient.CreatedAt = DateTime.Now;
            _context.Add(patient);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(nameof(Patient.PatientNumber), "Bu hasta numarası zaten kullanılıyor.");
            }
        }

        return View(patient);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patient = await _context.Patients.FindAsync(id);
        if (patient is null)
        {
            return NotFound();
        }

        return View(patient);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Patient patient)
    {
        if (id != patient.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingPatient = await _context.Patients.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (existingPatient is null)
                {
                    return NotFound();
                }

                patient.CreatedAt = existingPatient.CreatedAt;
                _context.Update(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(patient.Id))
                {
                    return NotFound();
                }

                throw;
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(nameof(Patient.PatientNumber), "Bu hasta numarası zaten kullanılıyor.");
            }
        }

        return View(patient);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patient = await _context.Patients
            .Include(p => p.ClinicalRecords)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (patient is null)
        {
            return NotFound();
        }

        return View(patient);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient is not null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool PatientExists(int id)
    {
        return _context.Patients.Any(e => e.Id == id);
    }
}
