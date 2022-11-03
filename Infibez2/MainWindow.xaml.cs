using System;
using System.Collections.Generic;
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
using System.Numerics;
using System.Text.RegularExpressions;

namespace Infibez2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] EN = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        char[] RU = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        bool IsRU = false; bool IsEN = false;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Decipher_Click(object sender, RoutedEventArgs e)
        {
            bool check = Language_Check(Message.Text);
            //bool check1 = Regex.IsMatch(Key.Text, @"^[a-zA-ZА-Яа-я]+$");
            /* if (!check1)
             {
                 MessageBox.Show("Ключ должен содержать только буквы!");
                 Key.Text = "";
             } */
            // else
            //{
            if (check)
            {
                var dechipher = Cipher.Text;
                dechipher = Dechipher(dechipher);
                Dechipherr.Text = dechipher;
            }
            //
            //}

            

        }

        private string Dechipher(string dechipher)
        {
            int nomer = 0; // Номер в алфавите
            int d = 0; // Смещение
            string s = ""; //Результат
            int j, f; // Переменная для циклов
            int t = 0; // Преременная для нумерации символов ключа.

            char[] message = dechipher.ToCharArray(); // Превращаем сообщение в массив символов.
            char[] key = Key.Text.ToLower().ToCharArray(); // Превращаем ключ в массив символов.
            char[] alphavit = ConvertAlphavit();

            // Перебираем каждый символ сообщения
            for (int i = 0; i < message.Length; i++)
            {
                // Ищем индекс буквы
                for (j = 0; j < alphavit.Length; j++)
                {
                    if (message[i] == alphavit[j])
                    {
                        break;
                    }
                }

                if (j != alphavit.Length) // Если j равно 33, значит символ не из алфавита
                {
                    nomer = j; // Индекс буквы

                    // Ключ закончился - начинаем сначала.
                    if (t > key.Length - 1) { t = 0; }

                    // Ищем индекс буквы ключа
                    for (f = 0; f < alphavit.Length; f++)
                    {
                        if (key[t] == alphavit[f])
                        {
                            break;
                        }
                    }

                    t++;

                    if (f != alphavit.Length) // Если f равно 33, значит символ не из алфавита
                    {
                        d = nomer;
                        d -= f;
                    }
                    else
                    {
                        d = nomer;
                    }

                    // Проверяем, чтобы не вышли за пределы алфавита
                    if (d > alphavit.Length - 1)
                    {
                        d = d - alphavit.Length;
                    }
                    if (d<0)
                    {
                        d = alphavit.Length + d;
                    }

                    message[i] = alphavit[d]; // Меняем букву
                }
            }

            s = new string(message); // Собираем символы обратно в строку.

            return s;
        }

        private void Cipher_Click(object sender, RoutedEventArgs e)
        {
            bool check = Language_Check(Message.Text);
            string key = Key.Text.Replace(" ", "");
            bool check1 = KeyCheck(key);
            if (!check1)
            {
                MessageBox.Show("Ключ должен содержать только буквы, причем выбранного языка!");
                Key.Text = "";
            }
            else
            {
                //почему открывается только один раз??????????????!!
                if (check == false)
                {
                    /* ToolTip cm = this.FindResource("toolTrip") as ToolTip;
                     cm.PlacementTarget = Message;
                     cm.IsOpen = true;
                     cm.StaysOpen = false; */
                    MessageBox.Show("Текст не соответсвтует выбранному языку или заданным параметрам!");
                }
                if (check == true)
                {
                    //var chipher = new List<string>();
                    var chipher = Message.Text.Replace(" ", "");
                    Message.Text = chipher;
                    chipher = Chipher(chipher);
                    //string combinedString = string.Join(" ", chipher);
                    Cipher.Text = chipher;
                }
            }
        }

        private bool KeyCheck(string key)
        {
            if (IsRU) return Regex.IsMatch(key, @"^[А-Яа-я]+$");
            if (IsEN) return Regex.IsMatch(key, @"^[a-zA-Z]+$");
            else return false;
        }

        private string Chipher(string chipher)
        {
            int nomer; // Номер в алфавите
            int d; // Смещение
            string s; //Результат
            int j, f; // Переменная для циклов
            int t = 0; // Преременная для нумерации символов ключа.

            char[] massage = chipher.ToCharArray(); 
            char[] key = Key.Text.ToLower().ToCharArray(); 
            char[] alphavit = ConvertAlphavit();
            // Перебираем каждый символ сообщения
            for (int i = 0; i < massage.Length; i++)
            {
                // Ищем индекс буквы
                for (j = 0; j < alphavit.Length; j++)
                {
                    if (massage[i] == alphavit[j])
                    {
                        break;
                    }
                }

                if (j != alphavit.Length) // Если j равно 33, значит символ не из алфавита
                {
                    nomer = j; 

                    if (t > key.Length - 1) { t = 0; }

                    // Ищем индекс буквы ключа
                    for (f = 0; f < alphavit.Length; f++)
                    {
                        if (key[t] == alphavit[f])
                        {
                            break;
                        }
                    }

                    t++;

                    if (f != alphavit.Length) 
                    {
                        d = nomer + f;
                    }
                    else
                    {
                        d = nomer;
                    }

                    // Проверяем, чтобы не вышли за пределы алфавита
                    if (d > alphavit.Length - 1)
                    {
                        d = d - alphavit.Length;
                    }

                    massage[i] = alphavit[d]; 
                }
            }

            s = new string(massage); // Собираем символы обратно в строку.

            return s;
        }

        private char[] ConvertAlphavit()
        {
            var result = new List<char>();
            if (IsRU)
            {
                for (int i = 0; i < RU.Length; i++)
                    result.Add(RU[i]);
            }
            if (IsEN)
            {
                for (int i = 0; i < EN.Length; i++)
                    result.Add(EN[i]);
            }
            return result.ToArray();
        } 


        /*   private char[] Words_chipher(char[] word, string key)
           {
               char[] chipher = new char[0];

               if (key > 0)
               {
                   chipher = Word_plus_operation(word, key);
               }

               if (key < 0)
               {
                   key = key * (-1);
                   chipher = Word_minus_operation(word, key);
               } 

               return chipher;} */

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Language_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }


        private void RU_Click(object sender, RoutedEventArgs e)
        {
            IsRU = true;
            IsEN = false;
        }

        private void EN_Click(object sender, RoutedEventArgs e)
        {
            IsEN = true;
            IsRU = false;
        }

        private bool Language_Check(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                MessageBox.Show("Empty!!");
                return false;
            }
            else
            {
                str.ToLower();

                byte[] b = System.Text.Encoding.Default.GetBytes(str);


                int angl_count = 0, rus_count = 0;

                foreach (byte bt in b)
                {
                    if ((bt >= 97) && (bt <= 122)) angl_count++;
                    if ((bt >= 192) && (bt <= 239)) rus_count++;
                }



                if (angl_count > rus_count && IsEN == true) return true;
                else if (angl_count < rus_count && IsRU == true) return true;
                else if (angl_count != 0 && rus_count != 0) return false;
                else if (str.Contains("ё")) return true;
                else return false;
                // if (angl_count < rus_count) return Language.Russian;
                //return Language.Unknown;
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "";
            Cipher.Text = "";
            Dechipherr.Text = "";
            Key.Text = "";
        }
    }
}

