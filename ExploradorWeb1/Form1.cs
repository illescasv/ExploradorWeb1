using Microsoft.Web.WebView2.Core;
using System;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ExploradorWeb1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Botonlr_Click(object sender, EventArgs e)
        {
            string direccion = comboBox1.Text.ToString();

            if (!direccion.Contains("http://") && !direccion.Contains("https://"))
            {
                direccion = "https://" + direccion;
            }

            if (!direccion.Contains("."))
            {
                direccion = "https://www.google.com/search?q=" + Uri.EscapeDataString(direccion);
            }

            webView.CoreWebView2.Navigate((direccion));

            // Concatenar la nueva dirección con las existentes en el textBoxEscritura
            textBoxEscritura.AppendText(Environment.NewLine + direccion);

            // Llama a la función Guardar después de navegar a la página
            Guardar("historial.txt", direccion);

            webView.CoreWebView2.Navigate((direccion));
        }

        
        


        private void Guardar(string fileName, string direccion)
        {
            // Abrir el archivo: Write sobreescribe el archivo, Append agrega los datos al final del archivo
            FileStream stream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            // Crear un objeto para escribir el archivo
            StreamWriter writer = new StreamWriter(stream);
            // Usar el objeto para escribir al archivo, WriteLine, escribe linea por linea
            // Write escribe todo en la misma linea. En este ejemplo se hará un dato por cada línea
            writer.WriteLine(direccion);
            // Cerrar el archivo
            writer.Close();
        }

        private void navegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //webView.GoHome();
        }

        private void haciaAtrásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webView.CoreWebView2.GoBack();
        }

        private void haciaAdelanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webView.CoreWebView2.GoForward();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            //webView.GoHome();
           
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //Directorio en donde se va a iniciar la busqueda
            openFileDialog1.InitialDirectory = "c:\\";
            //Tipos de archivos que se van a buscar, en este caso archivos de texto con extensión .txt
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            //Muestra la ventana para abrir el archivo y verifica que si se pueda abrir
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                //Guardamos en una variable el nombre del archivo que abrimos
                string fileName = openFileDialog1.FileName;

                //Abrimos el archivo, en este caso lo abrimos para lectura
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                //Un ciclo para leer el archivo hasta el final del archivo
                //Lo leído se va guardando en un control richTextBox
                while (reader.Peek() > -1)
                //Esta linea envía el texto leído a un control richTextBox, se puede cambiar para que
                //lo muestre en otro control por ejemplo un combobox
                {
                    richTextBox1.AppendText(reader.ReadLine());
                }
                //Cerrar el archivo, esta linea es importante porque sino despues de correr varias veces el programa daría error de que el archivo quedó abierto muchas veces. Entonces es necesario cerrarlo despues de terminar de leerlo.
                reader.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Guardar(@"C:\Users\Illescas\Desktop\historial.txt", textBoxEscritura.Text);
        }

        private void textBoxEscritura_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


