namespace Controle_de_Medicamentos.ConsoleApp.RequisitionModule;

public interface IRequisitionRepository
{
    public void AddEntryRequisition(EntryRequest requisition);
    public void AddExitRequisition(ExitRequest requisition);
    public List<ExitRequest> GetAllExitRequisitions();
}
