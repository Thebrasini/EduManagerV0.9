using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduManagerV0._9
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*
            Form2 form2 = new Form2();
            form2.ShowDialog(); // Mostrar Form2 como diálogo

            if (form2.IsKeyValid) // Verificar se a chave é válida
            {
            */
                Form1 form1 = new Form1();
                Application.Run(form1); // Executar Form1 se a chave for válida
            }
        }
    }