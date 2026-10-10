using ClubDeportivo.Modelos;
using MySql.Data.MySqlClient;

namespace ClubDeportivo.Infraestructura;

public sealed class RepositorioClub
{
    public bool ValidarUsuario(string usuario, string clave)
    {
        using var cn = Conexion.Crear();
        cn.Open();
        using var cmd = new MySqlCommand(
            "SELECT COUNT(*) FROM usuarios WHERE usuario=@usuario AND clave=@clave AND activo=1", cn);
        cmd.Parameters.AddWithValue("@usuario", usuario.Trim());
        cmd.Parameters.AddWithValue("@clave", clave);
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    public void AltaSocio(Socio socio)
    {
        using var cn = Conexion.Crear();
        cn.Open();
        using var cmd = new MySqlCommand(@"
INSERT INTO socios(documento,nombre,apellido,telefono,email,fecha_alta,vencimiento_cuota)
VALUES(@documento,@nombre,@apellido,@telefono,@email,@fecha,@vencimiento)", cn);
        cmd.Parameters.AddWithValue("@documento", socio.Documento.Trim());
        cmd.Parameters.AddWithValue("@nombre", socio.Nombre.Trim());
        cmd.Parameters.AddWithValue("@apellido", socio.Apellido.Trim());
        cmd.Parameters.AddWithValue("@telefono", socio.Telefono.Trim());
        cmd.Parameters.AddWithValue("@email", socio.Email.Trim());
        cmd.Parameters.AddWithValue("@fecha", socio.FechaAlta.Date);
        cmd.Parameters.AddWithValue("@vencimiento", socio.VencimientoCuota.HasValue
            ? socio.VencimientoCuota.Value.Date : DBNull.Value);
        cmd.ExecuteNonQuery();
    }

    public Socio? BuscarSocio(string documento)
    {
        using var cn = Conexion.Crear();
        cn.Open();
        using var cmd = new MySqlCommand(@"
SELECT id,documento,nombre,apellido,telefono,email,fecha_alta,vencimiento_cuota
FROM socios WHERE documento=@documento LIMIT 1", cn);
        cmd.Parameters.AddWithValue("@documento", documento.Trim());
        using var rd = cmd.ExecuteReader();
        if (!rd.Read()) return null;
        return LeerSocio(rd);
    }

    public List<Socio> ListarVencimientos(DateTime fecha)
    {
        var lista = new List<Socio>();
        using var cn = Conexion.Crear();
        cn.Open();
        using var cmd = new MySqlCommand(@"
SELECT id,documento,nombre,apellido,telefono,email,fecha_alta,vencimiento_cuota
FROM socios WHERE vencimiento_cuota IS NOT NULL AND DATE(vencimiento_cuota)<=@fecha
ORDER BY vencimiento_cuota,apellido,nombre", cn);
        cmd.Parameters.AddWithValue("@fecha", fecha.Date);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) lista.Add(LeerSocio(rd));
        return lista;
    }

    public void RegistrarPago(int socioId, decimal monto, DateTime fechaPago)
    {
        if (monto <= 0) throw new ArgumentException("El monto debe ser mayor que cero.");
        using var cn = Conexion.Crear();
        cn.Open();
        using var tx = cn.BeginTransaction();
        try
        {
            DateTime inicioPeriodo;
            DateTime vencimientoActual;
            using (var buscar = new MySqlCommand(
                "SELECT vencimiento_cuota FROM socios WHERE id=@id FOR UPDATE", cn, tx))
            {
                buscar.Parameters.AddWithValue("@id", socioId);
                var valor = buscar.ExecuteScalar();
                if (valor is null || valor == DBNull.Value)
                    inicioPeriodo = fechaPago.Date;
                else
                {
                    vencimientoActual = Convert.ToDateTime(valor).Date;
                    // Si la cuota sigue vigente, se extiende desde su vencimiento.
                    // Si ya venció, el nuevo período comienza al día siguiente del vencimiento.
                    inicioPeriodo = vencimientoActual >= fechaPago.Date
                        ? vencimientoActual.AddDays(1)
                        : vencimientoActual.AddDays(1);
                }
            }

            var nuevoVencimiento = inicioPeriodo.AddMonths(1).AddDays(-1);
            using (var pago = new MySqlCommand(
                "INSERT INTO pagos(socio_id,fecha_pago,monto,periodo_desde,periodo_hasta) VALUES(@id,@fecha,@monto,@desde,@hasta)", cn, tx))
            {
                pago.Parameters.AddWithValue("@id", socioId);
                pago.Parameters.AddWithValue("@fecha", fechaPago.Date);
                pago.Parameters.AddWithValue("@monto", monto);
                pago.Parameters.AddWithValue("@desde", inicioPeriodo);
                pago.Parameters.AddWithValue("@hasta", nuevoVencimiento);
                pago.ExecuteNonQuery();
            }
            using (var actualizar = new MySqlCommand(
                "UPDATE socios SET vencimiento_cuota=@vencimiento WHERE id=@id", cn, tx))
            {
                actualizar.Parameters.AddWithValue("@vencimiento", nuevoVencimiento);
                actualizar.Parameters.AddWithValue("@id", socioId);
                actualizar.ExecuteNonQuery();
            }
            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public List<Socio> ListarSocios()
    {
        var lista = new List<Socio>();
        using var cn = Conexion.Crear();
        cn.Open();
        using var cmd = new MySqlCommand(@"
SELECT id,documento,nombre,apellido,telefono,email,fecha_alta,vencimiento_cuota
FROM socios ORDER BY apellido,nombre", cn);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) lista.Add(LeerSocio(rd));
        return lista;
    }

    private static Socio LeerSocio(MySqlDataReader rd) => new()
    {
        Id = rd.GetInt32("id"),
        Documento = rd.GetString("documento"),
        Nombre = rd.GetString("nombre"),
        Apellido = rd.GetString("apellido"),
        Telefono = rd.IsDBNull(rd.GetOrdinal("telefono")) ? "" : rd.GetString("telefono"),
        Email = rd.IsDBNull(rd.GetOrdinal("email")) ? "" : rd.GetString("email"),
        FechaAlta = rd.GetDateTime("fecha_alta"),
        VencimientoCuota = rd.IsDBNull(rd.GetOrdinal("vencimiento_cuota"))
            ? null : rd.GetDateTime("vencimiento_cuota")
    };
}
