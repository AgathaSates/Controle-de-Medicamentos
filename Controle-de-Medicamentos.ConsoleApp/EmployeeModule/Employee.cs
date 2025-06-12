using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
public class Employee : BaseEntity<Employee>
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string CPF { get; set; }

    public Employee() { }

    public Employee(string name, string phoneNumber, string cpf)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        CPF = cpf;
    }

    public override void UpdateEntity(Employee entity)
    {
        Name = entity.Name;
        PhoneNumber = entity.PhoneNumber;
        CPF = entity.CPF;
    }

    /// <summary>
    /// Verifica se o CPF informado já está em uso por outro funcionário.
    /// </summary>
    /// <param name="employee">Funcionário a ser comparado.</param>
    /// <returns>
    /// <c>true</c> se o CPF for igual (ignorando maiúsculas, minúsculas e espaços) e os IDs forem diferentes; caso contrário, <c>false</c>.
    /// </returns>
    public bool IsSameCPF(Employee employee)
    {
        return string.Equals(CPF?.Trim(), employee?.CPF?.Trim(), StringComparison.OrdinalIgnoreCase) && Id != employee.Id;
    }
}
