using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduManagerV0._9
{
    public partial class Form2 : Form
    {
        public bool IsKeyValid { get; private set; } = false;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userKey = textBox1.Text;
            string validKey = "Ge1234FAffafRTAGR1231i9u";

            if (userKey == validKey)
            {
                MessageBox.Show("Chave correta! Acesso permitido.");
                IsKeyValid = true;
                this.Close(); // Fechar Form2
            }
            else
            {
                MessageBox.Show("Chave incorreta! Acesso negado.");
            }
        }
    }
}