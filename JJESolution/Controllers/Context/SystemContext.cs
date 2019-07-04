using Controllers.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.Context
{
    class SystemContext : DbContext
    {
        public SystemContext() : base ("Server=DESKTOP-QJUT24M; Database=JJE; Integrated Security=True;")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<SystemContext>(null);
            modelBuilder.Configurations.Add(new ProdutoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}