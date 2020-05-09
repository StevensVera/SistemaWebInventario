using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Web.Models.Almacen.Articulo;
using Sistemas.Entidades.Almacen;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ArticulosController(DbContextSistema context)
        {
            _context = context;
        }

        // Creamos el Metodo listar para la tabla de Categorias mediate ViewModel
        // GET: api/Articulos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloViewModel>> Listar()
        {
            var articulo = await _context.Articulos.Include(a => a.categoria).ToListAsync();

            return articulo.Select(a => new ArticuloViewModel
            {
                idarticulo = a.idarticulo,
                idcategoria = a.idcategoria,
                categoria= a.categoria.nombre,
                codigo = a.codigo,
                nombre = a.nombre,
                stock = a.stock,
                precio_venta = a.precio_venta,
                descripcion = a.descripcion,
                condicion = a.condicion
                
            }


            );
        }

        // Creamos el Metodo los registro de la tabla Articulos
        // GET: api/Articulos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            // Instaciamos para realizar la busqueda por id.
            var articulos = await _context.Articulos.Include(a=> a.categoria ).SingleOrDefaultAsync(a => a.idarticulo==id);

            if (articulos == null)
            {
                return NotFound();
            }

            return Ok(new ArticuloViewModel
            {
                idarticulo = articulos.idarticulo,
                idcategoria = articulos.idcategoria,
                categoria = articulos.categoria.nombre,
                codigo = articulos.codigo,
                nombre = articulos.nombre,
                descripcion = articulos.descripcion,
                stock = articulos.stock,
                precio_venta = articulos.precio_venta,
                condicion = articulos.condicion


            });
        }

        // PUT: api/Articulos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idarticulo <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.idarticulo == model.idarticulo);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.idcategoria = model.idcategoria;
            articulo.codigo = model.codigo;
            articulo.nombre = model.nombre;
            articulo.precio_venta = model.precio_venta;
            articulo.stock = model.stock;
            articulo.descripcion = model.descripcion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // Creamos el Metodo Crear los registro de la tabla Categorias
        // POST: api/Articulos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            // Valida el DataAnnotation, es decir que se cumpla el contenido de la clase CrearViewModel
            // Ejemplo que la el campo nombre tenga maximo 50 caracteres y menos de 3
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Creamos la Instacia
            Articulo articulo = new Articulo
            {
              idcategoria = model.idcategoria,
              codigo = model.codigo,
              nombre = model.nombre,
              precio_venta = model.precio_venta,
              stock = model.stock,
              descripcion = model.descripcion,
              condicion = true

            };

            _context.Articulos.Add(articulo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok();

        }

        // Creamos el Metodo para deactivar los registro de la tabla Categorias
        // PUT: api/Articulos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            //Indicamos el que Id si existe
            if (id <= 0)
            {
                return BadRequest();
            }

            //Consultar que la categoria exista con el id expecifico
            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.idarticulo == id);

            // Creamos una condicion para evaluar no sea null en las busquedas
            if (articulo == null)
            {
                return NotFound();

            }
            // Si se encuentra el objeto se actualiza la lista de esos datos

            articulo.condicion = false;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        // Creamos el Metodo para Activar los registro de la tabla Categorias
        // PUT: api/Articulos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            //Indicamos el que Id si existe
            if (id <= 0)
            {
                return BadRequest();
            }

            //Consultar que la categoria exista con el id expecifico
            var articulos = await _context.Articulos.FirstOrDefaultAsync(a => a.idarticulo == id);
             
            // Creamos una condicion para evaluar no sea null
            if (articulos == null)
            {
                return NotFound();

            }
            // Si se encuentra el objeto se actualiza la lista de esos datos

            articulos.condicion = true;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }


        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.idarticulo == id);
        }
    }
}
