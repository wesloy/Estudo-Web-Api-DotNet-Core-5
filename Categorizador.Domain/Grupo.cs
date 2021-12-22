using System.Collections.Generic;
using System;

namespace Categorizador.Domain
{
    /// <summary>
    /// Grupos de Filas de Trabalho
    /// </summary>
    /// <remarks>
    /// Objetivo: 
    /// Aglomerar as filas em um nicho de trabalho, facilitando estudos futuros.
    /// </remarks>
    public class Grupo
    {
        public int Id { get; set; }
        
        public string DescricaoGrupo { get; set; }

        /// <summary>
        /// Quando o registro está inativado, não lista para o usuário final.
        /// </summary>   
        public bool Ativo { get; set; } 
        

        /// <summary>
        /// Todas as filas vinculadas a este grupo.
        /// </summary>        
        public IEnumerable<Fila> Filas { get; set; }
    }
}