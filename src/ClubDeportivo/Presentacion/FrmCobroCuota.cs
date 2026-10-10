using ClubDeportivo.Infraestructura;

namespace ClubDeportivo.Presentacion;

public sealed class FrmCobroCuota : Form
{
    private readonly TextBox documento = new();
    private readonly Label datosSocio = new() { AutoSize = true, Text = "Busque un socio por documento." };
    private readonly NumericUpDown monto = new() { Minimum = 1, Maximum = 100000000, DecimalPlaces = 2, Value = 1000, Width = 160 };
    private readonly DateTimePicker fechaPago = new() { Value = DateTime.Today, Format = DateTimePickerFormat.Short };
    private int? socioId;

    public FrmCobroCuota()
    {
        Text = "Cobro de cuota";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(480, 260);
        var t = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 2, RowCount = 5 };
        t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
        t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66));
        t.Controls.Add(new Label { Text = "Documento:", AutoSize = true }, 0, 0);
        documento.Dock = DockStyle.Fill;
        t.Controls.Add(documento, 1, 0);
        var buscar = new Button { Text = "Buscar socio", AutoSize = true };
        buscar.Click += Buscar;
        t.Controls.Add(buscar, 1, 1);
        t.Controls.Add(datosSocio, 0, 2);
        t.SetColumnSpan(datosSocio, 2);
        t.Controls.Add(new Label { Text = "Monto:", AutoSize = true }, 0, 3);
        t.Controls.Add(monto, 1, 3);
        t.Controls.Add(new Label { Text = "Fecha de pago:", AutoSize = true }, 0, 4);
        t.Controls.Add(fechaPago, 1, 4);
        var pagar = new Button { Text = "Registrar pago", AutoSize = true };
        pagar.Click += Pagar;
        t.Controls.Add(pagar, 1, 5);
        Controls.Add(t);
        t.RowCount = 6;
    }

    private void Buscar(object? sender, EventArgs e)
    {
        try
        {
            var socio = new RepositorioClub().BuscarSocio(documento.Text);
            socioId = socio?.Id;
            datosSocio.Text = socio is null ? "No se encontró el socio." :
                $"{socio.Nombre} {socio.Apellido} — Vencimiento: {socio.VencimientoCuota?.ToString("dd/MM/yyyy") ?? "sin cuota"}";
        }
        catch (Exception ex) { MessageBox.Show("Error al buscar socio: " + ex.Message); }
    }

    private void Pagar(object? sender, EventArgs e)
    {
        if (socioId is null) { MessageBox.Show("Primero busque un socio válido."); return; }
        try
        {
            new RepositorioClub().RegistrarPago(socioId.Value, monto.Value, fechaPago.Value.Date);
            MessageBox.Show("Pago registrado correctamente.");
            Close();
        }
        catch (Exception ex) { MessageBox.Show("No se pudo registrar el pago: " + ex.Message); }
    }
}
