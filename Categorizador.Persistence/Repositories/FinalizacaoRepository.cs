using System.Linq;
using System.Threading.Tasks;
using Categorizador.Domain;
using Categorizador.Persistence.Contexts;
using Categorizador.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Categorizador.Persistence.Repositories
{
    public class FinalizacaoRepository : IFinalizacaoRepository
    {
        private readonly CategorizadorContext _context;
        public FinalizacaoRepository(CategorizadorContext context)
        {
             _context = context;

            //Config para nennhum método "segurar" a transação.
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 

            //Pode ser inserido em cada método, se desejar, exemplo:
            //return await qry.AsNoTracking().ToArrayAsync();
        }

        /// <summary>
        /// Retorna todos os registros.
        /// Incluindo os registros externos, vinculados por FK, caso a Control solicite.
        /// </summary>
        /// <returns>Arraio ordenado por ID</returns>
        public async Task<Finalizacao[]> GetAllRegistersAsync(bool incluirSubFinalizacoesVinculadas)
        {
             
            IQueryable<Finalizacao> qry = _context.Finalizacoes;
            
            //Validação para inclusão das registros vinculados por FK
            if (incluirSubFinalizacoesVinculadas)
            {
                qry = qry.Include(x => x.SubFInalizacoes);                
            }
            
            //Ordenando a qry, usando função Arrow
            qry = qry.OrderBy(x => x.Id);

            return await qry.AsNoTracking().ToArrayAsync();
        }

        /// <summary>
        /// Retorna todos os registros.
        /// Incluindo os registros externos, vinculados por FK, caso a Control solicite.
        /// Filtrado pela descrição (nome).
        /// </summary>
        /// <returns>Arraio ordenado por ID</returns>
        public async Task<Finalizacao[]> GetAllRegistersByNameAsync(string name, bool incluirSubFinalizacoesVinculadas)
        {
            IQueryable<Finalizacao> qry = _context.Finalizacoes;
            
            //Validação para inclusão das registros vinculados por FK
            if (incluirSubFinalizacoesVinculadas)
            {
                qry = qry.Include(x => x.SubFInalizacoes);                
            }
            
            //Ordenando a qry, usando função Arrow
            //Depois filtrando pelo 'name' passado
            qry = qry.OrderBy(x => x.Id)
                        .Where(x => x.DescricaoFinalizacao.ToLower().Contains(name.ToLower())); //convertido tudo para minúsculo para não dar erro de Case Sensitive

            return await qry.ToArrayAsync();
        }

        /// <summary>
        /// Retorna um único registro.
        /// Incluindo os registros externos, vinculados por FK, caso a Control solicite.
        /// Filtrado pelo ID.
        /// </summary>
        /// <returns>Arraio ordenado por ID</returns>
        public async Task<Finalizacao> GetRegisterByIdAsync(int finalizacaoId, bool incluirSubFinalizacoesVinculadas)
        {
             IQueryable<Finalizacao> qry = _context.Finalizacoes;
            
            //Validação para inclusão das registros vinculados por FK
            if (incluirSubFinalizacoesVinculadas)
            {
                qry = qry.Include(x => x.SubFInalizacoes);                
            }
            
            //Ordenando a qry, usando função Arrow
            //Depois filtrando pelo 'ID' passado
            qry = qry.OrderBy(x => x.Id)
                        .Where(x => x.Id == finalizacaoId); 

            return await qry.FirstOrDefaultAsync();
        }
    }
}