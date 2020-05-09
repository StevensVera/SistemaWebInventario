using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistemas.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistemas.Datos.Mapping.Almacen
{
    public class ArticulosMap : IEntityTypeConfiguration<Articulo>
    {
        public void Configure(EntityTypeBuilder<Articulo> builder)
        {
            builder.ToTable("articulo").HasKey(a => a.idarticulo);

        }
    }
}
