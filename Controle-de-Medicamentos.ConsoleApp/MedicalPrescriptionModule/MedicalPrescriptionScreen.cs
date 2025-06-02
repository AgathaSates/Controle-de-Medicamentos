﻿using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

public class MedicalPrescriptionScreen : BaseScreen<MedicalPrescription>, ICrudScreen
{
    MedicationScreen MedicationScreen { get; set; }
    IMedicationRepository MedicationRepository { get; set; }

    public MedicalPrescriptionScreen(IMedicalPrescriptionRepository repository, MedicationScreen medicationScreen, IMedicationRepository medicationRepository) : base(repository, "Prescrição Médica")
    {
        MedicationScreen = medicationScreen;
        MedicationRepository = medicationRepository;
    }
    
    public override void ShowMenu()
    {
        string[] options = new[] {"Cadastrar Prescrição Médica", "Gerar relatórios de Prescrições Médicas", "Voltar" };

        base.ShowMenu("Gerenciamento de Prescrições Médicas", options, ExecuteOption);
    }

    protected override bool ExecuteOption(int indexSelected)
    {
        switch (indexSelected)
        {
            case 0: Add(); break;
            case 1: ShowAll(true, true); break;
            case 2: return true;
            default: Write.InvalidOption(); break;
        }
        return false;
    }

    public override void Add()
    {
        Console.Clear();
        if (!MedicationScreen.ExistRegisters())
            return;
        base.Add();
    }

    protected override MedicalPrescription NewEntity()
    {
        Write.InColor("> Digite o CRM do médico (6 dígitos): ", ConsoleColor.Yellow, true);
        string doctorCRM = Console.ReadLine()!.Trim().ToTitleCase();

        Write.InColor("> Digite quantos tipos de medicamentos que irá na prescrição: ", ConsoleColor.Yellow, true);
        int quantity = Validator.GetValidInt();

        List<PrescriptionMedication> medications = NewPrescriptionMedication(quantity);

        return new MedicalPrescription(doctorCRM, medications);
    }

    /// <summary>
    /// Cria uma lista de medicamentos prescritos com base na entrada do usuário.
    /// </summary>
    /// <param name="quantity">Número de medicamentos a serem adicionados na prescrição.</param>
    /// <returns>Lista de objetos <see cref="PrescriptionMedication"/> válidos.</returns>
    /// <remarks>
    /// Para cada medicamento, o sistema exibe os medicamentos disponíveis, solicita ID, dosagem, quantidade e posologia. <br/>
    /// O processo se repete até que os dados fornecidos sejam válidos, utilizando <see cref="IsValid"/> para validação.
    /// </remarks>
    private List<PrescriptionMedication> NewPrescriptionMedication(int quantity)
    {
        var medications = new List<PrescriptionMedication>();
        for (int i = 0; i < quantity; i++)
        {
            while (true)
            {
                MedicationScreen.ShowAll(false, false);
                Write.InColor($"> Digite o id do medicamento N°{i + 1}: ", ConsoleColor.Yellow, true);
                string medicationIdInput = Console.ReadLine()!.Trim();

                if (!Guid.TryParse(medicationIdInput, out Guid medicationId))
                {
                    Write.InColor(">> (×) ID inválido! Por favor, insira um ID válido.", ConsoleColor.Red);
                    Write.TryAgain();
                    continue;
                }

                Medication? medication = MedicationRepository.GetById(medicationId);

                Write.InColor($"> Digite a dosagem do medicamento N°{i + 1} (Apenas o valor): ", ConsoleColor.Yellow, true);
                string dosage = Console.ReadLine()!.Trim().ToTitleCase();

                Write.InColor($"> Digite a quantidade de comprimidos do medicamento N°{i + 1}: ", ConsoleColor.Yellow, true);
                int medicationQuantity = Validator.GetValidInt();

                Write.InColor($"> Digite a posologia do medicamento N°{i + 1} (ex: 1 por dia): ", ConsoleColor.Yellow, true);
                string period = Console.ReadLine()!.Trim().ToTitleCase();

                PrescriptionMedication prescriptionMedication = new PrescriptionMedication(medication, dosage, medicationQuantity, period);

                if (!IsValid(prescriptionMedication))
                    continue;

                medications.Add(prescriptionMedication);

                Write.Loading();

                Write.InColor($">> Medicamento N°{i + 1} adicionado com sucesso!", ConsoleColor.Green);
                Write.Exit();
                break;
            }
        }
        return medications;
    }

    /// <summary>
    /// Valida os campos de um medicamento prescrito.
    /// </summary>
    /// <param name="prescription">Instância de <see cref="PrescriptionMedication"/> a ser validada.</param>
    /// <returns>
    /// <c>true</c> se a prescrição for válida; caso contrário, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Em caso de erro, exibe mensagens descritivas no console e solicita nova tentativa.
    /// </remarks>
    public bool IsValid(PrescriptionMedication prescription)
    {
        string errors = prescription.Validate();
        if (string.IsNullOrEmpty(errors))
            return true;
        Write.InColor($">> (×) Erro ao cadastrar o Medicamento!", ConsoleColor.Red);
        Write.InColor(errors, ConsoleColor.Red);
        Write.TryAgain();
        return false;
    }

    public override string[] GetHeaders()
    {
        return new string[] { "Id", "CRM do Médico", "Data", "Qtd. Medicamentos", "Status" };
    }

    public override void PrintRow(string[] row, int[] widths)
    {
        Console.Write("│");
        for (int i = 0; i < row.Length; i++)
        {
            string cell = row[i];
            string padded = cell.PadRight(widths[i]);

            var originalColor = Console.ForegroundColor;

            if (cell == "Expirada")
                Console.ForegroundColor = ConsoleColor.Red;
            else if (cell == "Fechada")
                Console.ForegroundColor = ConsoleColor.Green;
            else if (cell == "Aberta")
                Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write(" " + padded + " ");

            Console.ForegroundColor = originalColor;
            Console.Write("│");
        }
        Console.WriteLine();
    }
}

