using System;
using System.Threading.Tasks;
using Categorizador.Application.Dtos;
using Categorizador.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Categorizador.Api.Controllers
{
    [ApiController] //forma de interpretação do MVC
    [Route("api/[controller]")] //rota para acessar essa controller
    public class SubFinalizacaoController : ControllerBase
    {
        /// <summary>
        /// Construtor da classe, importando contratos externos
        /// </summary>
        public ISubFinalizacaoService _subFinalizacaoService { get; }
        public SubFinalizacaoController(ISubFinalizacaoService subFinalizacaoService)
        {
            _subFinalizacaoService = subFinalizacaoService;

        }
          /// <summary>
        /// Capturar todos os registros.
        /// </summary>
        /// <returns>Array de registros ou mensagem de erro</returns>
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
             try
            {
                var registros = await _subFinalizacaoService.GetAllRegistersAsync();
                if (registros == null) return NoContent();

                return Ok(registros);

            }
            catch (Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        $"Erro ao tentar recuperar registros. Erro: {ex.Message}");
            }

        }

        /// <summary>
        /// Capturar registro específico, filtrado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Json com registro específico ou mensagem de erro</returns>
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
             try
            {
                var registro = await _subFinalizacaoService.GetRegisterByIdAsync(id);
                if (registro == null) return NoContent();
                
                return Ok(registro);
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        $"Erro ao tentar recuperar registros. Erro: {ex.Message}");
                
            }
        }

        /// <summary>
        /// Incluir um novo registro
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Json com o novo registro ou mensagem de erro</returns>
        [HttpPost]
        public async Task<IActionResult> Post(SubFinalizacaoInsertDto model) 
        {
            try
            {
                var registro = await _subFinalizacaoService.AddRegister(model);
                if (registro == null) return NoContent();

                return Ok(registro);
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        $"Erro ao tentar adicionar registro. Erro: {ex.Message}");
                
            }
        }

        /// <summary>
        /// Atualizar registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Registro atualizado ou mensagem de erro</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put (int id, SubFinalizacaoUpdateDto model) 
        {
             try
            {
                var registro = await _subFinalizacaoService.UpdateRegister(model, id);
                if (registro == null) return NoContent();

                return Ok(registro);
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        $"Erro ao tentar atualizar registro. Erro: {ex.Message}");
                
            }
        }

        /// <summary>
        /// Deletar registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mensagem da excução do método</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                //Usando comparação ternária, para substituir o if simples
                return await _subFinalizacaoService.DeleteRegiter(id)
                        ? Ok("Registro Deletado")
                        : BadRequest("Registro não deletado!");
                        
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        $"Erro ao tentar deletar um registro. Erro: {ex.Message}");
                
            }
        }

    }
}