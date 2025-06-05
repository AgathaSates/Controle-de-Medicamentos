using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.Model;

namespace Controle_de_Medicamentos.ConsoleApp.Extensions;

public static class EmployeeExtensions
{
    public static Employee ForEntity(this FormEmployeeViewModel FormVM)
    {
        return new Employee(FormVM.Name, FormVM.Phone, FormVM.CPF);
    }

    public static DetailEmployeeViewModel DetailVM(this Employee employee)
    {
        return new DetailEmployeeViewModel(employee.Id, employee.Name, employee.PhoneNumber, employee.CPF);
    }
}