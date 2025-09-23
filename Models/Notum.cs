using System;
using System.Collections.Generic;

namespace appcolegio.Models;

public partial class Notum
{
    public int Id { get; set; }

    public decimal? Nota { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdMateria { get; set; }

    public virtual Estudiante? oEstudiante { get; set; }

    public virtual Materium? oMateria { get; set; }
}
