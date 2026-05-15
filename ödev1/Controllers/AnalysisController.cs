using System.Security.Claims;
using KeyloggerTespitSistemi.Services;
using KeyloggerTespitSistemi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyloggerTespitSistemi.Controllers;

[Authorize]
public class AnalysisController : Controller
{
    private readonly IAnalysisService _analysisService;
    private readonly IAuditLogService _auditLogService;

    public AnalysisController(IAnalysisService analysisService, IAuditLogService auditLogService)
    {
        _analysisService = analysisService;
        _auditLogService = auditLogService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _analysisService.GetReportsAsync());
    }

    public IActionResult Create()
    {
        return View(new ProcessReportCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProcessReportCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = GetCurrentUserId();
        var result = await _analysisService.CreateAnalysisAsync(model, userId);
        await _auditLogService.LogAsync(userId, "CreateAnalysis", "ProcessReport", result.ProcessReportId, "Yeni savunma amaçlı analiz oluşturuldu.");
        return RedirectToAction(nameof(Details), new { id = result.ProcessReportId });
    }

    public async Task<IActionResult> Details(int id)
    {
        var detail = await _analysisService.GetDetailAsync(id);
        return detail is null ? NotFound() : View(detail);
    }

    private int GetCurrentUserId()
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(value, out var id) ? id : 1;
    }
}
