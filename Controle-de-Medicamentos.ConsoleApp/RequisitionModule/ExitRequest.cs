using System.Diagnostics.CodeAnalysis;
using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

namespace Controle_de_Medicamentos.ConsoleApp.RequisitionModule;

public class ExitRequest
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Employee Employee { get; set; }
    public MedicalPrescription MedicalPrescription { get; set; }

    [ExcludeFromCodeCoverage]
    public ExitRequest() { }

    public ExitRequest(Employee employee, MedicalPrescription medicalPrescription)
    {
        Id = Guid.NewGuid();
        Date = DateTime.Now;
        Employee = employee;
        MedicalPrescription = medicalPrescription;
    }
}

  
