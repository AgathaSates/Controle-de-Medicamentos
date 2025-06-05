using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("/fornecedores")]
public class SupplierController : Controller
{
    private readonly DataContext _context;
    private readonly ISupplierRepository _supplierRepository;

    public SupplierController()
    {
        _context = new DataContext(true);
        _supplierRepository = new SupplierRepository(_context);
    }


    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("cadastrar")]
    public IActionResult Create()
    {
        var createVM = new CreateSupplierViewModel();

        return View(createVM);
    }


    [HttpPost("cadastrar")]
    public IActionResult Create(CreateSupplierViewModel createVM)
    {
        var newSupplier = createVM.ForEntity();

        _supplierRepository.Add(newSupplier);

        var notification = new NotificationViewModel("Registrado", "Fornecedor", newSupplier.Name, "cadatrado", "/fornecedores");

        return View("Notification", notification);
    }

    [HttpGet("editar{id:guid}")]
    public IActionResult Edit([FromRoute] Guid id)
    {
        var selectedRegister = _supplierRepository.GetById(id);

        var editVM = new EditSupplierViewModel
        (id, selectedRegister.Name, selectedRegister.PhoneNumber, selectedRegister.CNPJ);
        
        return View(editVM);
    }

    [HttpPost("editar{id:guid}")]
    public IActionResult Edit([FromRoute] Guid id, EditSupplierViewModel editVM)
    {
        var updatedRegister = editVM.ForEntity();

        _supplierRepository.Edit(id, updatedRegister);

        var notification = new NotificationViewModel("Atualizado", "Fornecedor", updatedRegister.Name, "editado", "visualizar");

        return View("Notification", notification); 
    }

    [HttpGet("excluir{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var selectedRegister = _supplierRepository.GetById(id);

        var deleteVM = new DeleteSupplierViewModel(id, selectedRegister.Name);

        return View("Delete", deleteVM);
    }

    [HttpPost("excluir{id:guid}")]
    public IActionResult Deleted([FromRoute] Guid id)
    {
        var selectedRegister = _supplierRepository.GetById(id);

        var notification = new NotificationViewModel("Removido", "Fornecedor", selectedRegister.Name, "excluído", "visualizar");

        _supplierRepository.Remove(id);

        return View("Notification", notification);
    }

    [HttpGet("visualizar")]
    public IActionResult View()
    {
        var suppliers = _supplierRepository.GetAll();

        var viewVM = new ViewSupplierViewModel(suppliers);

        return View(viewVM);
    }
}