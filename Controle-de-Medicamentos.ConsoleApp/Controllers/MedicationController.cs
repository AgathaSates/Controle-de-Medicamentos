using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("/medicamentos")]
public class MedicationController : Controller
{
    private readonly DataContext _context;
    private readonly IMedicationRepository _medicationRepository;
    private readonly ISupplierRepository _supplierRepository;

    public MedicationController()
    {
        _context = new DataContext(true);
        _medicationRepository = new MedicationRepository(_context);
        _supplierRepository = new SupplierRepository(_context);
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("cadastrar")]
    public IActionResult Create()
    {
        var suppliers = _supplierRepository.GetAll();

        var createVM = new CreateMedicationViewModel(suppliers);

        return View(createVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Create(CreateMedicationViewModel createVM)
    {
        var supplier = _supplierRepository.GetAll();

        var register = createVM.ForEntity(supplier);

        _medicationRepository.Add(register);

        var notification = new NotificationViewModel("Registrado", "Medicamento", register.Name, "cadastrado", "/medicamentos");
        return View("Notification", notification);
    }

    [HttpGet("editar{id:guid}")]
    public IActionResult Edit([FromRoute] Guid id)
    {
        var selectedRegister = _medicationRepository.GetById(id);

        var suppliers = _supplierRepository.GetAll();

        var editVM = new EditMedicationViewModel
        (
            id,
            selectedRegister.Name,
            selectedRegister.Description,
            selectedRegister.Supplier.Id,
            suppliers
        );

        return View(editVM);
    }

    [HttpPost("editar/{id:guid}")]
    public IActionResult Edit([FromRoute] Guid id, EditMedicationViewModel editVM)
    {
        var supplier = _supplierRepository.GetAll();

        var register = editVM.ForEntity(supplier);

        _medicationRepository.Edit(id, register);

        var notification = new NotificationViewModel("Atualizado", "Medicamento", register.Name, "editado", "/medicamentos");
        
        return View("Notification", notification);
    }

    [HttpGet("excluir{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var selectedRegister = _medicationRepository.GetById(id);

        var deleteVM = new DeleteMedicationViewModel
        (selectedRegister.Id,selectedRegister.Name);

        return View(deleteVM);
    }

    [HttpPost("excluir{id:guid}")]
    public IActionResult Deleted([FromRoute] Guid id)
    {
        var register = _medicationRepository.GetById(id);

        var notification = new NotificationViewModel("Removido", "Medicamento", register.Name, "excluído", "/medicamentos");

        _medicationRepository.Remove(id);
        
        return View("Notification", notification);
    }

    [HttpGet("visualizar")]
    public IActionResult View()
    {
        var medications = _medicationRepository.GetAll();

        var viewVM = new ViewMedicationViewModel(medications);

        return View(viewVM);
    }

    [HttpGet("detalhes{id:guid}")]
    public IActionResult Details([FromRoute] Guid id) 
    {
        var register = _medicationRepository.GetById(id);

        var viewVM = new DetailsMedicationViewModel(register);

        return View("Details", viewVM);
    }
}