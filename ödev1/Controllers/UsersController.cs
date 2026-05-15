using System.Security.Claims;
using KeyloggerTespitSistemi.Services;
using KeyloggerTespitSistemi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KeyloggerTespitSistemi.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IAuditLogService _auditLogService;

    public UsersController(IUserService userService, IAuditLogService auditLogService)
    {
        _userService = userService;
        _auditLogService = auditLogService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _userService.GetUsersAsync());
    }

    public async Task<IActionResult> Create()
    {
        await FillRolesAsync();
        return View(new UserFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await FillRolesAsync();
            return View(model);
        }

        try
        {
            await _userService.CreateAsync(model);
            await _auditLogService.LogAsync(GetCurrentUserId(), "CreateUser", "User", null, "Yeni kullanıcı oluşturuldu.");
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException exception)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
            await FillRolesAsync();
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        await _userService.ToggleAsync(id);
        await _auditLogService.LogAsync(GetCurrentUserId(), "ToggleUser", "User", id, "Kullanıcı aktif/pasif durumu değiştirildi.");
        return RedirectToAction(nameof(Index));
    }

    private async Task FillRolesAsync()
    {
        ViewBag.Roles = new SelectList(await _userService.GetRolesAsync(), "Id", "Name");
    }

    private int? GetCurrentUserId()
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(value, out var id) ? id : null;
    }
}
