using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private decimal _valorAtual;
        public decimal valorOperacao = 0;
        private string _lblValor = "0";

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
                if (_lblValor != value)
                {
                    _lblValor = value;

                    // Lógica Reversa: Atualiza o decimal sempre que o texto muda
                    if (decimal.TryParse(_lblValor, out decimal resultado))
                    {
                        _valorAtual = resultado;
                    }

                    OnPropertyChanged();
                }
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
            if (_lblValor == "0")
            {
                _lblValor = numero;
            }
            else
            {
                _lblValor += numero;
            }
        }

        // BOTÕES DE OPERAÇÃO (+ e =)
        private void OpButton_Click(object sender, RoutedEventArgs e)
        {
            Button opButton = (Button)sender;
            string signal = opButton.Content.ToString();

            if (signal == "+")
            {
                // Acumula o valor atual na memória
                valorOperacao += _valorAtual;

                // Reseta o VISOR para o usuário digitar o próximo número
                // Isso automaticamente zera o valorAtual via lógica reversa
                _lblValor = "0";
            }
        }

        private void eqButton_Click(object sender, RoutedEventArgs e)
        {
            _valorAtual = valorOperacao;
        }
    }
}