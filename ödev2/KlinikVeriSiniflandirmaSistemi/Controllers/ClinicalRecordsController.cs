using KlinikVeriSiniflandirmaSistemi.Data;
using KlinikVeriSiniflandirmaSistemi.Models;
using KlinikVeriSiniflandirmaSistemi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KlinikVeriSiniflandirmaSistemi.Controllers;

public class ClinicalRecordsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IClassificationService _classificationService;

    public ClinicalRecordsController(ApplicationDbContext context, IClassificationService classificationService)
    {
        _context = context;
        _classificationService = classificationService;
    }

    public async Task<IActionResult> Index(int? classId, string? riskLevel, DateTime? startDate, DateTime? endDate)
    {
        ViewData["ClassId"] = classId;
        ViewData["RiskLevel"] = riskLevel;
        ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd");
        ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");
        await PrepareFilterListsAsync(classId, riskLevel);

        var query = _context.ClinicalRecords
            .Include(r => r.Patient)
            .Include(r => r.ClinicalClass)
            .AsQueryable();

        if (classId.HasValue)
        {
            query = query.Where(r => r.ClinicalClassId == classId.Value);
        }

        if (!string.IsNullOrWhiteSpace(riskLevel))
        {
            query = query.Where(r => r.ClinicalClass != null && r.ClinicalClass.RiskLevel == riskLevel);
        }

        if (startDate.HasValue)
        {
            query = query.Where(r => r.RecordDate.Date >= startDate.Value.Date);
        }

        if (endDate.HasValue)
        {
            query = query.Where(r => r.RecordDate.Date <= endDate.Value.Date);
        }

        var records = await query
            .OrderByDescending(r => r.RecordDate)
            .ThenByDescending(r => r.Id)
            .ToListAsync();

        return View(records);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var record = await _context.ClinicalRecords
            .Include(r => r.Patient)
            .Include(r => r.ClinicalClass)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (record is null)
        {
            return NotFound();
        }

        return View(record);
    }

    public async Task<IActionResult> Create()
    {
        await PreparePatientListAsync();
        return View(new ClinicalRecord { RecordDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClinicalRecord record)
    {
        if (ModelState.IsValid)
        {
            var className = _classificationService.Classify(record);
            var clinicalClass = await _context.ClinicalClasses.FirstOrDefaultAsync(c => c.Name == className);

            if (clinicalClass is null)
            {
                ModelState.AddModelError(string.Empty, "Sınıflandırma kategorisi bulunamadı. Önce varsayılan kategorileri ekleyin.");
            }
            else
            {
                record.ClinicalClassId = clinicalClass.Id;
                _context.Add(record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        await PreparePatientListAsync(record.PatientId);
        return View(record);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var record = await _context.ClinicalRecords.FindAsync(id);
        if (record is null)
        {
            return NotFound();
        }

        await PreparePatientListAsync(record.PatientId);
        return View(record);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ClinicalRecord record)
    {
        if (id != record.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var className = _classificationService.Classify(record);
            var clinicalClass = await _context.ClinicalClasses.FirstOrDefaultAsync(c => c.Name == className);

            if (clinicalClass is null)
            {
                ModelState.AddModelError(string.Empty, "Sınıflandırma kategorisi bulunamadı. Önce varsayılan kategorileri ekleyin.");
            }
            else
            {
                try
                {
                    record.ClinicalClassId = clinicalClass.Id;
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinicalRecordExists(record.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
            }
        }

        await PreparePatientListAsync(record.PatientId);
        return View(record);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var record = await _context.ClinicalRecords
            .Include(r => r.Patient)
            .Include(r => r.ClinicalClass)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (record is null)
        {
            return NotFound();
        }

        return View(record);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var record = await _context.ClinicalRecords.FindAsync(id);
        if (record is not null)
        {
            _context.ClinicalRecords.Remove(record);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task PreparePatientListAsync(int? selectedPatientId = null)
    {
        var patients = await _context.Patients
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .Select(p => new
            {
                p.Id,
                FullName = p.FirstName + " " + p.LastName + " - " + p.PatientNumber
            })
            .ToListAsync();

        ViewBag.PatientId = new SelectList(patients, "Id", "FullName", selectedPatientId);
    }

    private async Task PrepareFilterListsAsync(int? selectedClassId, string? selectedRiskLevel)
    {
        var classes = await _context.ClinicalClasses.OrderBy(c => c.Name).ToListAsync();
        ViewBag.ClassId = new SelectList(classes, "Id", "Name", selectedClassId);

        var riskLevels = await _context.ClinicalClasses
            .Select(c => c.RiskLevel)
            .Distinct()
            .OrderBy(r => r)
            .ToListAsync();

        ViewBag.RiskLevel = new SelectList(riskLevels, selectedRiskLevel);
    }

    private bool ClinicalRecordExists(int id)
    {
        return _context.ClinicalRecords.Any(e => e.Id == id);
    }
}
