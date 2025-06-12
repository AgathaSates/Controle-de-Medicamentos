using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public class MedicationRepository : BaseRepository<Medication>, IMedicationRepository
{
    public MedicationRepository(DataContext context) : base(context) {}

    public override List<Medication> GetList()
    {
       return Context.Medications;
    }

    public override void Add(Medication entity)
    {
        entity.Id = Guid.NewGuid();
        List.Add(entity);
        Context.SaveData();
    }

    public bool HasMedicationForSupplier(Supplier supplier)
    {
        return List.Any(m => m.Supplier.Id == supplier.Id);
    }
}
