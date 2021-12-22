using System;
using System.Threading.Tasks;
using AutoMapper;
using Categorizador.Application.Dtos;
using Categorizador.Application.Interfaces;
using Categorizador.Domain;
using Categorizador.Persistence.Interfaces;

namespace Categorizador.Application.Services
{
    public class SubFinalizacaoService : ISubFinalizacaoService
    {

        private readonly IGeralRepository _geralRepository;
        private readonly ISubFinalizacaoRepository _subfinalizacaoRepository;
        private readonly IMapper _mapper;

        
        public SubFinalizacaoService(IGeralRepository geralRepository,
                                        ISubFinalizacaoRepository subfinalizacaoRepository,
                                            IMapper mapper)
        {
            _geralRepository = geralRepository;
            _subfinalizacaoRepository = subfinalizacaoRepository;
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
        public async Task<SubFinalizacaoDto> AddRegister(SubFinalizacaoInsertDto model)
        {
            try
            {

                //mapeando objeto para classe de domínio
                var classeDominio = _mapper.Map<SubFinalizacao>(model);

                //Criando o registro, como se fizesse um COMIT
                _geralRepository.Add<SubFinalizacao>(classeDominio);

                //Efetivando a criação no BD, como se fizesse o PUSH
                //Validando se foi com sucesso, desta forma pode-se retornar uma informação mais precisa para o Controller
                if (await _geralRepository.SaveChangesAsync())
                {
                    //retornando o recém registro adicionado, como prova que tudo deu certo
                    //mapeando da classe de domínio para um objeto, conforme scopo do método
                    var retorno = await _subfinalizacaoRepository.GetRegisterByIdAsync(classeDominio.Id);                    
                    return _mapper.Map<SubFinalizacaoDto>(retorno);

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
        /// <param name="subFinalizacaoId"></param>
        /// <returns>
        /// Se ok, retorna registro atualizado.
        /// Se não conseguir atualizar, mas não for um erro, retorna Null.
        /// Se houver erro envia a Mensagem ao Controller.
        /// </returns>
        public async Task<SubFinalizacaoDto> UpdateRegister(SubFinalizacaoUpdateDto model, int subFinalizacaoId)
        {
            try
            {
                //verificando se o registro que o Controller quer atualizar existe
                //se não exister retorna null para o Controller
                var registroExiste = await _subfinalizacaoRepository.GetRegisterByIdAsync(subFinalizacaoId);
                if (registroExiste == null) return null;

                //tratando qualquer falta de passagem de id pelo Controller
                model.Id = registroExiste.Id;

                //mapeando objeto para classe de domínio
                var classeDominio = _mapper.Map<Finalizacao>(model);

                //Aplicando as alterações, como se fizesse um COMIT
                _geralRepository.Update<Finalizacao>(classeDominio);
                //Efetivando a alteração no BD, como se fizesse o PUSH
                //Validando se foi com sucesso, desta forma pode-se retornar uma informação mais precisa para o Controller
                if (await _geralRepository.SaveChangesAsync())
                {
                    //retornando o recém registro atualizado, como prova que tudo deu certo
                    //mapeando da classe de domínio para um objeto, conforme scopo do método
                    var retorno = await _subfinalizacaoRepository.GetRegisterByIdAsync(classeDominio.Id);
                    return _mapper.Map<SubFinalizacaoDto>(retorno);
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
        /// <param name="subFinalizacaoId"></param>
        /// <returns>
        /// Verdadeiro se deletado ou Falso caso não consiga. 
        /// Envia mensagem ao Controller se caso o registro solicitado não exista para deleção.
        /// </returns>
        public async Task<bool> DeleteRegiter(int subFinalizacaoId)
        {
            try
            {
                //verificando se o registro que o Controller quer deletar existe
                //se não exister retorna mensagem para o Controller
                var registroExiste = await _subfinalizacaoRepository.GetRegisterByIdAsync(subFinalizacaoId);
                if (registroExiste == null) throw new Exception("Registro não localizado para deleção");


                //Deletando o registro, como se fizesse um COMIT
                _geralRepository.Delete<SubFinalizacao>(registroExiste);
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
        /// Capturar todos os registro.
        /// </summary>        
        /// <returns>Array de registros</returns>
        public async Task<SubFinalizacaoDto[]> GetAllRegistersAsync()
        {
            try
            {
                var subfinalizacaos = await _subfinalizacaoRepository.GetAllRegistersAsync();
                if (subfinalizacaos == null) return null;
                
                var resultado = _mapper.Map<SubFinalizacaoDto[]>(subfinalizacaos);
                return resultado;

            }
            catch (Exception ex)
            {
                
                //retornando erro ao Controller
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Capturar todos os registro, filtrando pela descrição.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SubFinalizacaoDto[]> GetAllRegistersByNameAsync(string name)
        {
            try
            {
                var subfinalizacaos = await _subfinalizacaoRepository.GetAllRegistersByNameAsync(name);
                if (subfinalizacaos == null) return null;
                
                var resultado = _mapper.Map<SubFinalizacaoDto[]>(subfinalizacaos);
                return resultado;
            }
            catch (Exception ex)
            {
                
                //retornando erro ao Controller
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Capturar registro único.
        /// Filtrando os registros pelo Id.
        /// </summary>
        /// <param name="subfinalizacaoId"></param>        
        /// <returns>Apenas um registro</returns>
        public async Task<SubFinalizacaoDto> GetRegisterByIdAsync(int subFinalizacaoId)
        {
            try
            {
                var subfinalizacao = await _subfinalizacaoRepository.GetRegisterByIdAsync(subFinalizacaoId);
                if (subfinalizacao == null) return null;

                var resultado = _mapper.Map<SubFinalizacaoDto>(subfinalizacao);
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