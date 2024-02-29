using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Policy;

namespace explorador_web
{
    public partial class Form1 : Form
    {
        List<URL> urls = new List<URL>();
        public Form1()
        {
            InitializeComponent();
            
            this.Resize += new System.EventHandler(this.Form_Resize);
        }
        private void Guardar(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            foreach (var url in urls)
            {
                writer.WriteLine(url.Pagina);
                writer.WriteLine(url.Veces);
                writer.WriteLine(url.Fecha);
            }
            writer.Close();
        }
        private void Form_Resize(object sender, EventArgs e)
        {
            webView.Size = this.ClientSize - new System.Drawing.Size(webView.Location);
            button1.Left = this.ClientSize.Width - button1.Width;
            comboBox1.Width = button1.Left - comboBox1.Left;
        }

        private void buttonIr_Click(object sender, EventArgs e)
        {
            string url = comboBox1.Text.ToString();
            if (url.Contains(".") || url.Contains("/") || url.Contains(":"))
            {
                if (url.Contains("https"))
                    webView.CoreWebView2.Navigate(url);
                else
                {
                    url = "https://" + url;
                    webView.CoreWebView2.Navigate(url);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(url))
                {
                    url = "https://www.google.com/search?q=" + url;
                    webView.CoreWebView2.Navigate(url);
                }
            }

            URL urlExiste = urls.Find(u => u.Pagina == url);
            if (urlExiste == null)
            {
                URL urlNueva = new URL();
                urlNueva.Pagina = url;
                urlNueva.Veces = 1;
                urlNueva.Fecha = DateTime.Now;
                urls.Add(urlNueva);
                Guardar("historial.txt");
                webView.CoreWebView2.Navigate(url);
            }
            else
            {
                urlExiste.Veces++;
                urlExiste.Fecha = DateTime.Now;
                Guardar("historial.txt");
                webView.CoreWebView2.Navigate(urlExiste.Pagina);
            }


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
            string fileName = @"C:\Users\Illescas\Desktop\historial.txt";

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)

            {
                URL url = new URL();
                url.Pagina = reader.ReadLine();
                url.Veces = Convert.ToInt32(reader.ReadLine());
                url.Fecha = Convert.ToDateTime(reader.ReadLine());
                urls.Add(url);
            }
            reader.Close();
            comboBox1.DisplayMember = "pagina";
            comboBox1.DataSource = urls;
            comboBox1.Refresh();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
