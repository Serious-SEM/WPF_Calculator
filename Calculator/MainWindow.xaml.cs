using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
        }

        //закрывает программу
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //Добавляет текст в кнопке в  конец textBox
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            TextBlock block = ((TextBlock)((Button)sender).Content);
            char b = block.Text[0];

            if (textBox1.Text.Length >  0)
            {                
                char t = textBox1.Text[textBox1.Text.Length - 1];

                // Если это специальный символ и в textBox тоже
                //специальный символ то  он  перезаписывает его  
                if (checkSpecial(t) && checkSpecial(b))
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                    textBox1.Text += block.Text;
                }
                else textBox1.Text += block.Text;
            }
            else
            {
                if (!checkSpecial(b)) textBox1.Text += block.Text;
            }            
        }

        //Проверяет специальный это символ или нет
        private bool checkSpecial(char s)
        {
            bool f = true;

            switch (s)
            {
                case '+': break;
                case '*': break;
                case '/': break;
                case '-': break;
                case ',': break;
                default:
                    f = false;
                    break;
            }
            
            return f;
        }

        //Удаляет последний символ в textBox
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
        }

        //Очищает textBox
        private void CE_Button_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
        }        

        //Результат
        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = "";

                List<Double> Number = new List<Double>();
                List<string> Znak = new List<string>();

                MatchCollection collection = Regex.Matches(textBox1.Text, @"\w*\,?\w*");
                
                MatchCollection collection1 = Regex.Matches(textBox1.Text, @"[+\-*/]");

                //Нахождение всех чисел
                foreach (var item in collection)
                {
                    try
                    {
                        Number.Add(Convert.ToDouble(item.ToString()));
                        //s += item.ToString() + "\r\n";
                    }
                    catch
                    {

                    }
                }

                //нахождение все действий
                foreach (var item in collection1)
                {
                    Znak.Add(item.ToString());
                    s += item.ToString() + "\r\n";
                }

                //Умножение и Деление
                for (int i = 0; i < Znak.Count; i++)
                {
                    Double a, b;
                    if (Znak[i] == "*")
                    {
                        a = Number[i];
                        b = Number[i + 1];

                        Number.RemoveAt(i);
                        Number.RemoveAt(i);

                        Number.Insert(i, a * b);

                        Znak.RemoveAt(i);

                        i--;
                        continue;
                    }

                    if (Znak[i] == "/")
                    {
                        a = Number[i];
                        b = Number[i + 1];

                        Number.RemoveAt(i);
                        Number.RemoveAt(i);

                        Number.Insert(i, a / b);

                        Znak.RemoveAt(i);

                        i--;
                    }
                }

                //Сложение и Вычитание
                for (int i = 0; i < Znak.Count; i++)
                {
                    Double a, b;
                    if (Znak[i] == "+")
                    {
                        a = Number[i];
                        b = Number[i + 1];

                        Number.RemoveAt(i);
                        Number.RemoveAt(i);

                        Number.Insert(i, a + b);

                        Znak.RemoveAt(i);

                        i--;
                        continue;
                    }

                    if (Znak[i] == "-")
                    {
                        a = Number[i];
                        b = Number[i + 1];

                        Number.RemoveAt(i);
                        Number.RemoveAt(i);

                        Number.Insert(i, a - b);

                        Znak.RemoveAt(i);

                        i--;
                    }
                }

                textBox1.Text = Number[0].ToString();

                //MessageBox.Show(s);
                //MessageBox.Show(Number[0].ToString());
            }
            catch (Exception err)
            {

               
            }            
        }
    }
}
