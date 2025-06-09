using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

public class PrescriptionMedication
{

    public Guid Id { get; set; }
    public Medication Medication { get; set; }
    public string Dosage { get; set; }
    public int Quantity { get; set; }
    public string Period { get; set; }

    [ExcludeFromCodeCoverage]
    public PrescriptionMedication() { }

    public PrescriptionMedication(Medication medication, string dosage, int quantity, string period)
    {
        Id = Guid.NewGuid();
        Medication = medication;
        Dosage = dosage;
        Quantity = quantity;
        Period = period;
    }
}
