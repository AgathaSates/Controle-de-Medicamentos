using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;

namespace Controle_de_Medicamentos.ConsoleApp.RequisitionModule;

public class EntryRequest
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Employee employee { get; set; }
    public Medication Medication { get; set; }
    public int Quantity { get; set; }

    public EntryRequest() { }

    public EntryRequest(Employee employee, Medication medication, int quantity)
    {
        Id = Guid.NewGuid();
        Date = DateTime.Now;
        this.employee = employee;
        Medication = medication;
        Quantity = quantity;
    }
}
