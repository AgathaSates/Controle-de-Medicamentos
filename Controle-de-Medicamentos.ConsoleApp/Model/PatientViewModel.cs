using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;

namespace Controle_de_Medicamentos.ConsoleApp.Model;

public abstract class FormPatientViewModel
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string SUSCard { get; set; }
}

public class CreatePatientViewModel : FormPatientViewModel
{
    public CreatePatientViewModel() { }
    public CreatePatientViewModel(string name, string phone, string susCard) : this()
    {
        Name = name;
        Phone = phone;
        SUSCard = susCard;
    }
}

public class EditPatientViewModel : FormPatientViewModel
{
    public Guid Id { get; set; }
    public EditPatientViewModel() { }
    public EditPatientViewModel(Guid id, string name, string phone, string susCard) : this()
    {
        Id = id;
        Name = name;
        Phone = phone;
        SUSCard = susCard;
    }
}

public class DeletePatientViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public DeletePatientViewModel() { }
    public DeletePatientViewModel(Guid id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}

public class ViewPatientViewModel
{
    public List<DetailPatientViewModel> Registers { get; }

    public ViewPatientViewModel(List<Patient> patients)
    {
        Registers = [];

        foreach (var p in patients)
        {
            var detailVM = p.DetailVM();

            Registers.Add(detailVM);
        }
    }
}

public class DetailPatientViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string SUSCard { get; set; }
    public DetailPatientViewModel(Guid id, string name, string phone, string susCard)
    {
        Id = id;
        Name = name;
        Phone = phone;
        SUSCard = susCard;
    }
}