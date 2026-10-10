namespace ClubDeportivo.Presentacion;

public sealed class FrmMenu : Form
{
    public FrmMenu()
    {
        Text = "Club Deportivo - Menú principal";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(460, 300);

        var panel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(25), FlowDirection = FlowDirection.TopDown, WrapContents = false };
        panel.Controls.Add(new Label { Text = "Sistema de Gestión del Club Deportivo", AutoSize = true, Font = new Font(Font.FontFamily, 13, FontStyle.Bold), Margin = new Padding(0, 0, 0, 18) });
        panel.Controls.Add(Boton("Alta de socio", (_, _) => new FrmAltaSocio().ShowDialog(this)));
        panel.Controls.Add(Boton("Cobrar cuota", (_, _) => new FrmCobroCuota().ShowDialog(this)));
        panel.Controls.Add(Boton("Consultar vencimientos", (_, _) => new FrmVencimientos().ShowDialog(this)));
        panel.Controls.Add(Boton("Listado de socios", (_, _) => new FrmListadoSocios().ShowDialog(this)));
        panel.Controls.Add(Boton("Salir", (_, _) => Close()));
        Controls.Add(panel);
    }

    private static Button Boton(string texto, EventHandler accion)
    {
        var b = new Button { Text = texto, Width = 300, Height = 32, Margin = new Padding(0, 3, 0, 3) };
        b.Click += accion;
        return b;
    }
}
