using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.RequisitionModule;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("/requisicao")]
public class RequisitionsController : Controller
{
    private DataContext DataContext;
    private IRequisitionRepository RequisitionRepository;
    private IEmployeeRepository EmployeeRepository;
    private IMedicationRepository MedicationRepository;
    private IPatientRepository PatientRepository;
    private IMedicalPrescriptionRepository MedicalPrescriptionRepository;

    public RequisitionsController()
    {
        DataContext = new DataContext(true);
        RequisitionRepository = new RequisitionRepository(DataContext);
        EmployeeRepository = new EmployeeRepository(DataContext);
        MedicationRepository = new MedicationRepository(DataContext);
        PatientRepository = new PatientRepository(DataContext);
        MedicalPrescriptionRepository = new MedicalPrescriptionRepository(DataContext);
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("entrada/{medicationID:guid}")]
    public IActionResult Entry(Guid medicationID)
    {
        var employee = EmployeeRepository.GetAll();
        var medication = MedicationRepository.GetById(medicationID);

        var createVM = new CreateEntryRequisitionsViewModel(medicationID, employee);
        ViewBag.MedicationName = medication.Name;
        return View("CreateEntryRequest", createVM);
    }

    [HttpPost("entrada/{medicationID:guid}")]
    public IActionResult Entry(Guid medicationID, CreateEntryRequisitionsViewModel requisitionVM)
    {
        var employees = EmployeeRepository.GetAll();

        var medications = MedicationRepository.GetAll();

        var register = requisitionVM.ForEntity(employees, medications);

        var medication = MedicationRepository.GetById(medicationID);

        medication.UpdateQuantity(register);

        RequisitionRepository.AddEntryRequisition(register);

        var notification = new NotificationViewModel("Registrado", "Requisição", "de entrada", "cadastrado", "/medicamentos/visualizar");

        return View("Notification", notification);
    }

    [HttpGet("saida/cadastrar")]
    public IActionResult ExitCreate()
    {
        var employees = EmployeeRepository.GetAll();

        var patients = PatientRepository.GetAll();

        var createVM = new CreateExitRequisitionsViewModel(employees, patients);

        return View("CreateExitRequest", createVM);
    }

    [HttpPost("saida/cadastrar")]
    public IActionResult ExitCreate(CreateExitRequisitionsViewModel requisitionVM)
    {
        var employees = EmployeeRepository.GetAll();
        var patients = PatientRepository.GetAll();
        var medicalPrescriptions = MedicalPrescriptionRepository.GetAll();

        Employee employee = null;

        foreach (var e in employees)
        {
            if (e.Id == requisitionVM.EmployeeID)
            {
                employee = e;
                break;
            }
        }

        Patient patient = null;

        foreach (var p in patients)
        {
            if (p.Id == requisitionVM.PatientID)
            {
                patient = p;
                break;
            }
        }

        var prescriptions = new List<MedicalPrescription>();

        foreach (var p in medicalPrescriptions)
        {
            if (p.Patient.Id == patient.Id)
            {
                prescriptions.Add(p);
            }
        }

        var createCompleteVM = new CreateCompleteExitRequisitionViewModel(employee, patient, prescriptions);

        return View("CreateCompleteExitRequisition", createCompleteVM);
    }

    [HttpPost("saida/completar-cadastro")]
    public IActionResult CreateCompleteExitRequisition(CreateCompleteExitRequisitionViewModel createCompleteVM)
    {
        var employees = EmployeeRepository.GetAll();
        var medicalPrescription = MedicalPrescriptionRepository.GetAll();

        var entity = createCompleteVM.ForEntity(employees, medicalPrescription);

        foreach (var medication in entity.MedicalPrescription.Medications)
        {
            var med = medication.Medication;

            med.SubstractQuantity(entity);
        }

        RequisitionRepository.AddExitRequisition(entity);

        var notification = new NotificationViewModel("Registrado", "Requisição", "de saída", "cadastrado", "/requisicao/visualizar");

        return View("Notification", notification);
    }

    [HttpGet("visualizar")]
    public IActionResult View()
    {
        var requisitions = RequisitionRepository.GetAllExitRequisitions();

        var viewModel = new ViewRequisitionViewModel(requisitions);

        return View(viewModel);
    }
}