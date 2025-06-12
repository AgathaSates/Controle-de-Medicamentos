using Controle_de_Medicamentos.ConsoleApp.RequisitionModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public class Medication : BaseEntity<Medication>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Supplier Supplier { get; set; }
    public List<EntryRequest> EntryRequests { get; set; }
    public List<ExitRequest> ExitRequest { get; set; }
    public int Quantity
    {
        get
        {
            int quantity = 0;
            foreach (var entry in EntryRequests)
            {
                quantity += entry.Quantity;
            }

            foreach (var exit in ExitRequest)
            {
                foreach (var med in exit.MedicalPrescription.Medications)
                {
                    quantity -= med.Quantity;
                }
            }
            return quantity;
        }
    }

    public Medication() { }

    public Medication(string name, string description, Supplier supplier)
    {
        Name = name;
        Description = description;
        Supplier = supplier;
        ExitRequest = new List<ExitRequest>();
        EntryRequests = new List<EntryRequest>();

    }

    public override void UpdateEntity(Medication entity)
    {
        Name = entity.Name;
        Description = entity.Description;
        Supplier = entity.Supplier;
    }


    /// <summary>
    /// Atualiza a quantidade em estoque do medicamento, somando o estoque atual com o valor informado.
    /// </summary>
    /// <param name="quantity">Valor da quantidade a somar.</param>
    /// <remarks>
    /// Este método sobrescreve a quantidade atual.
    /// </remarks>
    public void UpdateQuantity(EntryRequest entryRequest)
    {
        if (!EntryRequests.Contains(entryRequest))
        {
            EntryRequests.Add(entryRequest);
        }
    }

    /// <summary>
    /// Atualiza a quantidade em estoque do medicamento, subtraindo  o estoque atual pelo informado.
    /// </summary>
    /// <param name="quantity">Valor da quantidade a subtrair.</param>
    /// <remarks>
    /// Este método sobrescreve a quantidade atual.
    /// </remarks>
    public void SubstractQuantity(ExitRequest exitRequest)
    {
        if (!ExitRequest.Contains(exitRequest))
        {
            ExitRequest.Add(exitRequest);
        }
    }

    /// <summary>
    /// Verifica se o medicamento está com o estoque baixo, considerando o limite mínimo de 20 unidades.
    /// </summary>
    /// <returns>
    /// Retorna <c>true</c> se a quantidade em estoque for inferior a 20; caso contrário, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Esse método sera utilizado para exibir como "EM FALTA" caso atenda ao requisito.
    /// </remarks>
    public bool IsStockLow()
    {
        return Quantity < 20;
    }

    /// <summary>
    /// Compara dois medicamentos para verificar se representam o mesmo item, com base no nome.
    /// </summary>
    /// <param name="other">Outro medicamento a ser comparado.</param>
    /// <returns>
    /// Retorna <c>true</c> se os medicamentos tiverem o mesmo nome (ignorando maiúsculas, minúsculas e espaços); 
    /// caso contrário, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Esse método pode ser usado para evitar duplicidade de registros, consolidando a quantidade em estoque quando apropriado.
    /// </remarks>
    public bool IsSameMedication(Medication other)
    {
        return string.Equals(Name?.Trim(), other?.Name?.Trim(), StringComparison.OrdinalIgnoreCase) && Id != other.Id;
    }
}
