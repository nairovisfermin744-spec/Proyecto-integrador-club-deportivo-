namespace ClubDeportivo.Modelos;

public sealed class Socio
{
    public int Id { get; set; }
    public string Documento { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime FechaAlta { get; set; }
    public DateTime? VencimientoCuota { get; set; }
}
