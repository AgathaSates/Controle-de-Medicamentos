using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.SupplierModule;

public class Supplier : BaseEntity<Supplier>
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string CNPJ { get; set; }

    public Supplier() {}

    public Supplier(string name, string phone, string cnpj)
    {
        Name = name;
        PhoneNumber = phone;
        CNPJ = cnpj;
    }

    public override void UpdateEntity(Supplier entity)
    {
        Name = entity.Name;
        PhoneNumber = entity.PhoneNumber;
        CNPJ = entity.CNPJ;
    }

    /// <summary>
    /// Compara o CNPJ do fornecedor atual com o de outro fornecedor, ignorando maiúsculas, minúsculas e espaços.
    /// </summary>
    /// <param name="supplier">Fornecedor a ser comparado.</param>
    /// <returns>Retorna true se os CNPJs forem iguais; caso contrário, false.</returns>
    public bool IsSameCNPJ(Supplier supplier)
    {
        return string.Equals(CNPJ?.Trim(), supplier?.CNPJ?.Trim(), StringComparison.OrdinalIgnoreCase) && Id != supplier.Id;
    }
}
