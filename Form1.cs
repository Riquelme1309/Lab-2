using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using tarea2_de_navegador;

namespace archivos_de_texto
{
    public partial class Form1 : Form
    {
        private Historial historial;
        private List<Direccion> direcciones;

        public Form1()
        {
            InitializeComponent();
            historial = new Historial("historial.json");
            direcciones = new List<Direccion>();
        }

        private async void Form1_Load_1(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async(null);
            CargarHistorialEnComboBox();
        }

        private void CargarHistorialEnComboBox()
        {
            direcciones = historial.LeerHistorial();

            comboBox1.Items.Clear();

            foreach (var direccion in direcciones)
            {
                comboBox1.Items.Add(direccion.Url);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string url = comboBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Ingrese una dirección válida.");
                return;
            }

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "https://" + url;
            }

            if (webView21 != null && webView21.CoreWebView2 != null)
            {
                webView21.CoreWebView2.Navigate(url);

                historial.AgregarDireccion(url);
                CargarHistorialEnComboBox();
                comboBox1.Text = url;
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            string url = comboBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Seleccione o escriba la URL que desea eliminar.");
                return;
            }

            historial.EliminarDireccion(url);
            CargarHistorialEnComboBox();
        }

        private void buttonOrdenarUrl_Click(object sender, EventArgs e)
        {
            direcciones = historial.OrdenarPorUrl();
            MostrarDireccionesEnComboBox(direcciones);
        }

        private void buttonOrdenarVeces_Click(object sender, EventArgs e)
        {
            direcciones = historial.OrdenarPorVeces();
            MostrarDireccionesEnComboBox(direcciones);
        }

        private void buttonOrdenarFecha_Click(object sender, EventArgs e)
        {
            direcciones = historial.OrdenarPorFecha();
            MostrarDireccionesEnComboBox(direcciones);
        }

        private void MostrarDireccionesEnComboBox(List<Direccion> lista)
        {
            comboBox1.Items.Clear();

            foreach (var direccion in lista)
            {
                comboBox1.Items.Add(direccion.Url);
            }
        }
    }
}
