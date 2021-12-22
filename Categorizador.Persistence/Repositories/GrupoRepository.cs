using System.Linq;
using System.Threading.Tasks;
using Categorizador.Domain;
using Categorizador.Persistence.Contexts;
using Categorizador.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Categorizador.Persistence.Repositories
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly CategorizadorContext _context;
        public GrupoRepository(CategorizadorContext context)
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
        public async Task<Grupo[]> GetAllRegistersAsync(bool incluirFilasVinculadas)
        {
            
            IQueryable<Grupo> qry = _context.Grupos;
            
            //Validação para inclusão das registros vinculados por FK
            if (incluirFilasVinculadas)
            {
                qry = qry.Include(x => x.Filas);                
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
        public async Task<Grupo[]> GetAllRegistersByNameAsync(string name, bool incluirFilasVinculadas)
        {
            IQueryable<Grupo> qry = _context.Grupos;
            
            //Validação para inclusão das registros vinculados por FK
            if (incluirFilasVinculadas)
            {
                qry = qry.Include(x => x.Filas);                
            }
            
            //Ordenando a qry, usando função Arrow
            //Depois filtrando pelo 'name' passado
            qry = qry.OrderBy(x => x.Id)
                        .Where(x => x.DescricaoGrupo.ToLower().Contains(name.ToLower())); //convertido tudo para minúsculo para não dar erro de Case Sensitive

            return await qry.ToArrayAsync();
        }


        /// <summary>
        /// Retorna um único registro.
        /// Incluindo os registros externos, vinculados por FK, caso a Control solicite.
        /// Filtrado pelo ID.
        /// </summary>
        /// <returns>Arraio ordenado por ID</returns>
        public async Task<Grupo> GetRegisterByIdAsync(int grupoId, bool incluirFilasVinculadas)
        {
            IQueryable<Grupo> qry = _context.Grupos;
            
            //Validação para inclusão das registros vinculados por FK
            if (incluirFilasVinculadas)
            {
                qry = qry.Include(x => x.Filas);                
            }
            
            //Ordenando a qry, usando função Arrow
            //Depois filtrando pelo 'ID' passado
            qry = qry.OrderBy(x => x.Id)
                        .Where(x => x.Id == grupoId); 

            return await qry.FirstOrDefaultAsync();
        }
    }
}