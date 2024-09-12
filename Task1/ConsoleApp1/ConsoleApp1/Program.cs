using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //задаем культуру и получаем наименование месяцев в массив
            CultureInfo ci = new CultureInfo("ru-RU");
            string[] MonthNamesRU = ci.DateTimeFormat.MonthNames;


            //запрашиваем у пользователя строку
            Console.WriteLine("Введите строку из 3-5 слов через запятую:");
            string inputString = Console.ReadLine();



            // удаляем ненужные символы
            string cleanedString = Regex.Replace(inputString, @"[^а-яА-Я0-9, ]", "");
            // делим строку на массив
            string[] words = cleanedString.Split(',');
            //проверяем количество строк, не хватает - завершение
            if (words.Length<3)
            {
                Console.WriteLine("Количество слов недостаточно, программа будет завершена");
                return;
            }

            //запрашиваем у пользователя strLen
            int strLen;
            Console.WriteLine("Введите максимальную длину слова (strLen):");
            string InputstrLen = Console.ReadLine();
            //проверяем что значение корректно, в противном случае - завершение
            if (!int.TryParse(InputstrLen, out strLen))
            {
                Console.WriteLine("Введенное значение не является целым числом, программа будет завершена");
                return;
            }




            //перебираем слова, слова меньше strLen будут добавлены в processedWords
            List<string> processedWords = new List<string>();
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i].Trim();
                if (word.Length <= strLen)
                {
                    //первая буква заглавная, остальные строчные
                    word = char.ToUpper(word[0]) + word.ToLower().Substring(1);

                    //проверка, что слово является наименованием месяца
                    if (MonthNamesRU.Contains(word)) 
                    {
                        //получаем порядковый номер и добавляем к строке
                        string NumberMonth = DateTime.ParseExact(word, "MMMM", ci).Month.ToString();
                        word = word+"("+NumberMonth+")";
                    }
                    //добавляем в список
                    processedWords.Add(word);
                }
            }

            //сортируем
            processedWords.Sort();

            //выводим результат
            Console.WriteLine("Результат:");
            Console.WriteLine(string.Join(", ", processedWords));
        }
    }
}
