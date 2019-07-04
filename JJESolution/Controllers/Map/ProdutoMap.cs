using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Controllers.Map
{
    class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            this.ToTable("Produto");
            this.HasKey(q => q.Id);
            this.Property(q => q.Nome).HasColumnName("Nome Produto");
        }
    }
}