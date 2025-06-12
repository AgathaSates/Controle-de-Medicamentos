
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.RequisitionModule;

public class RequisitionRepository : BaseRepository<ExitRequest>, IRequisitionRepository
{
    private List<EntryRequest> entryRequisitions = new List<EntryRequest>();
    private List<ExitRequest> exitRequisitions = new List<ExitRequest>();

    public RequisitionRepository(DataContext context) : base(context)
    {
        entryRequisitions = context.entryRequisitions;
        exitRequisitions = context.exitRequisitions;
    }


    public void AddEntryRequisition(EntryRequest requisition)
    {
        entryRequisitions.Add(requisition);
        Context.SaveData();
    }

    public void AddExitRequisition(ExitRequest requisition)
    {
       exitRequisitions.Add(requisition);
        Context.SaveData();
    }

    public List<ExitRequest> GetAllExitRequisitions()
    {
        return exitRequisitions;
    }

    public List<EntryRequest> GetAllEntryRequisitions()
    {
        return entryRequisitions;
    }

    public override List<ExitRequest> GetList()
    {
        return Context.exitRequisitions;
    }
}
