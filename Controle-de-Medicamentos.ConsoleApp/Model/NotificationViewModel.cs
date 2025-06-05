namespace Controle_de_Medicamentos.ConsoleApp.Model;

public class NotificationViewModel
{
    public string TitleAction { get; set; }
    public string RegisterType { get; set; }
    public string RegisterName { get; set; }
    public string Action { get; set; }
    public string Linkdestination { get; set; }


    public NotificationViewModel(string titleAction, string registerType, string registerName, string action, string linkdestination)
    {
        TitleAction = titleAction;
        RegisterType = registerType;
        RegisterName = registerName;
        Action = action;
        Linkdestination = linkdestination;
    }
}