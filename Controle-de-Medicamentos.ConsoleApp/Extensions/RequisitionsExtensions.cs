using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.RequisitionModule;

namespace Controle_de_Medicamentos.ConsoleApp.Extensions;

public static class RequisitionsExtensions
{
    public static EntryRequest ForEntity(this CreateEntryRequisitionsViewModel formVM, List<Employee> employees, List<Medication> medications) 
    {
        Employee employee = null;

        foreach (var e in employees) 
        {
            if (e.Id == formVM.EmployeeID)
                employee = e;
        }

        Medication medication = null;

        foreach (var m in medications) 
        {
            if (m.Id == formVM.MedicationId)
                medication = m;
        }
        return new EntryRequest(employee, medication, formVM.Quantity);
    }

    public static ExitRequest ForEntity(this CreateCompleteExitRequisitionViewModel formVM, List<Employee> employees, List<MedicalPrescription> prescriptions) 
    {
        Employee employee = null;
        foreach (var e in employees)
        {
            if (e.Id == formVM.EmployeeID)
                employee = e;
        }
        MedicalPrescription medicalPrescription = null;
        foreach (var m in prescriptions) 
        {
            if (m.Id == formVM.PescriptionId)
                medicalPrescription = m;
        }
        return new ExitRequest(employee, medicalPrescription);
    }

    public static DetailsRequisitionViewModel DetailVM(this ExitRequest request) 
    {
        return new DetailsRequisitionViewModel(request.Id, request.Employee.Name, request.MedicalPrescription.Patient.Name, request.Date, request.MedicalPrescription.Medications);
    }
}
