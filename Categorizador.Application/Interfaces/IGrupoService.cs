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
    
    public interface IGrupoService
    {
         Task<GrupoDto> AddRegister(GrupoInsertDto model);
         Task<GrupoDto> UpdateRegister(GrupoUpdateDto model, int grupoId);
         Task<bool> DeleteRegiter(int grupoId);         
         Task<GrupoDto[]> GetAllRegistersAsync(bool incluirFilasVinculadas);
         Task<GrupoDto[]> GetAllRegistersByNameAsync(string name, bool incluirFilasVinculadas);
         Task<GrupoDto> GetRegisterByIdAsync(int grupoId, bool incluirFilasVinculadas);
    }
}