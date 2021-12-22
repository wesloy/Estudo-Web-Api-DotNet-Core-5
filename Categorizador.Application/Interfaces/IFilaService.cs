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
    
    public interface IFilaService
    {

        Task<FilaDto> AddRegister(FilaInsertDto model);
        Task<FilaDto> UpdateRegister(FilaUpdateDto model, int filaId);
        Task<bool> DeleteRegiter(int filaId);
        Task<FilaDto[]> GetAllRegistersAsync(bool incluirFinalizacoesVinculadas);
        Task<FilaDto[]> GetAllRegistersByNameAsync(string name, bool incluirFinalizacoesVinculadas);
        Task<FilaDto> GetRegisterByIdAsync(int filaId, bool incluirFinalizacoesVinculadas);
    }
}