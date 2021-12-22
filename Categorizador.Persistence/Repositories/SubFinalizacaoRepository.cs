using System.Linq;
using System.Threading.Tasks;
using Categorizador.Domain;
using Categorizador.Persistence.Contexts;
using Categorizador.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Categorizador.Persistence.Repositories
{
    public class SubFinalizacaoRepository : ISubFinalizacaoRepository
    {
        private readonly CategorizadorContext _context;
        public SubFinalizacaoRepository(CategorizadorContext context)
        {
             _context = context;

            //Config para nennhum método "segurar" a transação.
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 

            //Pode ser inserido em cada método, se desejar, exemplo:
            //return await qry.AsNoTracking().ToArrayAsync();
        }

        /// <summary>
        /// Retorna todos os registros.        
        /// </summary>
        /// <returns>Arraio ordenado por ID</returns>
        public async Task<SubFinalizacao[]> GetAllRegistersAsync()
        {
            IQueryable<SubFinalizacao> qry = _context.SubFinalizacoes;
            
            //Ordenando a qry, usando função Arrow
            qry = qry.OrderBy(x => x.Id);

            return await qry.AsNoTracking().ToArrayAsync();
        }


        /// <summary>
        /// Retorna todos os registros.        
        /// Filtrado pela descrição (nome).
        /// </summary>
        /// <returns>Arraio ordenado por ID</returns>
        public async Task<SubFinalizacao[]> GetAllRegistersByNameAsync(string name)
        {
            IQueryable<SubFinalizacao> qry = _context.SubFinalizacoes;
            
            //Ordenando a qry, usando função Arrow
            //Depois filtrando pelo 'name' passado
            qry = qry.OrderBy(x => x.Id)
                        .Where(x => x.DescricaoSubFinalizacao.ToLower().Contains(name.ToLower())); //convertido tudo para minúsculo para não dar erro de Case Sensitive

            return await qry.ToArrayAsync();
        }

        /// <summary>
        /// Retorna um único registro.        
        /// Filtrado pelo ID.
        /// </summary>
        /// <returns>Arraio ordenado por ID</returns>
        public async Task<SubFinalizacao> GetRegisterByIdAsync(int subFinalizacaoId)
        {
             IQueryable<SubFinalizacao> qry = _context.SubFinalizacoes;
            
           
            //Ordenando a qry, usando função Arrow
            //Depois filtrando pelo 'ID' passado
            qry = qry.OrderBy(x => x.Id)
                        .Where(x => x.Id == subFinalizacaoId); 

            return await qry.FirstOrDefaultAsync();
        }
    }
}