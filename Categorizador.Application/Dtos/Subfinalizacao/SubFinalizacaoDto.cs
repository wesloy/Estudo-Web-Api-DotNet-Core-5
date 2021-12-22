using System.ComponentModel.DataAnnotations;
using Categorizador.Application.Outros;

namespace Categorizador.Application.Dtos
{
    public class SubFinalizacaoDto
    {
        public int Id { get; set; }

        
        [Display(Name = "Descrição Subfinalização")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "{0} deve ter no máximo 30 caracteres.")]
        public string DescricaoSubFinalizacao { get; set; }


        /// <summary>
        /// Quando o registro está inativado, não lista para o usuário final.
        /// </summary>        
        public bool Ativo { get; set; } 
        

        /// <summary>
        /// Finalização a qual esta subfinalização está associada
        /// </summary>        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int FinalizacaoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public FinalizacaoDto Finalizacao { get; set; }
        /// <summary>
        /// Criar um novo registro de trabalho em uma determinada fila para fazer acompanhamentos e retornos.
        /// </summary> 

#region Follows
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool GerarNovoCaso { get; set; }

        /// <summary>
        /// Chave estrangeira que é carraga apenas quando existe roteamento de caso para uma nova fila.
        /// </summary>                
        [RequiredIfAttribute("GerarNovoCaso","true")]
        public int? FilaNovoCasoId { get; set; }

        [RequiredIfAttribute("GerarNovoCaso","true")]
        public FilaDto FilaNovoCaso { get; set; }
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