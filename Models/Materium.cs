using System;
using System.Collections.Generic;

namespace appcolegio.Models;

public partial class Materium
{
    public int Id { get; set; }

    public string? NomMateria { get; set; }

    public virtual ICollection<Notum> Nota { get; set; } = new List<Notum>();
}
