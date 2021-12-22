using System.Threading.Tasks;
using Categorizador.Domain;

namespace Categorizador.Persistence.Interfaces
{
    /// <summary>
    /// Interface exclusiva para uma tabela/repositorio, ou seja, 
    /// recebe apenas Gets relacionados a uma tabela/repositorio específico.
    /// Gera a possibilidade de incluir os registros vinculados por FK ou não (bool incluirFinalizacoesVinculadas). Na camada Repository este 
    /// parâmetro pode ser alternativo, mas na Interface ele é obrigatório.
    /// </summary>
    public interface IFilaRepository
    {
        Task<Fila[]> GetAllRegistersAsync(bool incluirFinalizacoesVinculadas);
         Task<Fila[]> GetAllRegistersByNameAsync(string name, bool incluirFinalizacoesVinculadas);
         Task<Fila> GetRegisterByIdAsync(int filaId, bool incluirFinalizacoesVinculadas);
         
    }
}