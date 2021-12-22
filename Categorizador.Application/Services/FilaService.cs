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
    public class FilaService : IFilaService
    {
        private readonly IGeralRepository _geralRepository;
        private readonly IFilaRepository _filaRepository;
        private readonly IMapper _mapper;


        public FilaService(IGeralRepository geralRepository,
                                IFilaRepository filaRepository,
                                    IMapper mapper)
        {
            _geralRepository = geralRepository;
            _filaRepository = filaRepository;
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
        public async Task<FilaDto> AddRegister(FilaInsertDto model)
        {
            try
            {

                //mapeando objeto para classe de domínio
                var classeDominio = _mapper.Map<Fila>(model);

                //Criando o registro, como se fizesse um COMIT
                _geralRepository.Add<Fila>(classeDominio);

                //Efetivando a criação no BD, como se fizesse o PUSH
                //Validando se foi com sucesso, desta forma pode-se retornar uma informação mais precisa para o Controller
                if (await _geralRepository.SaveChangesAsync())
                {
                    //retornando o recém registro adicionado, como prova que tudo deu certo
                    //mapeando da classe de domínio para um objeto, conforme scopo do método
                    var retorno = await _filaRepository.GetRegisterByIdAsync(classeDominio.Id, true);
                    return _mapper.Map<FilaDto>(retorno);

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
        /// <param name="filaId"></param>
        /// <returns>
        /// Se ok, retorna registro atualizado.
        /// Se não conseguir atualizar, mas não for um erro, retorna Null.
        /// Se houver erro envia a Mensagem ao Controller.
        /// </returns>
        public async Task<FilaDto> UpdateRegister(FilaUpdateDto model, int filaId)
        {
            try
            {
                //verificando se o registro que o Controller quer atualizar existe
                //se não exister retorna null para o Controller
                var registroExiste = await _filaRepository.GetRegisterByIdAsync(filaId, false);
                if (registroExiste == null) return null;

                //tratando qualquer falta de passagem de id pelo Controller
                model.Id = registroExiste.Id;

                //mapeando objeto para classe de domínio
                var classeDominio = _mapper.Map<Fila>(model);

                //Aplicando as alterações, como se fizesse um COMIT
                _geralRepository.Update<Fila>(classeDominio);
                //Efetivando a alteração no BD, como se fizesse o PUSH
                //Validando se foi com sucesso, desta forma pode-se retornar uma informação mais precisa para o Controller
                if (await _geralRepository.SaveChangesAsync())
                {
                    //retornando o recém registro atualizado, como prova que tudo deu certo
                    //mapeando da classe de domínio para um objeto, conforme scopo do método
                    var retorno = await _filaRepository.GetRegisterByIdAsync(classeDominio.Id, true);
                    return _mapper.Map<FilaDto>(retorno);
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
        /// <param name="filaId"></param>
        /// <returns>
        /// Verdadeiro se deletado ou Falso caso não consiga. 
        /// Envia mensagem ao Controller se caso o registro solicitado não exista para deleção.
        /// </returns>
        public async Task<bool> DeleteRegiter(int filaId)
        {
            try
            {
                //verificando se o registro que o Controller quer deletar existe
                //se não exister retorna mensagem para o Controller
                var registroExiste = await _filaRepository.GetRegisterByIdAsync(filaId, false);
                if (registroExiste == null) throw new Exception("Registro não localizado para deleção");


                //Deletando o registro, como se fizesse um COMIT
                _geralRepository.Delete<Fila>(registroExiste);
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
        /// <param name="incluirFinalizacoesVinculadas"></param>
        /// <returns>Array de registros</returns>
        public async Task<FilaDto[]> GetAllRegistersAsync(bool incluirFinalizacoesVinculadas)
        {
             try
            {
                var filas = await _filaRepository.GetAllRegistersAsync(true);
                if (filas == null) return null;
                
                var resultado = _mapper.Map<FilaDto[]>(filas);
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
        /// <param name="incluirFinalizacoesVinculadas"></param>
        /// <returns>Array de registros</returns>
        public async Task<FilaDto[]> GetAllRegistersByNameAsync(string name, bool incluirFinalizacoesVinculadas)
        {
            try
            {
                var filas = await _filaRepository.GetAllRegistersByNameAsync(name, true);
                if (filas == null) return null;
                
                var resultado = _mapper.Map<FilaDto[]>(filas);
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
        /// <param name="filaId"></param>
        /// <param name="incluirFinalizacoesVinculadas"></param>
        /// <returns>Apenas um registro</returns>
        public async Task<FilaDto> GetRegisterByIdAsync(int filaId, bool incluirFinalizacoesVinculadas)
        {
            try
            {
                var fila = await _filaRepository.GetRegisterByIdAsync(filaId, true);
                if (fila == null) return null;

                var resultado = _mapper.Map<FilaDto>(fila);
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