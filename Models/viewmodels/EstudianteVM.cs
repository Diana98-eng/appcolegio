using Microsoft.AspNetCore.Mvc.Rendering;

    namespace appcolegio.Models.viewmodels

    {
        public class EstudianteVM
        {
           public Notum oNota{  get; set; } = new Notum();

            public String?Nombre { get; set; }

            public String?Apellido { get; set; }

            public String?NombreMateria { get; set; }

            public decimal?Calificacion {  get; set; }

        }
    }
