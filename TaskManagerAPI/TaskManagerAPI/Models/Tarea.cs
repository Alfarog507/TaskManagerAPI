using System;
using System.Collections.Generic;

namespace TaskManagerAPI.Models;

public partial class Tarea
{
    public int IdTarea { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public int IdUsuario { get; set; }

    public int IdCategoria { get; set; }

    public int IdEstado { get; set; }

    public virtual ICollection<HistorialTarea> HistorialTareas { get; set; } = new List<HistorialTarea>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual EstadosTarea IdEstadoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
