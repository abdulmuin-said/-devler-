using System.Security.Claims;
using KeyloggerTespitSistemi.Services;
using KeyloggerTespitSistemi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyloggerTespitSistemi.Controllers;

[Authorize(Roles = "Admin")]
public class DetectionRulesController : Controller
{
    private readonly IDetectionRuleService _ruleService;
    private readonly IAuditLogService _auditLogService;

    public DetectionRulesController(IDetectionRuleService ruleService, IAuditLogService auditLogService)
    {
        _ruleService = ruleService;
        _auditLogService = auditLogService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _ruleService.GetAllAsync());
    }

    public IActionResult Create()
    {
        return View("Form", new DetectionRuleFormViewModel());
    }

    public async Task<IActionResult> Edit(int id)
    {
        var rule = await _ruleService.GetByIdAsync(id);
        if (rule is null)
        {
            return NotFound();
        }

        return View("Form", new DetectionRuleFormViewModel
        {
            Id = rule.Id,
            Name = rule.Name,
            Description = rule.Description,
            RuleType = rule.RuleType,
            RiskPoint = rule.RiskPoint,
            IsActive = rule.IsActive
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(DetectionRuleFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", model);
        }

        await _ruleService.SaveAsync(model);
        await _auditLogService.LogAsync(GetCurrentUserId(), "SaveRule", "DetectionRule", model.Id, "Detection rule kaydedildi.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        await _ruleService.ToggleAsync(id);
        await _auditLogService.LogAsync(GetCurrentUserId(), "ToggleRule", "DetectionRule", id, "Detection rule aktif/pasif durumu değiştirildi.");
        return RedirectToAction(nameof(Index));
    }

    private int? GetCurrentUserId()
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(value, out var id) ? id : null;
    }
}
