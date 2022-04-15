using Microsoft.EntityFrameworkCore;
using TesteViceri_Herois.Model;

namespace TesteViceri_Herois.Data
{
    public class ApplicationContext : DbContext
    {
        #region Propriedades Públicas
        //Váriavel com o valor da Model carregando seus atributos
        public DbSet<HeroisModel> Herois { get; set; }
        public DbSet<HeroisSuperPoderesModel> HeroisSuperpoderes { get; set; }
        public DbSet<SuperpoderesModel> Superpoderes { get; set; }
        #endregion

        #region Construtores
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        #endregion

        #region Métodos/Operadores Protegidos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroisModel>().HasKey(p => p.Id);
            modelBuilder.Entity<HeroisModel>().Property(p => p.Nome).HasMaxLength(120);
            modelBuilder.Entity<HeroisModel>().Property(p => p.NomeHeroi).HasMaxLength(120);

            modelBuilder.Entity<HeroisSuperPoderesModel>().HasKey(p => p.HeroiId);
            modelBuilder.Entity<HeroisSuperPoderesModel>().HasKey(p => p.SuperpoderId);

            modelBuilder.Entity<SuperpoderesModel>().Property(p => p.Superpoder).HasMaxLength(50);
            modelBuilder.Entity<SuperpoderesModel>().Property(p => p.Descricao).HasMaxLength(250);
        }
        #endregion
    }
}
