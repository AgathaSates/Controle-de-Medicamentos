namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

public abstract class BaseEntity<T>
{
    public Guid Id { get; set; }

    /// <summary>
    /// Atualiza os dados da entidade com base nas informações fornecidas pelo usuário.
    /// </summary>
    /// <param name="entity">Instância da entidade que será modificada.</param>
    /// <remarks>
    /// Este método deve ser implementado nas classes derivadas para aplicar atualizações específicas nos campos da entidade.
    /// </remarks>
    public abstract void UpdateEntity(T entity);

}
