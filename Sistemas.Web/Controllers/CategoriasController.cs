using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistemas.Entidades.Almacen;
using Sistemas.Web.Models.Almacen.Categorias;

namespace Sistemas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CategoriasController(DbContextSistema context)
        {
            _context = context;
        }

  // Creamos el Metodo listar para la tabla de Categorias mediate ViewModel
        // GET: api/Categorias/Listar
        [HttpGet("[action]")] 
        public async Task <IEnumerable<CategoriaViewModel>> Listar()
        {
            var categoria = await _context.Categorias.ToListAsync();

            return categoria.Select(c => new CategoriaViewModel
            {
                idcategoria = c.idcategoria,
                nombre = c.nombre,
                descripcion = c.descripcion,
                condicion = c.condicion
            }
            
            
            ); 
        }

    // Creamos el Metodo los registro de la tabla Categorias
        // GET: api/Categorias/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
           // Instaciamos para realizar la busqueda por id.
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(new CategoriaViewModel { 
            
                idcategoria = categoria.idcategoria,
                nombre = categoria.nombre,
                descripcion = categoria.descripcion,
                condicion = categoria.condicion
            });
        }
    
    // Creamos el Metodo los Actualizar los registros de la tabla Categorias
        // PUT: api/Categorias/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {   
            //Validamos el modelo por si no se cumple el DataAnnotation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Indicamos el que Id si existe
            if (model.idcategoria <= 0)
            {
                return BadRequest();
            }

            //Consultar que la categoria exista con el id expecifico
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.idcategoria == model.idcategoria);

            // Creamos una condicion para evaluar no sea null
            if(categoria == null)
            {
                return NotFound();

            }
            // Si se encuentra el objeto se actualiza la lista de esos datos

            categoria.nombre = model.nombre;
            categoria.descripcion = model.descripcion;

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

    // Creamos el Metodo Crear los registro de la tabla Categorias
        // POST: api/Categorias/Crear
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
            Categoria categoria = new Categoria
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true

            };

            _context.Categorias.Add(categoria);

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

     // Creamos el Metodo Eliminar los registro de la tabla Categorias
        // DELETE: api/Categorias/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);           
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok(categoria);
        }

     // Creamos el Metodo para deactivar los registro de la tabla Categorias
        // PUT: api/Categorias/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            //Indicamos el que Id si existe
            if (id <= 0)
            {
                return BadRequest();
            }

            //Consultar que la categoria exista con el id expecifico
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.idcategoria == id);

            // Creamos una condicion para evaluar no sea null
            if (categoria == null)
            {
                return NotFound();

            }
            // Si se encuentra el objeto se actualiza la lista de esos datos

            categoria.condicion = false;
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
        // PUT: api/Categorias/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            //Indicamos el que Id si existe
            if (id <= 0)
            {
                return BadRequest();
            }

            //Consultar que la categoria exista con el id expecifico
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.idcategoria == id);

            // Creamos una condicion para evaluar no sea null
            if (categoria == null)
            {
                return NotFound();

            }
            // Si se encuentra el objeto se actualiza la lista de esos datos

            categoria.condicion = true;
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


        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.idcategoria == id);
        }
    }
}