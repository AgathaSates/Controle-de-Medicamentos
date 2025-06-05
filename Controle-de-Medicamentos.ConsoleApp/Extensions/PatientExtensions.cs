using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;

namespace Controle_de_Medicamentos.ConsoleApp.Extensions;

public static class PatientExtensions
{
    public static Patient ForEntity(this FormPatientViewModel FormVM)
    {
        return new Patient(FormVM.Name, FormVM.Phone, FormVM.SUSCard);
    }

    public static DetailPatientViewModel DetailVM(this Patient patient)
    {
        return new DetailPatientViewModel(patient.Id, patient.Name, patient.PhoneNumber, patient.SUSCard);
    }
}
