using System;
using System.Threading.Tasks;
using AutoMapper;
using Categorizador.Application.Dtos;
using Categorizador.Application.Interfaces;
using Categorizador.Domain;
using Categorizador.Persistence.Interfaces;

namespace Categorizador.Application.Services
{
    /// <summary>
    /// Services, aplica o contrato das Interfaces desta mesma camada.
    /// Invoca as Interfaces da camada de Persistência relacionadas ao Serviço/Repositório alvo.
    /// Nesta camada que se faz as tratativas de erros, para ser enviado ao Controller.
    /// </summary>
    public class GrupoService : IGrupoService
    {
        public readonly IGeralRepository _geralRepository ;
        public readonly IGrupoRepository _grupoRepository ;
        public readonly IMapper _mapper ;
        public GrupoService(IGeralRepository geralRepository,
                                IGrupoRepository grupoRepository, 
                                IMapper mapper)
        {
            _geralRepository = geralRepository;
            _grupoRepository = grupoRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Adicionar registro no BD
        /// Com parâmetro modelado (model) na composição da tabela (Domain)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// Se ok, retorna registro ADD.
        /// Se não conseguir ADD, mas não for um erro, retorna Null.
        /// Se houver erro envia a Mensagem ao Controller.
        /// </returns>
        public async Task<GrupoDto> AddRegister(GrupoInsertDto model)
        {
            try
            {

                //mapeando objeto para classe de domínio
                var classeDominio = _mapper.Map<Grupo>(model);

                //Criando o registro, como se fizesse um COMIT
                _geralRepository.Add<Grupo>(classeDominio);

                //Efetivando a criação no BD, como se fizesse o PUSH
                //Validando se foi com sucesso, desta forma pode-se retornar uma informação mais precisa para o Controller
                if (await _geralRepository.SaveChangesAsync())
                {
                    //retornando o recém registro adicionado, como prova que tudo deu certo
                    //mapeando da classe de domínio para um objeto, conforme scopo do método
                    var retorno = await _grupoRepository.GetRegisterByIdAsync(classeDominio.Id, true);
                    return _mapper.Map<GrupoDto>(retorno);

                } 
                
                //caso não houve adição volta nulo
                return null;
                
            }
            catch (Exception ex)
            {
                //retornando erro ao Controller
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Atualiza registro no BD
        /// Com parâmetro modelado (model) na composição da tabela (Domain)
        /// E o Id do registro alvo
        /// </summary>
        /// <param name="model"></param>
        /// <param name="grupoId"></param>
        /// <returns>
        /// Se ok, retorna registro atualizado.
        /// Se não conseguir atualizar, mas não for um erro, retorna Null.
        /// Se houver erro envia a Mensagem ao Controller.
        /// </returns>
        public async Task<GrupoDto> UpdateRegister(GrupoUpdateDto model, int grupoId)
        {
            try
            {
                //verificando se o registro que o Controller quer atualizar existe
                //se não exister retorna null para o Controller
                var registroExiste = await _grupoRepository.GetRegisterByIdAsync(grupoId, false);
                if (registroExiste == null) return null;

                //tratando qualquer falta de passagem de id pelo Controller
                model.Id = registroExiste.Id;

                //mapeando objeto para classe de domínio
                var classeDominio = _mapper.Map<Grupo>(model);

                //Aplicando as alterações, como se fizesse um COMIT
                _geralRepository.Update<Grupo>(classeDominio);
                //Efetivando a alteração no BD, como se fizesse o PUSH
                //Validando se foi com sucesso, desta forma pode-se retornar uma informação mais precisa para o Controller
                if (await _geralRepository.SaveChangesAsync())
                {
                    //retornando o recém registro atualizado, como prova que tudo deu certo
                    //mapeando da classe de domínio para um objeto, conforme scopo do método
                    var retorno = await _grupoRepository.GetRegisterByIdAsync(classeDominio.Id, true);
                    return _mapper.Map<GrupoDto>(retorno);
                } 
                return null;
                
            }
            catch (Exception ex)
            {
                //retornando erro ao Controller
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Deletar registro da Tabela, pelo Id específico
        /// </summary>
        /// <param name="grupoId"></param>
        /// <returns>
        /// Verdadeiro se deletado ou Falso caso não consiga. 
        /// Envia mensagem ao Controller se caso o registro solicitado não exista para deleção.
        /// </returns>
        public async Task<bool> DeleteRegiter(int grupoId)
        {
             try
            {
                //verificando se o registro que o Controller quer deletar existe
                //se não exister retorna mensagem para o Controller
                var registroExiste = await _grupoRepository.GetRegisterByIdAsync(grupoId, false);
                if (registroExiste == null) throw new Exception("Registro não localizado para deleção");


                //Deletando o registro, como se fizesse um COMIT
                _geralRepository.Delete<Grupo>(registroExiste);
                //Efetivando a deleção é retornando boolean ao Controller
                return await _geralRepository.SaveChangesAsync();

                
            }
            catch (Exception ex)
            {
                //retornando erro ao Controller
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Capturar todos os registros, podendo optar em incluir ou não registros vinculados por FK.
        /// </summary>
        /// <param name="incluirFilasVinculadas"></param>
        /// <returns>Array de registros</returns>
        public async Task<GrupoDto[]> GetAllRegistersAsync(bool incluirFilasVinculadas)
        {
            try
            {
                var grupos = await _grupoRepository.GetAllRegistersAsync(true);
                if (grupos == null) return null;
                
                var resultado = _mapper.Map<GrupoDto[]>(grupos);
                return resultado;

            }
            catch (Exception ex)
            {
                
                //retornando erro ao Controller
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Capturar todos os registros, podendo optar em incluir ou não registros vinculados por FK.
        /// Filtrando os registros por descrição/nome.
        /// </summary>
        /// <param name="incluirFilasVinculadas"></param>
        /// <returns>Array de registros</returns>
        public async Task<GrupoDto[]> GetAllRegistersByNameAsync(string name, bool incluirFilasVinculadas)
        {
            try
            {
                var grupos = await _grupoRepository.GetAllRegistersByNameAsync(name, true);
                if (grupos == null) return null;
                
                var resultado = _mapper.Map<GrupoDto[]>(grupos);
                return resultado;
            }
            catch (Exception ex)
            {
                
                //retornando erro ao Controller
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Capturar registro único, podendo optar em incluir ou não registros vinculados por FK.
        /// Filtrando os registros pelo Id.
        /// </summary>
        /// <param name="grupoId"></param>
        /// <param name="incluirFilasVinculadas"></param>
        /// <returns>Apenas um registro</returns>
        public async Task<GrupoDto> GetRegisterByIdAsync(int grupoId, bool incluirFilasVinculadas)
        {
            try
            {
                var grupo = await _grupoRepository.GetRegisterByIdAsync(grupoId, true);
                if (grupo == null) return null;

                var resultado = _mapper.Map<GrupoDto>(grupo);
                return resultado;
            }
            catch (Exception ex)
            {                
                //retornando erro ao Controller
                throw new Exception(ex.Message);
            }
        }
    }
}