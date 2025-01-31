using System;
using System.Collections.Generic;

namespace TaskManagerAPI.Models;

public partial class HistorialTarea
{
    public int IdHistorial { get; set; }

    public int IdTarea { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? DescripcionCambio { get; set; }

    public virtual Tarea IdTareaNavigation { get; set; } = null!;
}
