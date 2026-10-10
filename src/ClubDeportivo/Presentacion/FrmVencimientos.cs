using ClubDeportivo.Infraestructura;

namespace ClubDeportivo.Presentacion;

public sealed class FrmVencimientos : Form
{
    private readonly DateTimePicker fecha = new() { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
    private readonly DataGridView grilla = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

    public FrmVencimientos()
    {
        Text = "Cuotas vencidas";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(800, 420);
        var arriba = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 45, Padding = new Padding(8) };
        arriba.Controls.Add(new Label { Text = "Vencidas hasta:", AutoSize = true, Padding = new Padding(0, 6, 0, 0) });
        arriba.Controls.Add(fecha);
        var consultar = new Button { Text = "Consultar", AutoSize = true };
        consultar.Click += (_, _) => Consultar();
        arriba.Controls.Add(consultar);
        Controls.Add(grilla);
        Controls.Add(arriba);
        Load += (_, _) => Consultar();
    }

    private void Consultar()
    {
        try
        {
            grilla.DataSource = new RepositorioClub().ListarVencimientos(fecha.Value.Date)
                .Select(s => new { s.Documento, s.Nombre, s.Apellido, s.Telefono, s.Email, Vencimiento = s.VencimientoCuota?.ToString("dd/MM/yyyy") })
                .ToList();
        }
        catch (Exception ex) { MessageBox.Show("No se pudieron consultar los vencimientos: " + ex.Message); }
    }
}
