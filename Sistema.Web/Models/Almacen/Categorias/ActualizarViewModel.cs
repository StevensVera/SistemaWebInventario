using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistemas.Web.Models.Almacen.Categorias
{
    public class ActualizarViewModel
    {
        /*   Utilizamos datAnnotations para validar los datos 
      Se puede utilizar la clase Categorias de las entidades 
      (No son las misma las instalacias y los controladores)
   */
        [Required]
        public int idcategoria { get; set; }
        //Usign Data Using Data Annotations for Model Validation
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener mas de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [StringLength(265)]
        public string descripcion { get; set; }
     

    }
}
