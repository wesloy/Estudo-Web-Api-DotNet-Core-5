using System.Threading.Tasks;
using Categorizador.Persistence.Contexts;
using Categorizador.Persistence.Interfaces;

namespace Categorizador.Persistence.Repositories
{
    /// <summary>
    /// Repositorio genérico, para 3 funções do CRUD.
    /// Criar, Atualizar e Deletar
    /// Por ser genérico, esse repositório precisa receber a classe para trabalhar
    /// Como herda a interface (contrato) ajuda a implantar todos os métodos necessários
    /// Abre conexão com o BD através do contexto
    /// </summary>
    public class GeralRepository : IGeralRepository
    {
        private readonly CategorizadorContext _context;
        public GeralRepository(CategorizadorContext context)
        {
            _context = context;

        }


        /// <summary>
        /// Adicionando um registro no BD.
        /// Como recebe a classe como parâmetro (entity) consegue determinar qual tabela do BD será afetada.
        /// Este método depende do SaveChange para efetivar a ação no BD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        
        /// <summary>
        /// Atualizando um registro no BD.
        /// Como recebe a classe como parâmetro (entity) consegue determinar qual tabela do BD será afetada.
        /// Este método depende do SaveChange para efetivar a ação no BD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }


        /// <summary>
        /// Deletando um registro no BD.
        /// Como recebe a classe como parâmetro (entity) consegue determinar qual tabela do BD será afetada.
        /// Este método depende do SaveChange para efetivar a ação no BD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }


        /// <summary>
        /// Deletando vários registros no BD.
        /// Como recebe a classe como parâmetro (entity) consegue determinar qual tabela do BD será afetada.
        /// Este método depende do SaveChange para efetivar a ação no BD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }



        /// <summary>
        /// Este é o método que efetiva os demais no BD.
        /// É como se os métodos, Update, Remove e Add, fizessem um Migration e este método fosse o Database.Update
        /// </summary>
        /// <returns>Boolean, True para qualquer mudança salva e False se não houver nenhuma mudança</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }

}