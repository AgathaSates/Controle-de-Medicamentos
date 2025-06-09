using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

public interface IMedicalPrescriptionRepository 
{
    public void Add(MedicalPrescription medicalPrescription);
    public List<MedicalPrescription> GetAll();
}
