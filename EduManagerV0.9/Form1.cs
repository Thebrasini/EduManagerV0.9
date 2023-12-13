using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using System.Xml.Linq;
using System.Linq;
using System.Drawing;

namespace EduManagerV0._9   
{
    public partial class Form1 : Form
    {

        private string customPath;
        private Dictionary<int, string> botNameMapping = new Dictionary<int, string>();


        // Constantes para a API do Windows
        private const uint WM_SETTEXT = 0x000C;
        private const uint BM_CLICK = 0x00F5;

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);


        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            // Inicialize a variável customPath no construtor
            string userProfile = Environment.GetEnvironmentVariable("USERPROFILE");
            customPath = Path.Combine(userProfile, "Documents", "MemoryError");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string userProfile = Environment.GetEnvironmentVariable("USERPROFILE");
            string customPath = Path.Combine(userProfile, "Documents", "MemoryError");
            string filePath = Path.Combine(customPath, "XT1.txt");
            Console.WriteLine("Caminho do arquivo: " + filePath); // Verifica o caminho do arquivo

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo existe."); // Mensagem de depuração

                    // Lê todas as linhas do arquivo
                    string[] lines = File.ReadAllLines(filePath);
                    Console.WriteLine("Número de linhas lidas: " + lines.Length); // Mensagem de depuração

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT1.txt não foi encontrado no caminho C:/");
                    MessageBox.Show("O arquivo XT1.txt não foi encontrado no caminho C:/");
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }
            {
                // Caminho do arquivo de proxies
                string proxiesFilePath = Path.Combine(customPath, "proxies.txt");

                try
                {
                    // Verifica se o arquivo existe
                    if (File.Exists(proxiesFilePath))
                    {
                        // Lê todas as linhas do arquivo
                        string[] proxyLines = File.ReadAllLines(proxiesFilePath);

                        // Para cada linha no arquivo
                        foreach (string proxyLine in proxyLines)
                        {
                            // Adicione a linha completa ao ComboBox2
                            comboBox2.Items.Add(proxyLine);
                        }

                        // Se houver proxies no ComboBox2, selecione a primeira por padrão
                        if (comboBox2.Items.Count > 0)
                        {
                            comboBox2.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        // Trate o caso em que o arquivo não existe
                        Console.WriteLine("O arquivo proxies.txt não foi encontrado no caminho C:/");
                        MessageBox.Show("O arquivo proxies.txt não foi encontrado no caminho C:/");
                    }
                }
                catch (Exception ex)
                {
                    // Trate exceções, se ocorrerem
                    Console.WriteLine("Erro ao ler o arquivo de proxies: " + ex.Message);
                    MessageBox.Show("Erro ao ler o arquivo de proxies: " + ex.Message);
                }
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifique se um item foi selecionado na combobox
            if (comboBox1.SelectedItem != null)
            {
                // Um item foi selecionado, agora você pode continuar com o código

                // Obtém a linha completa do item selecionado
                string selectedLine = comboBox1.SelectedItem.ToString();

                // Divida a linha completa em email e senha
                string[] parts = selectedLine.Split(':');

                if (parts.Length == 2)
                {
                    // A primeira parte é o email (nome de usuário)
                    string email = parts[0].Trim();

                    // A segunda parte é a senha
                    string password = parts[1].Trim();

                    // Agora você pode usar o 'email' e o 'password' onde precisar no seu código
                    // Por exemplo:
                    // inputSimulator.Keyboard.TextEntry(email + ":" + password);
                }
                else
                {
                    // Trate o caso em que o item selecionado não possui o formato esperado (email:senha)
                    MessageBox.Show("O item selecionado não possui o formato esperado (email:senha).");
                }
            }
            else
            {
                MessageBox.Show("Nenhum item foi selecionado na ComboBox.");
            }
        }





        private void comboBox1_SelectedIndexChanged_Custom(object sender, EventArgs e)

        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // Obtém o item selecionado na combobox
            string selectedItem = comboBox1.SelectedItem.ToString();

            // Divide o item selecionado em nome de usuário e senha
            string[] parts = selectedItem.Split(':');

            if (parts.Length == 2)
            {
                // A primeira parte é o nome de usuário
                string username = parts[0].Trim();

                // A segunda parte é a senha
                string password = parts[1].Trim();

                // Agora você pode usar o 'username' e 'password' onde precisar no seu código
                // Por exemplo:
                // inputSimulator.Keyboard.TextEntry(username);
                // inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                // inputSimulator.Keyboard.TextEntry(password);
            }
            else
            {
                // Trate o caso em que o item selecionado não possui o formato esperado (email:senha)
                MessageBox.Show("O item selecionado não possui o formato esperado (email:senha).");
            }
        }

        private IntPtr FindWindowByPID(int pid)
        {
            Process process = Process.GetProcessById(pid);
            return process.MainWindowHandle;
        }


        // Este método deve estar fora do button1_Click
        private void SimulateActions(IntPtr windowHandle, int gamePID, string username, string password)
        {
            var inputSimulator = new InputSimulator();

            // Coordenadas X e Y para o campo de username
            int usernameX = 473;
            int usernameY = 181;


            // Move o cursor e clica no campo de username
            //   Cursor.Position = new Point(usernameX, usernameY);
            //   inputSimulator.Mouse.LeftButtonClick();

            // Aguarda um momento após o clique

            Thread.Sleep(500);
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
            Thread.Sleep(500);

            // Pressiona 'END' e 'BACKSPACE' várias vezes para limpar o campo
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
            for (int i = 0; i < 100; i++)
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
            }

            // Digita o nome de usuário
            // Substitua "seu_username" pela string real ou pela variável que contém o username
            inputSimulator.Keyboard.TextEntry(username);

            // Aguarda um momento antes de mudar para o campo de senha
            Thread.Sleep(500);

            // Move para o campo de senha (pressiona 'TAB')
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);

            // Limpa o campo de senha
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
            for (int i = 0; i < 100; i++)
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
            }

            // Digita a senha
            // Substitua "sua_senha" pela string real ou pela variável que contém a senha
            inputSimulator.Keyboard.TextEntry(password);
            Thread.Sleep(500);

            // Da enter
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(500);

            // Aguarde um momento após o login antes de iniciar o processo externo
            Thread.Sleep(1000);

            // Execute o processo externo BasicInjector.exe
            string injectorPath = Path.Combine(customPath, "BasicInjector.exe");

            if (File.Exists(injectorPath))
            {
                Process.Start(injectorPath);
            }
            else
            {
                MessageBox.Show("O arquivo BasicInjector.exe não foi encontrado em: " + injectorPath);
            }
        }

        // Declarar uma variável para rastrear o nome do bot
        private string botName = "BOT1";

        // ...

        private void button1_Click(object sender, EventArgs e)
        {
            string gameUrl = "rs-launch://www.runescape.com/k=5/l=$(Language:0)/jav_config.ws";

            try
            {
                // Inicia o jogo usando a URL personalizada
                Process.Start(gameUrl);

                // Aguarda um tempo para o jogo abrir e para o novo processo iniciar
                Thread.Sleep(22000); // Ajuste este valor conforme a necessidade

                Process[] processes = Process.GetProcessesByName("rs2client");
                Process gameProcess = null;

                // Encontra o processo do jogo que iniciou por último
                DateTime latestStartTime = DateTime.MinValue;
                foreach (var proc in processes)
                {
                    if (proc.StartTime > latestStartTime)
                    {
                        latestStartTime = proc.StartTime;
                        gameProcess = proc;
                    }
                }

                if (gameProcess != null)
                {
                    // Se o processo do jogo foi encontrado, aguarde até que esteja pronto
                    gameProcess.WaitForInputIdle();

                    // Agora execute as ações de automação necessárias...
                    IntPtr gameWindowHandle = gameProcess.MainWindowHandle;
                    if (gameWindowHandle != IntPtr.Zero)
                    {
                        // Se o handle foi encontrado, redimensione a janela e simule as ações

                        SetForegroundWindow(gameWindowHandle);
                        MoveWindow(gameWindowHandle, 0, 0, 880, 606, true);
                        Thread.Sleep(2000); // Aguarde a janela ser redimensionada

                        // Verifique se um item foi selecionado na combobox
                        if (comboBox1.SelectedItem != null)
                        {
                            // Um item foi selecionado, agora você pode continuar com o código
                            string selectedItem = comboBox1.SelectedItem.ToString();

                            // Divida o item selecionado em nome de usuário e senha
                            string[] parts = selectedItem.Split(':');

                            if (parts.Length == 2)
                            {
                                // A primeira parte é o nome de usuário
                                string username = parts[0].Trim();

                                // A segunda parte é a senha
                                string password = parts[1].Trim();

                                // Chame o método SimulateActions e passe username e password
                                SimulateActions(gameWindowHandle, gameProcess.Id, username, password);

                                // Edite o arquivo .ppx
                                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                                string customPath = Path.Combine(userProfile, "AppData", "Roaming", "Proxifier4", "Profiles");
                                string ppxFilePath = Path.Combine(customPath, "BOT.ppx"); // Substitua pelo nome real do arquivo .ppx

                                // Carregue o arquivo .ppx usando XDocument
                                XDocument xdoc = XDocument.Load(ppxFilePath);

                                // Encontre o elemento que deseja modificar com base no nome do bot atual
                                var botElement = xdoc.Descendants("Name")
                                                    .FirstOrDefault(element => element.Value == botName);

                                if (botElement != null)
                                {
                                    // Encontre o elemento "Applications" dentro do mesmo elemento "Rule"
                                    var applicationsElement = botElement.Parent.Element("Applications");

                                    if (applicationsElement != null)
                                    {
                                        // Substitua o valor de "pid" pelo PID do bot
                                        applicationsElement.Value = $"pid={gameProcess.Id}";
                                    }
                                    else
                                    {
                                        // Trate o caso em que o elemento "Applications" não foi encontrado
                                        MessageBox.Show("Elemento 'Applications' não encontrado no arquivo .ppx.");
                                    }
                                }
                                else
                                {
                                    // Trate o caso em que o bot com o nome especificado não foi encontrado
                                    MessageBox.Show($"Bot com o nome '{botName}' não encontrado no arquivo .ppx.");
                                }

                                // Salve as alterações de volta no arquivo .ppx
                                xdoc.Save(ppxFilePath);

                                // Atualize o nome do bot para o próximo (por exemplo, BOT2, BOT3, etc.)
                                IncrementBotName();

                                // Recarrega o perfil do Proxifier (.ppx) sem fechar o Proxifier
                                string proxifierPath = @"C:\Program Files (x86)\Proxifier\Proxifier.exe"; // Substitua pelo caminho real do Proxifier.exe

                                // Execute o Proxifier com o comando para recarregar o perfil
                                Process.Start(proxifierPath, "\"" + ppxFilePath + "\" silent-load");
                                Console.WriteLine("Perfil do Proxifier recarregado com sucesso.");
                            }
                            else
                            {
                                // Trate o caso em que o item selecionado não possui o formato esperado (email:senha)
                                MessageBox.Show("O item selecionado não possui o formato esperado (email:senha).");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nenhum item foi selecionado na ComboBox.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível encontrar a janela do jogo.");
                    }
                }
                else
                {
                    MessageBox.Show("Não foi possível encontrar o processo do jogo.");
                }
            }
            catch
            {
            }
        }


        // Método para atualizar o nome do bot
        private void IncrementBotName()
        {
            // Extrair o número do nome atual do bot
            int botNumber = int.Parse(botName.Substring(3));

            // Incrementar o número
            botNumber++;

            // Construir o novo nome do bot
            botName = $"BOT{botNumber}";
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT2.txt"); // Caminho para o arquivo XT2.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT2.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT2.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT2.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT2.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT13.txt"); // Caminho para o arquivo XT13.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT13.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT13.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT13.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT13.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT3.txt"); // Caminho para o arquivo XT3.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT3.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT3.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT3.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT3.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT4.txt"); // Caminho para o arquivo XT4.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT4.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT4.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT4.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT4.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT5.txt"); // Caminho para o arquivo XT5.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT5.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT5.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT5.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT5.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT6.txt"); // Caminho para o arquivo XT6.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT6.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT6.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT6.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT6.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT7.txt"); // Caminho para o arquivo XT7.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT7.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT7.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT7.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT7.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT8.txt"); // Caminho para o arquivo XT8.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT8.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT8.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT8.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT8.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button9_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT9.txt"); // Caminho para o arquivo XT9.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT9.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT9.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT9.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT9.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button10_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT10.txt"); // Caminho para o arquivo XT10.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT10.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT10.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT10.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT10.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button11_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT11.txt"); // Caminho para o arquivo XT11.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT11.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT11.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT11.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT11.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button12_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT12.txt"); // Caminho para o arquivo XT12.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT12.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT12.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT12.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT12.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button14_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT14.txt"); // Caminho para o arquivo XT14.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT14.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT14.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT14.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT14.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button15_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT15.txt"); // Caminho para o arquivo XT15.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT15.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT15.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT15.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT15.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button16_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT16.txt"); // Caminho para o arquivo XT16.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT16.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT16.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT16.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT16.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }

        private void button17_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(customPath, "XT17.txt"); // Caminho para o arquivo XT17.txt

            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo XT17.txt existe."); // Mensagem de depuração

                    // Limpe o ComboBox antes de adicionar novos itens
                    comboBox1.Items.Clear();

                    // Lê todas as linhas do arquivo XT17.txt
                    string[] lines = File.ReadAllLines(filePath);

                    // Para cada linha no arquivo
                    foreach (string line in lines)
                    {
                        // Adicione a linha completa ao ComboBox
                        comboBox1.Items.Add(line);
                    }

                    // Se houver contas na ComboBox, selecione a primeira por padrão
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Trate o caso em que o arquivo não existe
                    Console.WriteLine("O arquivo XT17.txt não foi encontrado em " + customPath);
                    MessageBox.Show("O arquivo XT17.txt não foi encontrado em " + customPath);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções, se ocorrerem
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }


        }
    }
}



