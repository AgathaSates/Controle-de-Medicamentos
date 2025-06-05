using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.Model;

public abstract class FormSupplierViewModel
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string CNPJ { get; set; }
}

public class CreateSupplierViewModel : FormSupplierViewModel
{
    public CreateSupplierViewModel() { }
    public CreateSupplierViewModel(string name, string phone, string cnpj) : this()
    {
        Name = name;
        Phone = phone;
        CNPJ = cnpj;
    }
}

public class EditSupplierViewModel : FormSupplierViewModel
{
    public Guid Id { get; set; }
    public EditSupplierViewModel() { }
    public EditSupplierViewModel(Guid id, string name, string phone, string cnpj) : this()
    {
        Id = id;
        Name = name;
        Phone = phone;
        CNPJ = cnpj;
    }
}

public class DeleteSupplierViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public DeleteSupplierViewModel() { }
    public DeleteSupplierViewModel(Guid id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}

public class ViewSupplierViewModel
{
    public List<DetailSupplierViewModel> Registers { get; }

    public ViewSupplierViewModel(List<Supplier> suppliers)
    {
        Registers = [];

        foreach (var s in suppliers)
        {
            var detailVM = s.DetailVM();

            Registers.Add(detailVM);
        }
    }
}

public class DetailSupplierViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string CNPJ { get; set; }
    public DetailSupplierViewModel(Guid id, string name, string phone, string cnpj)
    {
        Id = id;
        Name = name;
        Phone = phone;
        CNPJ = cnpj;
    }
}
