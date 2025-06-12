using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

public class MedicalPrescription : BaseEntity<MedicalPrescription>
{
    public Guid Id { get; set; }
    public string DoctorCRM { get; set; }
    public DateTime Date { get; set; }
    public Patient Patient { get; set; }
    public List<PrescriptionMedication> Medications { get; set; }

    [ExcludeFromCodeCoverage]
    public MedicalPrescription() { }

    public MedicalPrescription(string doctorCRM, Patient patient, List<PrescriptionMedication> medications)
    {
        Id = Guid.NewGuid();
        DoctorCRM = doctorCRM;
        Date = DateTime.Now;
        Patient = patient;
        Medications = medications;
    }

    public override void UpdateEntity(MedicalPrescription entity)
    {
        throw new NotImplementedException();
    }

}

