using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.Extensions;

public static class MedicationExtensions
{
    public static Medication ForEntity(this FormMedicationViewModel formVM, List<Supplier> suppliers)
    {
        Supplier selectSupplier = null;

        foreach (var s in suppliers)
        {
            if (s.Id == formVM.SupplierID)
                selectSupplier = s;
        }

        return new Medication(formVM.Name, formVM.Description, selectSupplier);
    }

    public static DetailsMedicationViewModel DetailVM(this Medication medication)
    {
        return new DetailsMedicationViewModel(medication.Id, medication.Name, medication.Description, medication.Supplier.Name, medication.Quantity, medication.IsStockLow());
    }
}
