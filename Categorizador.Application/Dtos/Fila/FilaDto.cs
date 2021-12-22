using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Categorizador.Application.Dtos
{
    public class FilaDto
    {
         public int Id { get; set; }

        [Display(Name = "Descrição Fila")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(15, ErrorMessage = "{0} deve ter no máximo 15 caracteres.")]        
        public string DescricaoFila { get; set; }

         /// <summary>
        /// Quando o registro está inativado, não lista para o usuário final.
        /// </summary>   
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Ativo { get; set; } 

        /// <summary>
        /// Tempo máximo em dias que se espera que um registro de trabalho seja atendido.
        /// </summary>        
        [Range(1,14, ErrorMessage = "{0} deve estar entre 1 e 14")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")] 
        public int Sla { get; set; }

        /// <summary>
        /// Descrição macro da função da fila.
        /// </summary>        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(250, ErrorMessage = "{0} deve ter no máximo 250 caracteres.")]
        public string RegraDeNegocio { get; set; }

        /// <summary>
        /// Deternima se haverá necessidade de importação de arquivos externos.
        /// </summary>     
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool ImportacaoManual { get; set; }

        /// <summary>
        /// Grupo ao qual a fila pertence.
        /// </summary>
        /// <exemple>DLP ou TAMNUN</exemple>      
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int GrupoId { get; set; }
        public GrupoDto Grupo { get; set; }

        /// <summary>
        /// Todas as Finalizações associadas a esta fila.
        /// </summary>        
        public IEnumerable<FinalizacaoDto> Finalizacoes { get; set; }
    }
}