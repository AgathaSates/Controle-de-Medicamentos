using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

public class MedicalPrescriptionRepository : BaseRepository<MedicalPrescription>, IMedicalPrescriptionRepository
{
    private List<MedicalPrescription> medicalPrescriptions = new List<MedicalPrescription>();
    
    public MedicalPrescriptionRepository(DataContext context) : base(context)
    {
        medicalPrescriptions = context.MedicalPrescriptions;
    }

    public void Add(MedicalPrescription medicalPrescription)
    {
        medicalPrescriptions.Add(medicalPrescription);
        Context.SaveData();
    }

    public List<MedicalPrescription> GetAll()
    {
        return medicalPrescriptions;
    }

    public override List<MedicalPrescription> GetList()
    {
        return Context.MedicalPrescriptions;
    }
}
