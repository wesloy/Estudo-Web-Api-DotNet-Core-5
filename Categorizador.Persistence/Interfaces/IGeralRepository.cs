using System.Threading.Tasks;

namespace Categorizador.Persistence.Interfaces
{

    /// <summary>
    /// Implantação dos métodos de manipulação do BD que são iguais a qualquer tabela/repositorio.
    /// Funcionam  como contratos, pra obrigar a classe que a herda a implantar todos os métodos sem exceção.
    /// Com métodos genéricos que recebe uma classe como parâmetro
    /// </summary>
    
    public interface IGeralRepository
    {
        void Add<T>(T entity) where T:class;  

        void Update<T>(T entity) where T:class;  

        void Delete<T>(T entity) where T:class;  

        void DeleteRange<T>(T[] entityArray) where T:class;  

        Task<bool> SaveChangesAsync();
    }
}