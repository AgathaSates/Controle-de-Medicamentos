using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.PatientModule
{
    public class Patient : BaseEntity<Patient>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string SUSCard { get; set; }

        public Patient() {}

        public Patient(string name, string phoneNumber, string susCard)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            SUSCard = susCard;
        }

        public override void UpdateEntity(Patient entity)
        {
            Name = entity.Name;
            PhoneNumber = entity.PhoneNumber;
            SUSCard = entity.SUSCard;
        }


        /// <summary>
        /// Compara o número do cartão SUS do paciente atual com o de outro paciente, 
        /// ignorando diferenças de maiúsculas/minúsculas e espaços em branco nas extremidades.
        /// </summary>
        /// <param name="patient">O paciente a ser comparado.</param>
        /// <returns>
        /// Retorna <c>true</c> se os cartões SUS forem iguais (ignorando maiúsculas/minúsculas e espaços); 
        /// caso contrário, <c>false</c>.
        /// </returns>
        public bool IsSameSUSCard(Patient patient)
        {
            return string.Equals(SUSCard?.Trim(), patient?.SUSCard?.Trim(), StringComparison.OrdinalIgnoreCase) && Id != patient.Id;
        }
    }
}

