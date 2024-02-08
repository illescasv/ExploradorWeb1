using Microsoft.Web.WebView2.Core;
using System;
using System.Windows.Forms;

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
    }
}


