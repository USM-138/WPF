using System;
using System.Windows;
using System.ComponentModel;
using Calculator.enumerador;
using System.Windows.Controls;
using System.Runtime.CompilerServices;

namespace Calculator
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private decimal _lblValor = 0;
        private TipoOperacao _operacao = TipoOperacao.Nenhuma;
        private decimal valorOperacao = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this; //Porpriedade que faz parte do propertyChanged
        }

        // PROPRIEDADE VINCULADA (BINDING)
        public decimal LblValor
        {
            get { return _lblValor; }
            set
            {
                if(value != _lblValor) 
                {
                    _lblValor = value;
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
            decimal numero = Convert.ToDecimal(numberButton.Content.ToString());

            Button signalButton = (Button)sender;
            string signal = signalButton.Content.ToString();

            if (numero == 0 && LblValor == 0)
            {
                LblValor = 0;
            }
            else if (numero == 0 && LblValor != 0)
            {
                LblValor = Convert.ToInt32(LblValor.ToString() + numero.ToString());
            }
            else if (resultLabel.Content.ToString().Contains(".") )
            {
                string valor = resultLabel.Content.ToString();
                string parteInteira = "";
                string parteDecimal = "";

                string[] partes = valor.Split(',');

                if (partes.Length > 0)
                {
                     parteInteira = partes[0]; 
                }

                if (partes.Length > 1)
                {
                     parteDecimal = partes[1];
                }
                LblValor = decimal.Parse(parteInteira + parteDecimal + numero.ToString());
                resultLabel.Content = LblValor.ToString();
            }
            else
            {
                LblValor = Convert.ToInt32(LblValor.ToString() + numero.ToString());
            }
        }

        // BOTÕES DE OPERAÇÃO (+ e =)
        private void OpButton_Click(object sender, RoutedEventArgs e)
        {
            Button opButton = (Button)sender;
            string signal = opButton.Content.ToString();
            valorOperacao += LblValor;
   

            if (signal == "+")
            {
                _operacao = TipoOperacao.Adicao;
            }
            else if (signal == "-") 
            {
                _operacao = TipoOperacao.Subtracao;
            }
            else if(signal == "*") 
            {
                _operacao = TipoOperacao.Multiplicacao;
            }
            else if(signal == "/") 
            {
                _operacao = TipoOperacao.Divisao;
            }
            else if (signal == "%")
            {
                _operacao = TipoOperacao.Porcentagem;
            }
            else if (signal == "AC")
            {
                valorOperacao = 0;
            }
            else if (signal == "+/-")
            {
                LblValor = LblValor * -1;
                return;
            }
            else if (signal == ".") 
            {
                resultLabel.Content = LblValor.ToString() + ".";
                return;
            }
                LblValor = 0;
        }

        private void eqButton_Click(object sender, RoutedEventArgs e)
        {
            switch (_operacao)
            {
                case TipoOperacao.Adicao:
                    LblValor += valorOperacao;
                    valorOperacao = 0;
                    break;

                case TipoOperacao.Subtracao:
                    LblValor = valorOperacao - LblValor;
                    valorOperacao = 0;
                break;

                case TipoOperacao.Divisao:
                    LblValor = valorOperacao / LblValor;
                    valorOperacao = 0;
                break;

                case TipoOperacao.Multiplicacao:
                    LblValor = valorOperacao * LblValor;
                    valorOperacao = 0;
                break;

                case TipoOperacao.Porcentagem:

                    if(LblValor < 0)
                        LblValor = (valorOperacao - LblValor * -1) * (1 + LblValor / 100);
                    else
                        LblValor = valorOperacao * (1 + LblValor / 100);

                    valorOperacao = 0;
                break;
            }
        }
    }
}