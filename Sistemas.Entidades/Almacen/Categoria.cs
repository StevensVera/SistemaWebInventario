using System.ComponentModel.DataAnnotations;

namespace Sistemas.Entidades.Almacen
{
    public class Categoria
    {
 
        public int idcategoria { get; set; }
        //Usign Data Using Data Annotations for Model Validation
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener mas de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [StringLength(265)]
        public string descripcion { get; set; }
        public bool condicion { get; set; }

    }
}
