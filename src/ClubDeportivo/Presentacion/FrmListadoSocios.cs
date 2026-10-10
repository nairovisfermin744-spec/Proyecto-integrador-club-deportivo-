using ClubDeportivo.Infraestructura;

namespace ClubDeportivo.Presentacion;

public sealed class FrmListadoSocios : Form
{
    private readonly DataGridView grilla = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

    public FrmListadoSocios()
    {
        Text = "Listado de socios";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(800, 420);
        Controls.Add(grilla);
        Load += (_, _) => Cargar();
    }

    private void Cargar()
    {
        try
        {
            grilla.DataSource = new RepositorioClub().ListarSocios()
                .Select(s => new { s.Documento, s.Nombre, s.Apellido, s.Telefono, s.Email, Alta = s.FechaAlta.ToString("dd/MM/yyyy"), Vencimiento = s.VencimientoCuota?.ToString("dd/MM/yyyy") })
                .ToList();
        }
        catch (Exception ex) { MessageBox.Show("No se pudo cargar el listado: " + ex.Message); }
    }
}
