namespace EntidadesDAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PrimaModel : DbContext
    {
        public PrimaModel()
            : base("name=PrimaModel")
        {
        }

        public virtual DbSet<TabAnimais> TabAnimais { get; set; }
        public virtual DbSet<TabPedido> TabPedidoes { get; set; }
        public virtual DbSet<Evidencias> Evidencias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TabAnimais>()
                .Property(e => e.Sexo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TabAnimais>()
                .Property(e => e.Peso)
                .HasPrecision(18, 1);

            modelBuilder.Entity<TabPedido>()
                .Property(e => e.Fornecedor)
                .IsUnicode(false);
        }
    }
}
