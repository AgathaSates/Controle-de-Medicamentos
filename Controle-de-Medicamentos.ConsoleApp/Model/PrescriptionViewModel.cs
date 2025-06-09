using System.Globalization;
using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;

namespace Controle_de_Medicamentos.ConsoleApp.Model;

public class CreatePrescriptionViewModel
{
    public Guid PatientId { get; set; }
    public List<SelectPatientViewModel> Patients { get; set; }
    public string CRMMedic { get; set; }
    public List<SelectMedicationViewModel> selectMedications { get; set; }
    public List<DetailsPrescriptionMedicationViewModel> medications { get; set; }

    public Guid MedicationID { get; set; }
    public string Dosage { get; set; }
    public string Period { get; set; }
    public int Quantity { get; set; }

    public CreatePrescriptionViewModel()
    {
        Patients = new List<SelectPatientViewModel>();
        selectMedications = new List<SelectMedicationViewModel>();
        medications = new List<DetailsPrescriptionMedicationViewModel>();
    }

    public CreatePrescriptionViewModel(List<Patient> patients, List<Medication> medications) : this()
    {
        AddPatients(patients);
        AddMedication(medications);
    }

    public void AddPatients(List<Patient> patients)
    {
        foreach (var p in patients)
        {
            var selectPatientVM = new SelectPatientViewModel(p.Id, p.Name);

            Patients.Add(selectPatientVM);
        }
    }
    public void AddMedication(List<Medication> medications)
    {
        foreach (var m in medications)
        {
            var selectMedicationVM = new SelectMedicationViewModel(m.Id, m.Name);
            selectMedications.Add(selectMedicationVM);
        }
    }
}


public class SelectMedicationViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public SelectMedicationViewModel() { }

    public SelectMedicationViewModel(Guid id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}

public class DetailsPrescriptionMedicationViewModel
{
    public Guid MedicationID { get; set; }
    public string Medicatio { get; set; }
    public string Dosage { get; set; }
    public string Period { get; set; }
    public int Quantity { get; set; }

    public DetailsPrescriptionMedicationViewModel() { }

    public DetailsPrescriptionMedicationViewModel(Guid medicationId, string namemedication, string dosage, string period, int quantity) : this()
    {
        MedicationID = medicationId;
        Medicatio = namemedication;
        Dosage = dosage;
        Period = period;
        Quantity = quantity;
    }
}

public class ViewPrescriptionsViewModel
{
    public List<DetailsPrescriptionViewModel> registers { get;}

    public ViewPrescriptionsViewModel(List<MedicalPrescription> prescriptions)
    {
        registers = [];
        foreach (var p in prescriptions) 
        {
            var detailVM = p.DetailVM();
            registers.Add(detailVM);
        }
    }
}

public class DetailsPrescriptionViewModel
{
    public Guid id { get; set; }
    public string CRMMEdic { get; set; }
    public string Patient { get; set; }
    public DateTime Date { get; set; }
    public List<SelectPrescribedMedicationViewModel> Medications { get; set; }

    public DetailsPrescriptionViewModel(Guid id, string crmmedic, string patient, DateTime date, List<PrescriptionMedication> medications) 
    {
        this.id = id;
        CRMMEdic = crmmedic;
        Patient = patient;
        Date = date;

        Medications = new List<SelectPrescribedMedicationViewModel>();

        foreach (var med in medications) 
        {
            var selectVM = new SelectPrescribedMedicationViewModel(med.Medication.Name);

            Medications.Add(selectVM);
        }
    }

}