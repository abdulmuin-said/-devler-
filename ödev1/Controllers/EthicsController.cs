using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyloggerTespitSistemi.Controllers;

public class EthicsController : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }
}
