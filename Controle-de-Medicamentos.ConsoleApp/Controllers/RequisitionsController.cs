using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;
public class RequisitionsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
