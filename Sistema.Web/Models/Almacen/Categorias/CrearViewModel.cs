using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistemas.Web.Models.Almacen.Categorias
{
    public class CrearViewModel
    {

        /*Al crear la Categoria no es necesario el ID
          La razon es sencilla, por el es AutoIncrementable desde la Tabla Categorias */

        //Usign Data Using Data Annotations for Model Validation
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener mas de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [StringLength(265)]
        public string descripcion { get; set; }

    }
}
