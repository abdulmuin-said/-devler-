using KlinikVeriSiniflandirmaSistemi.Data;
using KlinikVeriSiniflandirmaSistemi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KlinikVeriSiniflandirmaSistemi.Controllers;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var classStatistics = await _context.ClinicalClasses
            .Select(c => new ClassStatisticViewModel
            {
                ClassName = c.Name,
                RiskLevel = c.RiskLevel,
                ColorCode = c.ColorCode,
                Count = c.ClinicalRecords.Count
            })
            .OrderByDescending(c => c.Count)
            .ToListAsync();

        var model = new DashboardViewModel
        {
            TotalPatients = await _context.Patients.CountAsync(),
            TotalClinicalRecords = await _context.ClinicalRecords.CountAsync(),
            NormalCount = await _context.ClinicalRecords.CountAsync(r => r.ClinicalClass != null && r.ClinicalClass.Name == "Normal"),
            RiskyCount = await _context.ClinicalRecords.CountAsync(r => r.ClinicalClass != null && r.ClinicalClass.Name == "Riskli"),
            EmergencyCount = await _context.ClinicalRecords.CountAsync(r => r.ClinicalClass != null && r.ClinicalClass.Name == "Acil"),
            ClassStatistics = classStatistics,
            LatestClinicalRecords = await _context.ClinicalRecords
                .Include(r => r.Patient)
                .Include(r => r.ClinicalClass)
                .OrderByDescending(r => r.RecordDate)
                .ThenByDescending(r => r.Id)
                .Take(5)
                .ToListAsync()
        };

        return View(model);
    }
}
