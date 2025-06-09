using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Model;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;

namespace Controle_de_Medicamentos.ConsoleApp.Extensions;

public static class PrescriptionExtensions
{
    public static MedicalPrescription ForEntity(this CreatePrescriptionViewModel creteVM, List<Patient> patients, List<Medication> medictions) 
    {
        Patient patient = null;

        foreach (var p in patients) 
        {
            if (p.Id == creteVM.PatientId)
                patient = p;
        }

        var registers = new List<PrescriptionMedication>();

        foreach (var selectMVM in creteVM.medications)
        {
            foreach (var m in medictions)
            {
                if (selectMVM.MedicationID == m.Id) 
                {
                    var register = new PrescriptionMedication(m, selectMVM.Dosage, selectMVM.Quantity, selectMVM.Period);
                    registers.Add(register);
                }
            }
        }

        return new MedicalPrescription(creteVM.CRMMedic, patient, registers);
    }

    public static DetailsPrescriptionViewModel DetailVM(this MedicalPrescription prescription)
    {
        return new DetailsPrescriptionViewModel(prescription.Id, prescription.DoctorCRM, prescription.Patient.Name, prescription.Date, prescription.Medications);
    }
}
