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
    public interface ISubFinalizacaoService
    {
        Task<SubFinalizacaoDto> AddRegister(SubFinalizacaoInsertDto model);
        Task<SubFinalizacaoDto> UpdateRegister(SubFinalizacaoUpdateDto model, int subFinalizacaoId);
        Task<bool> DeleteRegiter(int subFinalizacaoId);
        Task<SubFinalizacaoDto[]> GetAllRegistersAsync();
        Task<SubFinalizacaoDto[]> GetAllRegistersByNameAsync(string name);
        Task<SubFinalizacaoDto> GetRegisterByIdAsync(int subFinalizacaoId); 
    
    }
}