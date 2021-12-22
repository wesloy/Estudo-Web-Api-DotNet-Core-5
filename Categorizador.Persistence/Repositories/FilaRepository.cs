using System.Linq;
using System.Threading.Tasks;
using Categorizador.Domain;
using Categorizador.Persistence.Contexts;
using Categorizador.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Categorizador.Persistence.Repositories
{
    public class FilaRepository : IFilaRepository
    {
        private readonly CategorizadorContext _context;
        public FilaRepository(CategorizadorContext context)
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
        public async Task<Fila[]> GetAllRegistersAsync(bool incluirFinalizacoesVinculadas)
       {
            
            IQueryable<Fila> qry = _context.Filas;
            
            //Validação para inclusão das registros vinculados por FK
            if (incluirFinalizacoesVinculadas)
            {
                qry = qry
                        .Include(x => x.Grupo)     
                        .Include(x => x.Finalizacoes);                
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
        public async Task<Fila[]> GetAllRegistersByNameAsync(string name, bool incluirFinalizacoesVinculadas)
        {
            IQueryable<Fila> qry = _context.Filas;
            
            //Validação para inclusão das registros vinculados por FK
            if (incluirFinalizacoesVinculadas)
            {
                qry = qry
                        .Include(x => x.Grupo)                
                        .Include(x => x.Finalizacoes);
            }
            
            //Ordenando a qry, usando função Arrow
            //Depois filtrando pelo 'name' passado
            qry = qry.OrderBy(x => x.Id)
                        .Where(x => x.DescricaoFila.ToLower().Contains(name.ToLower())); //convertido tudo para minúsculo para não dar erro de Case Sensitive

            return await qry.ToArrayAsync();
        }


        /// <summary>
        /// Retorna um único registro.
        /// Incluindo os registros externos, vinculados por FK, caso a Control solicite.
        /// Filtrado pelo ID.
        /// </summary>
        /// <returns>Arraio ordenado por ID</returns>
        public async Task<Fila> GetRegisterByIdAsync(int filaId, bool incluirFinalizacoesVinculadas)
        {
            IQueryable<Fila> qry = _context.Filas;
            
            //Validação para inclusão das registros vinculados por FK
            if (incluirFinalizacoesVinculadas)
            {
                qry = qry
                        .Include(x => x.Grupo)                
                        .Include(x => x.Finalizacoes).ThenInclude(x => x.SubFInalizacoes); //como foi incluído Finalizacoes, posso invocar a FK que está linkada nele
            }
            
            //Ordenando a qry, usando função Arrow
            //Depois filtrando pelo 'ID' passado
            qry = qry.OrderBy(x => x.Id)
                        .Where(x => x.Id == filaId); 

            return await qry.FirstOrDefaultAsync();
        }
    }
}