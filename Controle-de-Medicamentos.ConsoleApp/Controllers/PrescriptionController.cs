using System.Text.Json;
using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("/prescricao")]
public class PrescriptionController : Controller
{
    private DataContext _dataContext;
    private IMedicalPrescriptionRepository medicalPrescriptionRepository;
    private IPatientRepository patientRepository;
    private IMedicationRepository medicationRepository;

    public PrescriptionController()
    {
        _dataContext = new DataContext(true);
        medicalPrescriptionRepository = new MedicalPrescriptionRepository(_dataContext);
        patientRepository = new PatientRepository(_dataContext);
        medicationRepository = new MedicationRepository(_dataContext);
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("cadastrar")]
    public IActionResult Create()
    {
        var patients = patientRepository.GetAll();
        var medications = medicationRepository.GetAll();

        CreatePrescriptionViewModel createVM;

        var saveprescription = TempData.Peek("prescricao");

        if (saveprescription is not null && saveprescription is string jsonString)
        {
            createVM = JsonSerializer.Deserialize<CreatePrescriptionViewModel>(jsonString)!;

            createVM.AddPatients(patients);
            createVM.AddMedication(medications);

            return View(createVM);
        }

        createVM = new CreatePrescriptionViewModel(patients, medications);

        return View(createVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Create(CreatePrescriptionViewModel createVM, string submit)
    {
        var patients = patientRepository.GetAll();
        var medications = medicationRepository.GetAll();

        if (TempData.TryGetValue("prescricao", out var value) && value is string jsonString)
        {
            var lastVm = JsonSerializer.Deserialize<CreatePrescriptionViewModel>(jsonString)!;

            lastVm.PatientId = createVM.PatientId;
            lastVm.CRMMedic = createVM.CRMMedic;
            lastVm.MedicationID = createVM.MedicationID;
            lastVm.Dosage = createVM.Dosage;
            lastVm.Period = createVM.Period;
            lastVm.Quantity = createVM.Quantity;

            createVM = lastVm;
        }

        if (submit == "adicionarMedicamento")
        {
            var medication = medicationRepository.GetById(createVM.MedicationID);

            var detailsMedication = new DetailsPrescriptionMedicationViewModel
            (
              createVM.MedicationID,
                medication.Name,
                createVM.Dosage,
                createVM.Period, createVM.Quantity
            );

            createVM.medications.Add(detailsMedication);

            TempData["prescricao"] = JsonSerializer.Serialize(createVM);

            return RedirectToAction("Create");
        }

        else if (submit == "limpar")
        {
            TempData.Remove("prescricao");

            return RedirectToAction("Create");
        }

        else 
        {
            var newregister = createVM.ForEntity(patients, medications);

            medicalPrescriptionRepository.Add(newregister);

            NotificationViewModel notification = new NotificationViewModel("Registrado", "Prescrição", "Médica", "cadastrado", "/prescricao");

            TempData.Remove("prescricao");

            return View("Notification", notification);
        }
    }

    [HttpGet("visualizar")]
    public IActionResult View()
    {
        var prescriptions = medicalPrescriptionRepository.GetAll();
        
        var viewVM = new ViewPrescriptionsViewModel(prescriptions);

        return View(viewVM);
    }
}
