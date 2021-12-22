using System.Threading.Tasks;
using Categorizador.Domain;

namespace Categorizador.Persistence.Interfaces
{
    /// <summary>
    /// Interface exclusiva para uma tabela/repositorio, ou seja, 
    /// recebe apenas Gets relacionados a uma tabela/repositorio específico.
    /// Gera a possibilidade de incluir os registros vinculados por FK ou não (bool incluirFilasVinculadas). Na camada Repository este 
    /// parâmetro pode ser alternativo, mas na Interface ele é obrigatório.
    /// </summary>
    public interface IGrupoRepository
    {
         Task<Grupo[]> GetAllRegistersAsync(bool incluirFilasVinculadas);
         Task<Grupo[]> GetAllRegistersByNameAsync(string name, bool incluirFilasVinculadas);
         Task<Grupo> GetRegisterByIdAsync(int grupoId, bool incluirFilasVinculadas);
    }
}