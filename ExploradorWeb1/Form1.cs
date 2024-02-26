using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ExploradorWeb1
{
    public partial class Form1 : Form
    {
        private List<URL> historial = new List<URL>();

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

            URL existingUrl = historial.FirstOrDefault(u => u.Address == direccion);
            if (existingUrl != null)
            {
                existingUrl.VisitCount++;
                existingUrl.LastAccessed = DateTime.Now;
            }
            else
            {
                historial.Add(new URL(direccion));
            }

            // Ordenar el historial por número de visitas descendente
            historial = historial.OrderByDescending(u => u.VisitCount).ToList();

            MostrarHistorial();

            // Guardar el historial en el archivo de texto
            GuardarHistorial();
        }

        private void GuardarHistorial()
        {
            using (StreamWriter writer = new StreamWriter("historial.txt"))
            {
                foreach (URL url in historial)
                {
                    writer.WriteLine($"{url.Address},{url.VisitCount},{url.LastAccessed}");
                }
            }
        }

        private void MostrarHistorial()
        {
            richTextBox1.Clear();
            foreach (URL url in historial)
            {
                richTextBox1.AppendText($"{url.Address} - Visitas: {url.VisitCount} - Último acceso: {url.LastAccessed}\n");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarHistorial();
            MostrarHistorial();
        }

        private void CargarHistorial()
        {
            string fileName = "historial.txt";
            if (File.Exists(fileName))
            {
                historial.Clear();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 3)
                        {
                            string address = parts[0];
                            int visitCount = int.Parse(parts[1]);
                            DateTime lastAccessed = DateTime.Parse(parts[2]);
                            historial.Add(new URL(address, visitCount, lastAccessed));
                        }
                    }
                }
            }
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
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        //Limpiamos el historial existente antes de cargar el nuevo
                        historial.Clear();

                        //Un ciclo para leer el archivo hasta el final del archivo
                        //Lo leído se va guardando en un control richTextBox
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] parts = line.Split(',');
                            if (parts.Length >= 3)
                            {
                                string address = parts[0];
                                int visitCount = int.Parse(parts[1]);
                                DateTime lastAccessed = DateTime.Parse(parts[2]);
                                historial.Add(new URL(address, visitCount, lastAccessed));
                            }
                        }
                    }
                }
            }

            MostrarHistorial();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GuardarHistorial();
        }
    }

    public class URL
    {
        public string Address { get; set; }
        public int VisitCount { get; set; }
        public DateTime LastAccessed { get; set; }

        public URL(string address)
        {
            Address = address;
            VisitCount = 1;
            LastAccessed = DateTime.Now;
        }

        public URL(string address, int visitCount, DateTime lastAccessed)
        {
            Address = address;
            VisitCount = visitCount;
            LastAccessed = lastAccessed;
        }
    }
}
