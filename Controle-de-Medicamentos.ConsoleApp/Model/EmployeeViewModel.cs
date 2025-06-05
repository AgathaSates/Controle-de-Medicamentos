using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.Extensions;

namespace Controle_de_Medicamentos.ConsoleApp.Model;

public abstract class FormEmployeeViewModel
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string CPF { get; set; }
}

public class CreateEmployeeViewModel : FormEmployeeViewModel
{
    public CreateEmployeeViewModel() { }
    public CreateEmployeeViewModel(string name, string phone, string cpf) : this()
    {
        Name = name;
        Phone = phone;
        CPF = cpf;
    }
}

public class EditEmployeeViewModel : FormEmployeeViewModel
{
    public Guid Id { get; set; }
    public EditEmployeeViewModel() { }
    public EditEmployeeViewModel(Guid id, string name, string phone, string cpf) : this()
    {
        Id = id;
        Name = name;
        Phone = phone;
        CPF = cpf;
    }
}

public class DeleteEmployeeViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public DeleteEmployeeViewModel() { }
    public DeleteEmployeeViewModel(Guid id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}

public class ViewEmployeeViewModel
{
    public List<DetailEmployeeViewModel> Registers { get; }

    public ViewEmployeeViewModel(List<Employee> employees)
    {
        Registers = [];

        foreach (var e in employees)
        {
            var detailVM = e.DetailVM();

            Registers.Add(detailVM);
        }
    }
}

public class DetailEmployeeViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string CPF { get; set; }
    public DetailEmployeeViewModel(Guid id, string name, string phone, string cpf)
    {
        Id = id;
        Name = name;
        Phone = phone;
        CPF = cpf;
    }
}
