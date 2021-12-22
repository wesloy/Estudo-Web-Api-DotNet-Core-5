using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Categorizador.Application.Dtos
{
    public class GrupoDto
    {
        public int Id { get; set; }


        [Display(Name = "Descrição Grupo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(15, ErrorMessage = "{0} deve ter no máximo 50 caracteres.")]
        public string DescricaoGrupo { get; set; }

        
        /// <summary>
        /// Quando o registro está inativado, não lista para o usuário final.
        /// </summary>   
        public bool Ativo { get; set; }        


        /// <summary>
        /// Todas as filas vinculadas a este grupo.
        /// </summary>        
        public IEnumerable<FilaDto> Filas { get; set; }

    }
}