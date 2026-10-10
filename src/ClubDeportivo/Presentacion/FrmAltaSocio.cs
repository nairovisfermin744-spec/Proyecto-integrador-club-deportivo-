using ClubDeportivo.Infraestructura;
using ClubDeportivo.Modelos;

namespace ClubDeportivo.Presentacion;

public sealed class FrmAltaSocio : Form
{
    private readonly TextBox documento = new();
    private readonly TextBox nombre = new();
    private readonly TextBox apellido = new();
    private readonly TextBox telefono = new();
    private readonly TextBox email = new();

    public FrmAltaSocio()
    {
        Text = "Alta de socio";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(430, 310);
        var tabla = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 2, RowCount = 7 };
        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68));
        Agregar(tabla, "Documento:", documento, 0);
        Agregar(tabla, "Nombre:", nombre, 1);
        Agregar(tabla, "Apellido:", apellido, 2);
        Agregar(tabla, "Teléfono:", telefono, 3);
        Agregar(tabla, "Email:", email, 4);
        var guardar = new Button { Text = "Guardar socio", AutoSize = true };
        guardar.Click += Guardar;
        tabla.Controls.Add(guardar, 1, 5);
        var cancelar = new Button { Text = "Cancelar", AutoSize = true };
        cancelar.Click += (_, _) => Close();
        tabla.Controls.Add(cancelar, 1, 6);
        Controls.Add(tabla);
    }

    private static void Agregar(TableLayoutPanel t, string etiqueta, TextBox campo, int fila)
    {
        campo.Dock = DockStyle.Fill;
        t.Controls.Add(new Label { Text = etiqueta, AutoSize = true, Anchor = AnchorStyles.Left }, 0, fila);
        t.Controls.Add(campo, 1, fila);
    }

    private void Guardar(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(documento.Text) ||
            string.IsNullOrWhiteSpace(nombre.Text) ||
            string.IsNullOrWhiteSpace(apellido.Text))
        {
            MessageBox.Show("Documento, nombre y apellido son obligatorios.");
            return;
        }

        try
        {
            new RepositorioClub().AltaSocio(new Socio
            {
                Documento = documento.Text,
                Nombre = nombre.Text,
                Apellido = apellido.Text,
                Telefono = telefono.Text,
                Email = email.Text,
                FechaAlta = DateTime.Today
            });
            MessageBox.Show("Socio registrado correctamente.");
            Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex) when (ex.Number == 1062)
        {
            MessageBox.Show("Ya existe un socio registrado con ese documento.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("No se pudo registrar el socio: " + ex.Message);
        }
    }
}
