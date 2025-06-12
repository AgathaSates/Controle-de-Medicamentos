using System.Diagnostics.CodeAnalysis;
using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.RequisitionModule;

public class ExitRequest : BaseEntity<ExitRequest>
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Employee Employee { get; set; }
    public MedicalPrescription MedicalPrescription { get; set; }

    public ExitRequest()
    {
    }

    public ExitRequest(Employee employee, MedicalPrescription medicalPrescription)
    {
        Id = Guid.NewGuid();
        Date = DateTime.Now;
        Employee = employee;
        MedicalPrescription = medicalPrescription;
    }

    public override void UpdateEntity(ExitRequest updatedEntity)
    {
        Date = updatedEntity.Date;
        Employee = updatedEntity.Employee;
        MedicalPrescription = updatedEntity.MedicalPrescription;
    }

}

  
