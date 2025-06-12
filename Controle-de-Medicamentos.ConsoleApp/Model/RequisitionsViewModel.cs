using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.RequisitionModule;

namespace Controle_de_Medicamentos.ConsoleApp.Model;

public class CreateEntryRequisitionsViewModel
{
    public Guid MedicationId { get; set; }
    public Guid EmployeeID { get; set; }
    public int Quantity { get; set; }
    public List<SelectEmployeeVieModel> Employees { get; set; }

    public CreateEntryRequisitionsViewModel() { }

    public CreateEntryRequisitionsViewModel(Guid medicationId, List<Employee> employees)
    {
        MedicationId = medicationId;
        Employees = new List<SelectEmployeeVieModel>();

        foreach (var f in employees)
        {
            var selectVM = new SelectEmployeeVieModel(f.Id, f.Name);

            Employees.Add(selectVM);
        }
    }
}

public class CreateExitRequisitionsViewModel
{
    public Guid EmployeeID { get; set; }
    public Guid PatientID { get; set; }
    public List<SelectEmployeeVieModel> SelectEmployees { get; set; }
    public List<SelectPatientViewModel> SelectPatients { get; set; }

    public CreateExitRequisitionsViewModel() { }

    public CreateExitRequisitionsViewModel(List<Employee> employees, List<Patient> patients) 
    {
        SelectEmployees = new List<SelectEmployeeVieModel>();
        foreach (var e in employees)
        {
            var selectVM = new SelectEmployeeVieModel(e.Id, e.Name);
            SelectEmployees.Add(selectVM);
        }

        SelectPatients = new List<SelectPatientViewModel>();

        foreach (var p in patients)
        {
            var selectVM = new SelectPatientViewModel(p.Id, p.Name);
            SelectPatients.Add(selectVM);
        }
    }
}

public class CreateCompleteExitRequisitionViewModel 
{
    public Guid EmployeeID { get; set; }
    public string EmployeeName { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; }
    public Guid PescriptionId { get; set; }
    public List<SelectPrescriptionViewModel> prescription { get; set; }

    public CreateCompleteExitRequisitionViewModel() { }

    public CreateCompleteExitRequisitionViewModel(Employee employee, Patient patient, List<MedicalPrescription> prescriptions): this() 
    {
        EmployeeID = employee.Id;
        EmployeeName = employee.Name;

        PatientId = patient.Id;
        PatientName = patient.Name;

        prescription = new List<SelectPrescriptionViewModel>();

        foreach (var p in prescriptions) 
        {
            var SelectVM = new SelectPrescriptionViewModel(p.Id, p.Patient.Name, p.Date, p.Medications);

            prescription.Add(SelectVM);
        }
    }
}

public class ViewRequisitionViewModel 
{
    public List<DetailsRequisitionViewModel> Registers { get; set; }

    public ViewRequisitionViewModel(List<ExitRequest> requests) 
    {
        Registers = new List<DetailsRequisitionViewModel>();

        foreach (var r in requests) 
        {
            var detailsVM = r.DetailVM();
            Registers.Add(detailsVM);
        }
    }
}

public class DetailsRequisitionViewModel 
{
    public Guid Id { get; set; }
    public string Employee { get; set; }
    public string Patient { get; set; }
    public DateTime Date { get; set; }
    public List<string> Medications { get; set; }

    public DetailsRequisitionViewModel(Guid id, string employee, string patient, DateTime date, List<PrescriptionMedication> medications)
    {
        Id = id;
        Employee = employee;
        Patient = patient;
        Date = date;

        Medications = new List<string>();

        foreach (var m in medications) 
        {
            Medications.Add(m.Medication.Name);
        }
    }
}
public class SelectEmployeeVieModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public SelectEmployeeVieModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class SelectPatientViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public SelectPatientViewModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class SelectPrescriptionViewModel
{
    public Guid Id { get; set; }
    public string PatientName { get; set; }
    public DateTime Date { get; set; }
    public List<SelectPrescribedMedicationViewModel> PrescribedMedications { get; set; }

    public SelectPrescriptionViewModel() { }

    public SelectPrescriptionViewModel(Guid id, string patientName, DateTime dat, List<PrescriptionMedication> prescriptionMedications) : this()
    {
        Id = id;
        PatientName = patientName;
        Date = dat;

        PrescribedMedications = new List<SelectPrescribedMedicationViewModel>();

        foreach (var m in prescriptionMedications)
        {
            var SelectVM = new SelectPrescribedMedicationViewModel(m.Medication.Name);
            PrescribedMedications.Add(SelectVM);
        }
    }
    public override string ToString()
    {
        var medicationnames = string.Join(", ", PrescribedMedications);

        return string.Join(" - ", $"Emissão: {Date.ToShortDateString()}", $"[{medicationnames}]");
    }

}

public class SelectPrescribedMedicationViewModel
{
    public string Name { get; set; }

    public SelectPrescribedMedicationViewModel(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}