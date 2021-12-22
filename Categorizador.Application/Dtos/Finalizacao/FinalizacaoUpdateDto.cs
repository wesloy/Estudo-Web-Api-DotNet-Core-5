using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Categorizador.Application.Outros;

namespace Categorizador.Application.Dtos
{
    public class FinalizacaoUpdateDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
         public int Id { get; set; }


        [Display(Name = "Descrição Finalização")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "{0} deve ter no máximo 30 caracteres.")]
        public string DescricaoFinalizacao { get; set; }

        /// <summary>
        /// Quando o registro está inativado, não lista para o usuário final.
        /// </summary>              
        public bool Ativo { get; set; } 

        /// <summary>
        /// Fila a qual esta finalização está associada.
        /// </summary>   
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int FilaId { get; set; }        
        

#region Follows
        /// <summary>
        /// Criar um novo registro de trabalho em uma determinada fila para fazer acompanhamentos e retornos.
        /// </summary> 
        [Required(ErrorMessage = "O campo {0} é obrigatório")]       
        public bool GerarNovoCaso { get; set; }
        
        /// <summary>
        /// Chave estrangeira que é carraga apenas quando existe roteamento de caso para uma nova fila.
        /// </summary>         
        [RequiredIfAttribute("GerarNovoCaso","true")]
        public int? FilaNovoCasoId { get; set; } 

    
        /// <summary>
        /// Quantidade de tempo em dias, para que o novo caso seja disponibilizado para trabalho.
        /// Sendo que Zero, indica retorno no mesmo dia.
        /// </summary>
        [RequiredIfAttribute("GerarNovoCaso","true")]
        [Range(0,5,ErrorMessage="Valor deve ser entre 0 e 5, sendo que 0 o caso retorna a fila no mesmo dia")]
        public int? AgingNovoCaso { get; set; }
#endregion
  
      
    }
}