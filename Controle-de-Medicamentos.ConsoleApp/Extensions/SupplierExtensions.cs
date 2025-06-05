using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.Extensions;

public static class SupplierExtensions
{
    public static Supplier ForEntity(this FormSupplierViewModel FormVM)
    {
        return new Supplier(FormVM.Name, FormVM.Phone, FormVM.CNPJ);
    }

    public static DetailSupplierViewModel DetailVM(this Supplier supplier)
    {
        return new DetailSupplierViewModel(supplier.Id, supplier.Name, supplier.PhoneNumber, supplier.CNPJ);
    }
}