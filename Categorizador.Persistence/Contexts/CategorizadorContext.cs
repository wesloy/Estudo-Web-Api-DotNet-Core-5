
using Categorizador.Domain;
using Microsoft.EntityFrameworkCore;


namespace Categorizador.Persistence.Contexts

{
    public class CategorizadorContext : DbContext
    {
        public CategorizadorContext(DbContextOptions<CategorizadorContext> options) : base(options)
        {
            //Método construtor que invoca a string de conexão, registrada
            //no Service do arquivo Startup.cs (Dentro da Web Api)
        }

        public DbSet<Grupo> Grupos {get; set;} 
        public DbSet<Fila> Filas {get; set;} 
        public DbSet<Finalizacao> Finalizacoes {get; set;} 
        public DbSet<SubFinalizacao> SubFinalizacoes {get; set;} 

        
    }
}