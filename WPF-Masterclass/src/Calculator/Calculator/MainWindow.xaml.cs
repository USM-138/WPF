using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Runtime.CompilerServices;

namespace Calculator
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private decimal _valorAtual;
        private string _lblValor = "0";

        private decimal valorOperacao = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        // PROPRIEDADE VINCULADA (BINDING)
        public string LblValor
        {
            get { return _lblValor; }
            set
            {
                if(value != _lblValor)
                    _lblValor = value;

                // Lógica Reversa: Atualiza o decimal sempre que o texto muda
                if (decimal.TryParse(_lblValor, out decimal resultado))
                  {
                      _valorAtual = resultado;
                  }
                  OnPropertyChanged();
            }
        }

        // Implementação do INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // BOTÕES DE NÚMERO
        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            Button numberButton = (Button)sender;
            string numero = numberButton.Content.ToString();

            // Verificamos a PROPRIEDADE, não a Label
            if (LblValor == "0")
            {
                LblValor = numero;
            }
            else
            {
                LblValor += numero;
            }
        }

        // BOTÕES DE OPERAÇÃO (+ e =)
        private void OpButton_Click(object sender, RoutedEventArgs e)
        {
            Button opButton = (Button)sender;
            string signal = opButton.Content.ToString();

            if (signal == "+")
            {
                valorOperacao += _valorAtual;
            }
            else if (signal == "-") 
            {
                valorOperacao += _valorAtual;
            }
            else if (signal == "AC")
            {
                valorOperacao = 0;
            }
            LblValor = "0";
        }

        private void eqButton_Click(object sender, RoutedEventArgs e)
        {
            if (valorOperacao < 0) 
            {
                LblValor = (valorOperacao - -_valorAtual).ToString();
            }
            else if (valorOperacao > 0)
            {
                LblValor = (valorOperacao + +_valorAtual).ToString();
            }
            valorOperacao = 0;
        }
    }
}