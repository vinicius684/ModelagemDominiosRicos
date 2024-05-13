using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.Data;

namespace NerdStore.Catalogo.Data
{
    public class CatalogoContext : DbContext, IUnitOfWork
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options)
            : base(options) { }//parâmetro necessário para configurar o contexto na Program

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //das entidades mapeadas, verificar quais são do tipo string e mapear automaticamente como varchar(100) -> Evitar o varchar(n)
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);//Registrando Mappings
        }

        public async Task<bool> Commit()
        {
            //na hora de fazer algum commit(salvar no banco), vou pegar o traker(mapeador de mudanças) do Ef core, e vou buscar por propriedades que possuam o nome data Cadastro, Se DataCadastro existe e eu estiver adicionando essa entidades, DataCadastro vai possuir o valor DateTime.now 
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {   //Se DataCadastro existe e eu estiver adicionando essa entidades, DataCadastro vai possuir o valor DateTime.now 
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }
                //Caso esteja atualizando a entidade, vou ignorar qualquer dado no campo data cadastro
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;//numero de linhas afetadas for mais que 0, vai retornar true
        }
    }
}
