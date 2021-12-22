using System.Collections.Generic;

namespace Categorizador.Domain
{
    /// <summary>
    /// Filas de Trabalho
    /// </summary>
    /// <remarks>
    /// Objetivo: 
    /// Listar registros suspeitos, alertados de uma mesma ferramenta de segurança ou comportamento.
    /// </remarks>
    public class Fila
    {
        public int Id { get; set; }

        public string DescricaoFila { get; set; }

         /// <summary>
        /// Quando o registro está inativado, não lista para o usuário final.
        /// </summary>   
        public bool Ativo { get; set; } 

        /// <summary>
        /// Tempo máximo em dias que se espera que um registro de trabalho seja atendido.
        /// </summary>        
        public int Sla { get; set; }

        /// <summary>
        /// Descrição macro da função da fila.
        /// </summary>        
        public string RegraDeNegocio { get; set; }

        /// <summary>
        /// Deternima se haverá necessidade de importação de arquivos externos.
        /// </summary>        
        public bool ImportacaoManual { get; set; }

        /// <summary>
        /// Grupo ao qual a fila pertence.
        /// </summary>
        /// <exemple>DLP ou TAMNUN</exemple>      
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }

        /// <summary>
        /// Todas as Finalizações associadas a esta fila.
        /// </summary>        
        public IEnumerable<Finalizacao> Finalizacoes { get; set; }
    }
}