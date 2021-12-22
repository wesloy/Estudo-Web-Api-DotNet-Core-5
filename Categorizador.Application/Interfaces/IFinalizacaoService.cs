using System.Threading.Tasks;
using Categorizador.Application.Dtos;

namespace Categorizador.Application.Interfaces
{
    /// <summary>    
    /// Funciona como um contrato, pra obrigar a classe que a herda a implantar todos os métodos sem exceção.
    /// Já obrigando passar os parâmetros corretos desta camada para a camada de Persistência.
    /// Possui a definicação de todo o CRUD necessário para o Repositório alvo.
    /// Utilizando de objeto de transferência (dto) e não o domínio
    /// </summary>
    
    public interface IFinalizacaoService
    {
        Task<FinalizacaoDto> AddRegister(FinalizacaoInsertDto model);
        Task<FinalizacaoDto> UpdateRegister(FinalizacaoUpdateDto model, int finalizacaoId);
        Task<bool> DeleteRegiter(int finalizacaoId);
        Task<FinalizacaoDto[]> GetAllRegistersAsync(bool incluirSubFinalizacoesVinculadas);
        Task<FinalizacaoDto[]> GetAllRegistersByNameAsync(string name, bool incluirSubFinalizacoesVinculadas);
        Task<FinalizacaoDto> GetRegisterByIdAsync(int finalizacaoId, bool incluirSubFinalizacoesVinculadas);           
    }
}