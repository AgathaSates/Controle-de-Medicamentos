
using Controle_de_Medicamentos.ConsoleApp.Shared;

namespace Controle_de_Medicamentos.ConsoleApp.RequisitionModule;

public class RequisitionRepository : IRequisitionRepository
{
    private DataContext context;
    private List<EntryRequest> entryRequisitions;
    private List<ExitRequest> exitRequisitions;

    public RequisitionRepository(DataContext context)
    {
        this.context = context;
        entryRequisitions = new List<EntryRequest>();
        exitRequisitions = new List<ExitRequest>();
    }

    public void AddEntryRequisition(EntryRequest requisition)
    {
        entryRequisitions.Add(requisition);
        context.SaveData();
    }

    public void AddExitRequisition(ExitRequest requisition)
    {
       exitRequisitions.Add(requisition);
        context.SaveData();
    }

    public List<ExitRequest> GetAllExitRequisitions()
    {
        return exitRequisitions;
    }

    public List<EntryRequest> GetAllEntryRequisitions()
    {
        return entryRequisitions;
    }
}
