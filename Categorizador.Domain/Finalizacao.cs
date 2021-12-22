using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Categorizador.Domain
{
    /// <summary>
    /// Finalizçaão é a categorização principal para indicar o desfecho de uma análise.
    /// </summary>
    /// <remarks>
    /// Objetivo: 
    /// Descrever de forma macro a análise realizada.
    /// Parametrizar valores e ações quando utilizada esta finalização para categorizar um registro de trabalho.
    /// </remarks>
    public class Finalizacao
    {
        public int Id { get; set; }

        public string DescricaoFinalizacao { get; set; }


        /// <summary>
        /// Quando o registro está inativado, não lista para o usuário final.
        /// </summary>        
        public bool Ativo { get; set; } 


        /// <summary>
        /// Fila a qual esta finalização está associada.
        /// </summary>   
        public int FilaId { get; set; }
        public Fila Fila { get; set; }


        /// <summary>
        /// Criar um novo registro de trabalho em uma determinada fila para fazer acompanhamentos e retornos.
        /// </summary>        
        public bool GerarNovoCaso { get; set; }
        /// <summary>
        /// Chave estrangeira que é carraga apenas quando existe roteamento de caso para uma nova fila.
        /// </summary> 
        public int? FilaNovoCasoId { get; set; }
        public string FilaNovoCaso { get; set; }
        /// <summary>
        /// Quantidade de tempo em dias, para que o novo caso seja disponibilizado para trabalho.
        /// Sendo que Zero, indica retorno no mesmo dia.
        /// </summary>        
        public int? AgingNovoCaso { get; set; }

        
        /// <summary>
        /// Todas as Subfinalizações relacionadas a esta finalização.
        /// </summary>        
        public IEnumerable<SubFinalizacao> SubFInalizacoes { get; set; }
    }
}