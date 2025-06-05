using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("/pacientes")]
public class PatientController : Controller
{
    private readonly DataContext _context;
    private readonly IPatientRepository _patientRepository;

    public PatientController()
    {
        _context = new DataContext(true);
        _patientRepository = new PatientRepository(_context);
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("cadastrar")]
    public IActionResult Create()
    {
        var createVM = new CreatePatientViewModel();

        return View(createVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Create(CreatePatientViewModel createVM)
    {
        var newPatient = createVM.ForEntity();

        _patientRepository.Add(newPatient);

        var notification = new NotificationViewModel("Registrado", "Paciente", newPatient.Name, "cadatrado", "/pacientes");

        return View("Notification", notification);
    }

    [HttpGet("editar{id:guid}")]
    public IActionResult Edit([FromRoute] Guid id)
    {
        var selectedRegister = _patientRepository.GetById(id);

        var editVM = new EditPatientViewModel

        (id, selectedRegister.Name, selectedRegister.PhoneNumber, selectedRegister.SUSCard);
        
        return View(editVM);
    }

    [HttpPost("editar{id:guid}")]
    public IActionResult Edit([FromRoute] Guid id, EditPatientViewModel editVM)
    {
        var updatedPatient = editVM.ForEntity();

        _patientRepository.Edit(id, updatedPatient);

        var notification = new NotificationViewModel("Atualizado", "Paciente", updatedPatient.Name, "editado", "visualizar");
       
        return View("Notification", notification);
    }

    [HttpGet("excluir{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var selectedRegister = _patientRepository.GetById(id);

        var deleteVM = new DeletePatientViewModel(id, selectedRegister.Name);

        return View("Delete", deleteVM);
    }


    [HttpPost("excluir{id:guid}")]
    public IActionResult Deleted([FromRoute] Guid id)
    {
        var selectedRegister = _patientRepository.GetById(id);

        var notification = new NotificationViewModel("Removido", "Paciente", selectedRegister.Name, "excluído", "visualizar");

        _patientRepository.Remove(id);

        return View("Notification", notification);
    }

    [HttpGet("visualizar")]
    public IActionResult View()
    {
        var patients = _patientRepository.GetAll();

        var viewVM = new ViewPatientViewModel(patients);

        return View(viewVM);
    }
}