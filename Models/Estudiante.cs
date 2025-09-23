using System;
using System.Collections.Generic;

namespace appcolegio.Models;

public partial class Estudiante
{
    

    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public virtual ICollection<Notum> Nota { get; set; } = new List<Notum>();

    
}
