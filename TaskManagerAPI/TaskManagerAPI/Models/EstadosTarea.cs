using System;
using System.Collections.Generic;

namespace TaskManagerAPI.Models;

public partial class EstadosTarea
{
    public int IdEstado { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
