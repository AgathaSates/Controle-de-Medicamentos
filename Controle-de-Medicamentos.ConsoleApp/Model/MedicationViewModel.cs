using System.Security;
using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.Model;

public abstract class FormMedicationViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid SupplierID { get; set; }
    public List<SelectSupplierViewModel> Suppliers { get; set; }

    protected FormMedicationViewModel()
    {
        Suppliers = new List<SelectSupplierViewModel>();
    }
}

public class SelectSupplierViewModel
{
    public Guid ID { get; set; }
    public string Name { get; set; }

    public SelectSupplierViewModel(Guid id, string name)
    {
        ID = id;
        Name = name;
    }
}

public class CreateMedicationViewModel : FormMedicationViewModel
{
    public CreateMedicationViewModel() {}

    public CreateMedicationViewModel(List<Supplier> suppliers) 
    {
        foreach (var s in suppliers)
        {
            var selectVM = new SelectSupplierViewModel(s.Id, s.Name);
            Suppliers.Add(selectVM);
        }
    }
}

public class EditMedicationViewModel : FormMedicationViewModel
{
    public Guid ID { get; set; }
    public EditMedicationViewModel() {}
    public EditMedicationViewModel(Guid id, string name, string description, Guid supplierId, List<Supplier> suppliers)
    {
        ID = id;
        Name = name;
        Description = description;
        SupplierID = supplierId;

        foreach (var s in suppliers)
        {
            var selectVM = new SelectSupplierViewModel(s.Id, s.Name);
           
            Suppliers.Add(selectVM);
        }
    }
}

public class DeleteMedicationViewModel 
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public DeleteMedicationViewModel() {}
    public DeleteMedicationViewModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class ViewMedicationViewModel
{
    public List<DetailsMedicationViewModel> Registers { get; set; }

    public ViewMedicationViewModel(List<Medication> medications) 
    {
        Registers = new List<DetailsMedicationViewModel>();
        foreach (var m in medications)
        {
            var DetailsVM = m.DetailVM();

            Registers.Add(DetailsVM);
        }
    } 
}

public class DetailsMedicationViewModel 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SupplierName { get; set; }
    public int QuantityInStock { get; set; }
    public bool OutOfStock { get; set; }
    
    public DetailsMedicationViewModel(Medication medication) 
    {
        Id= medication.Id;
        Name= medication.Name;
        Description= medication.Description;
        SupplierName = medication.Supplier.Name;
        QuantityInStock = medication.Quantity;
        OutOfStock = medication.IsStockLow();
    }

    public DetailsMedicationViewModel(Guid id, string name, string description, string supplierName, int quantityInStock, bool outOfStock) 
    {
        Id = id;
        Name = name;
        Description = description;
        SupplierName = supplierName;
        QuantityInStock = quantityInStock;
        OutOfStock = outOfStock;
    }
}