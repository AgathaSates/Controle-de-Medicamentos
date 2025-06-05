using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("/funcionarios")]
public class EmployeeController : Controller
{
    private readonly DataContext _context;
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController()
    {
        _context = new DataContext(true);
        _employeeRepository = new EmployeeRepository(_context);
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("cadastrar")]
    public IActionResult Create()
    {
        var createVM = new CreateEmployeeViewModel();

        return View(createVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Create(CreateEmployeeViewModel createVM)
    {
        var newEmployee = createVM.ForEntity();

        _employeeRepository.Add(newEmployee);

        var notification = new NotificationViewModel("Registrado", "Funcionário", newEmployee.Name, "cadatrado", "/funcionarios");

        return View("Notification", notification);
    }

    [HttpGet("editar{id:guid}")]
    public IActionResult Edit([FromRoute] Guid id)
    {
        var selectedRegister = _employeeRepository.GetById(id);

        var editVM = new EditEmployeeViewModel
        (id, selectedRegister.Name, selectedRegister.PhoneNumber, selectedRegister.CPF);

        return View(editVM);
    }

    [HttpPost("editar{id:guid}")]
    public IActionResult Edit([FromRoute] Guid id, EditEmployeeViewModel editVM)
    {
        var updatedRegister = editVM.ForEntity();

        _employeeRepository.Edit(id, updatedRegister);

        var notification = new NotificationViewModel("Atualizado", "Funcionário", updatedRegister.Name, "editado", "visualizar");

        return View("Notification", notification);
    }

    [HttpGet("excluir{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var selectedRegister = _employeeRepository.GetById(id);

        var deleteVM = new DeleteEmployeeViewModel(id, selectedRegister.Name);

        return View("Delete", deleteVM);
    }

    [HttpPost("excluir{id:guid}")]
    public IActionResult Deleted([FromRoute] Guid id)
    {
        var selectedRegister = _employeeRepository.GetById(id);

        var notification = new NotificationViewModel("Removido", "Funcionário", selectedRegister.Name, "excluído", "visualizar");

        _employeeRepository.Remove(id);

        return View("Notification", notification);
    }

    [HttpGet("visualizar")]
    public IActionResult View()
    {
        var employees = _employeeRepository.GetAll();

        var viewVM = new ViewEmployeeViewModel(employees);

        return View(viewVM);
    }
}