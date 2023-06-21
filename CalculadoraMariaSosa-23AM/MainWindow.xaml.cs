
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculadoraMariaSosa_23AM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();


        }
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            try //si funciona se ejecuta sino manda un error
            {

                Button button = (Button)sender; //INSTANCIAR EL BOTON
                string value = (string)button.Content;
                if (IsNumber(value))
                {
                    HandleNumbers(value);//SE TIENE QUE VALIDAR SI ES UN NUMERO PORQUE HAY OPWERACIONES ARITMETICAS
                }//validar si es un numero
                //MessageBox.Show("Haz dado un click"); //tipo string //BUSCAR UNA FUNCION QUE VALIDE EL STRING
                else if (IsOperator(value))
                {
                    HandleOperators(value);
                }
                else if (value == "CE")
                {
                    Screen.Clear();
                }

                else if (value == "AC")
                {
                    if (Screen.Text.Length == 1)
                        Screen.Text = " ";
                    else
                    {
                        if (Screen.Text.Length > 1)
                            Screen.Text = Screen.Text.Remove(Screen.Text.Length - 1);
                        else
                            Screen.Clear();
                    }
                }
                else if (value == "=")
                {
                    HandleEquals(Screen.Text);
                }



            }

            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        public bool IsNumber(string num)
        {
            return double.TryParse(num, out _);
        }
        private void HandleDecimalSeparator()
        {
            if (!Screen.Text.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                Screen.Text += CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }
        }


        private void HandleNumbers(string value)
        {
            if (string.IsNullOrEmpty(Screen.Text))//si esta vacio o nulo 
            {
                Screen.Text = value;
            }
            else
            {
                Screen.Text += value; //metodos que van a validar que el string es un numero
            }

        }
        private bool IsOperator(string possibleOperator)
        {
            return (possibleOperator == "+" || possibleOperator == "-"
                || possibleOperator == "*" || possibleOperator == "/");

            //if(possibleOperator == "+"|| possibleOperator == "-" || possibleOperator=="*"|| possibleOperator
            //    =="/")
            // {
            //   return true;
            // }
            //  return false;
        }

        private void HandleOperators(string value)
        {
            if (!string.IsNullOrEmpty(Screen.Text) && !ContainsOtherOperators(Screen.Text))
            {
                Screen.Text += value;
            }
        }
        private void HandleEquals(string ScreenContent)
        {
            string op = FindOperator(ScreenContent);
            if (!string.IsNullOrEmpty(op))
            {
                switch (op)
                {
                    case "+":
                        Screen.Text = Sum();
                        break;
                    case "-":
                        Screen.Text = Res();
                        break;
                    case "*":
                        Screen.Text = Mul();
                        break;
                    case "/":
                        Screen.Text = Div();
                        break;
                }
            }
        }
        private string Sum()
        {
            string[] numbers = Screen.Text.Split("+");

            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);

            return Math.Round(n1 + n2, 12).ToString();
        }
        private string Res()
        {
            string[] numbers = Screen.Text.Split("-");
            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);

            return Math.Round(n1 - n2, 12).ToString();

        }
        private string Mul()
        {
            string[] numbers = Screen.Text.Split("*");
            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);
            return Math.Round(n1 * n2, 12).ToString();
        }
        private string Div()
        {
            string[] numbers = Screen.Text.Split("/");
            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);
            return Math.Round(n1 / n2, 12).ToString();
        }
        private string FindOperator(string ScreenContent)
        {
            foreach (char c in ScreenContent)
            {
                if (IsOperator(c.ToString()))
                {
                    return c.ToString();
                }
            }
            return ScreenContent;
        }
        private bool ContainsOtherOperators(string screenContent)
        {
            return screenContent.Contains("+") || screenContent.Contains("-") || screenContent.Contains("*") ||
                screenContent.Contains("/");
        }
    }
}