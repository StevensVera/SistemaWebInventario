using Microsoft.EntityFrameworkCore;
using Sistemas.Datos.Mapping.Almacen;
using Sistemas.Datos.Mapping.Usuarios;
using Sistemas.Entidades.Almacen;
using Sistemas.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Sistemas.Entidades.Usuarios.Rol> Roles { get; set; }


        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new ArticulosMap());
            modelBuilder.ApplyConfiguration(new RolMap());
        }

    }
}
