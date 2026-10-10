using ClubDeportivo.Infraestructura;

namespace ClubDeportivo.Presentacion;

public sealed class FrmLogin : Form
{
    private readonly TextBox txtUsuario = new() { Width = 220 };
    private readonly TextBox txtClave = new() { Width = 220, UseSystemPasswordChar = true };

    public FrmLogin()
    {
        Text = "Club Deportivo - Ingreso";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        ClientSize = new Size(360, 200);

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 2, RowCount = 3 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
        layout.Controls.Add(new Label { Text = "Usuario:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
        layout.Controls.Add(txtUsuario, 1, 0);
        layout.Controls.Add(new Label { Text = "Contraseña:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        layout.Controls.Add(txtClave, 1, 1);
        var btn = new Button { Text = "Ingresar", AutoSize = true };
        btn.Click += Ingresar;
        layout.Controls.Add(btn, 1, 2);
        Controls.Add(layout);
        AcceptButton = btn;
    }

    private void Ingresar(object? sender, EventArgs e)
    {
        try
        {
            if (new RepositorioClub().ValidarUsuario(txtUsuario.Text, txtClave.Text))
            {
                Hide();
                using var menu = new FrmMenu();
                menu.ShowDialog();
                Close();
            }
            else MessageBox.Show("Usuario o contraseña incorrectos.", "Acceso denegado",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show("No se pudo conectar con la base de datos. Revise la configuración.\n" + ex.Message,
                "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
