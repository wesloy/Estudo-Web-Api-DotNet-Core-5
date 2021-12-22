using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Categorizador.Application.Dtos
{
    public class GrupoUpdateDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Id { get; set; }


        [Display(Name = "Descrição Grupo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(15, ErrorMessage = "{0} deve ter no máximo 50 caracteres.")]
        public string DescricaoGrupo { get; set; }

        
        /// <summary>
        /// Quando o registro está inativado, não lista para o usuário final.
        /// </summary>   
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Ativo { get; set; }        


    }
}