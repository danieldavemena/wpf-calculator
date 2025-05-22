using Microsoft.VisualBasic;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Wpf_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private String sign = "";
        private float num1;
        private float num2;

        private float result;

        public MainWindow()
        {
         
           
            InitializeComponent();

            answer.IsReadOnly = true;
            answer.Focusable = false;   
            history.IsReadOnly = true;
            history.Focusable = false;
        }

        [DllImport("user32.dll")]
        static extern int ToUnicode(
        uint virtualKeyCode,
        uint scanCode,
        byte[] keyboardState,
        [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder receivingBuffer,
        int bufferSize,
        uint flags);

        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var virtualKey = KeyInterop.VirtualKeyFromKey(e.Key);
            var keyboardState = new byte[256];
            GetKeyboardState(keyboardState);
            var buffer = new StringBuilder(2);
            ToUnicode((uint)virtualKey, 0, keyboardState, buffer, buffer.Capacity, 0);

            if (buffer.Length > 0)
            {
                char c = buffer[0];
                var match = Regex.IsMatch(c.ToString(), @"[0-9\+-\/*]+");

                if(match || e.Key == Key.Enter || e.Key == Key.Back || e.Key == Key.Escape)
                {

                    if (match)
                    {
                        switch (c.ToString())
                        {
                            case "+":
                                btnPlus_Click(sender, e);
                                break;
                            case "-":
                                btnMinus_Click(sender, e);
                                break;
                            case "*":
                                btnTimes_Click(sender, e);
                                break;
                            case "/":
                                btnDivide_Click(sender, e);
                                break;
                            case ".":
                                if (!answer.Text.Contains("."))
                                {
                                    btnPoint_Click(sender, e);
                                }
                                break;
                            default:
                                answer.Text += c;
                                break;
                        }

                        
                    }

                    if (e.Key == Key.Enter)
                    {
                        btnEquals_Click(sender, e);
                    }


                    if(e.Key == Key.Back)
                    {
                        btnErase_Click(sender, e);
                    }

                    if (e.Key == Key.Escape)
                    {
                        btnClear_Click(sender, e);
                    }
                } else
                {
                    e.Handled = true;
                }
            }
        }


        private void equate(float num1, float num2)
        {

            switch(sign)
            {
                case "+":
                    result = (num1) + (num2);
                    break;
                case "-":
                    result = (num1) - (num2);
                    break;
                case "*":
                    result = (num1) * (num2);
                    break;
                case "/":
                    result = (num1) / (num2);
                    break;
            }

            history.Clear();
            answer.Clear();
            answer.Text = result.ToString();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            answer.Text += 1;
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            answer.Text += 2;

        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            answer.Text += 3;
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            answer.Text += 4;
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            answer.Text += 5;
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            answer.Text += 6;
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            answer.Text += 7;
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            answer.Text += 8;
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            answer.Text += 9;
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            answer.Text += 0;
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            if(answer.Text != "-" && answer.Text != "" && history.Text != "")
            {
                num2 = float.Parse(answer.Text);
                equate(num1, num2);
            }
        }

       
        private void btnErase_Click(object sender, RoutedEventArgs e)
        {

            if (answer.Text != "")
            {
                answer.Text = answer.Text.Substring(0, answer.Text.Length - 1);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            answer.Clear();
            history.Clear();
            if(answer.Text != "" || history.Text != "")
            {
                num1 = float.Parse(history.Text);
                num2 = float.Parse(answer.Text);
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            if(answer.Text != "" && answer.Text != "-")
            {
                sign = "+";
                num1 = float.Parse(answer.Text);
                history.Text = answer.Text + sign;
                answer.Clear();
            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            
            if(answer.Text == "")
            {
                answer.Text = "-";
            } else if(answer.Text != "-" )
            {
                sign = "-";
                num1 = float.Parse(answer.Text);
                history.Text = answer.Text + sign;
                answer.Clear();
            }
        }


        private void btnTimes_Click(object sender, RoutedEventArgs e)
        {
            if (answer.Text != "" && answer.Text != "-")
            {
                sign = "*";
                num1 = float.Parse(answer.Text);
                history.Text = answer.Text + sign;
                answer.Clear();
            }

            
        }

        private void btnDivide_Click(object sender, RoutedEventArgs e)
        {

            if(answer.Text != "" && answer.Text != "-")
            {
                sign = "/";
                num1 = float.Parse(answer.Text);
                history.Text = answer.Text + sign;
                answer.Clear();
            }
            
        }

        private void btnPoint_Click(object sender, RoutedEventArgs e)
        {
            if(answer.Text != "" && answer.Text != "-")
            {
                answer.Text += ".";
            }
        }

        private void btnEmpty_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}