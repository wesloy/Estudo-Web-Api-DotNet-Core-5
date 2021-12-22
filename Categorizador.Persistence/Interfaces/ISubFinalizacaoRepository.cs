using System.Threading.Tasks;
using Categorizador.Domain;

namespace Categorizador.Persistence.Interfaces
{
/// <summary>
    /// Interface exclusiva para uma tabela/repositorio, ou seja, 
    /// recebe apenas Gets relacionados a uma tabela/repositorio específico.
    /// </summary>
    public interface ISubFinalizacaoRepository
    {
        Task<SubFinalizacao[]> GetAllRegistersAsync();
        Task<SubFinalizacao[]> GetAllRegistersByNameAsync(string name);
        Task<SubFinalizacao> GetRegisterByIdAsync(int subFinalizacaoId);            
        
    }
}