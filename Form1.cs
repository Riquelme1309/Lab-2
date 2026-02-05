using System;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;


namespace archivos_de_texto
{
    public partial class Form1 : Form
    {
        string archivoHistorial = "historial.txt";

        public Form1()
        {
            InitializeComponent();
        }
        private async void InicializarWebView()
        {
            await webView21.EnsureCoreWebView2Async(null);
        }

        // Función para guardar en el archivo
        private void Guardar(string nombreArchivo, string texto)
        {
            FileStream stream = new FileStream(nombreArchivo, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(texto);
            writer.Close();
        }
        
        // Botón navegar
        private void button1_Click_1(object sender, EventArgs e)
        {
            string url = comboBox1.Text;

            // Asegurar que tenga http
            if (!url.StartsWith("http"))
            {
                url = "https://" + url;
            }

            webView21.CoreWebView2.Navigate(url);

            // Guardar en historial
            Guardar(archivoHistorial, url);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Evento Load del formulario
        private async void Form1_Load_1(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async(null);

            if (File.Exists(archivoHistorial))
             {
                FileStream stream = new FileStream(archivoHistorial, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                 comboBox1.Items.Clear();

                    while (reader.Peek() > -1)
                    {
                    string linea = reader.ReadLine();
                    comboBox1.Items.Add(linea);
                    }

        reader.Close();
    }
        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }
    }
}
