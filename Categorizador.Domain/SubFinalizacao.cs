using System.ComponentModel.DataAnnotations.Schema;

namespace Categorizador.Domain
{
    /// <summary>
    /// SubFinalizçaão é a categorização secundária, vinculada sempre a uma finalização principal.
    /// </summary>
    /// <remarks>
    /// Objetivo: 
    /// Descrever de forma mais detalhada a análise realizada.
    /// Parametrizar valores e ações quando utilizada esta finalização para categorizar um registro de trabalho.
    /// </remarks>
    public class SubFinalizacao
    {

        public int Id { get; set; }


        public string DescricaoSubFinalizacao { get; set; }


        /// <summary>
        /// Quando o registro está inativado, não lista para o usuário final.
        /// </summary>        
        public bool Ativo { get; set; } 
        

        /// <summary>
        /// Finalização a qual esta subfinalização está associada
        /// </summary>        
        public int FinalizacaoId { get; set; }
        public Finalizacao Finalizacao { get; set; }
        /// <summary>
        /// Criar um novo registro de trabalho em uma determinada fila para fazer acompanhamentos e retornos.
        /// </summary> 



        public bool GerarNovoCaso { get; set; }
        /// <summary>
        /// Chave estrangeira que é carraga apenas quando existe roteamento de caso para uma nova fila.
        /// </summary>                
        public int? FilaNovoCasoId { get; set; }
        public Fila FilaNovoCaso { get; set; }
        /// <summary>
        /// Quantidade de tempo em dias, para que o novo caso seja disponibilizado para trabalho.
        /// Sendo que Zero, indica retorno no mesmo dia.
        /// </summary>        
        public int? AgingNovoCaso { get; set; }
        
    }
}