using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp3
{
    internal class ReadnSave
    {
        public static void Read(string path) // чтение файлов, десериализация
        {
            Console.Clear();
            Console.WriteLine("Для изменения текста нажмите F1, для выхода - Escape\r\n----------------------------------------------------");

            int choice = 0;
            List<Diary> result = new List<Diary>();

            if (path.Contains(".txt"))
            {
                choice = 1;

                string[] txt = File.ReadAllLines(path);

                Diary newDiary = new Diary();

                newDiary.author = txt[0];

                for (int j = 0; j < txt.Length / 3; j++)
                {
                    txt[2 + (j * 3)] += "\r\n";
                    newDiary.date[j] = txt[2 + (j * 3)];
                }

                for (int k = 0; k < txt.Length / 3; k++)
                {
                    txt[2 + (k * 3)] += "\r\n";
                    newDiary.content[k] = txt[3 + (k * 3)];
                }

                result.Add(newDiary);

                foreach (Diary diary in result)
                {
                    Console.WriteLine("Автор: " + diary.author + "\r\n");

                    for (int j = 0; j < diary.date.Length; j++)
                    {
                        Console.WriteLine(diary.date[j] + diary.content[j] + "\r\n");
                    }
                }
            }

            else if ((path.Contains(".xml")) || (path.Contains(".json")))
            {
                if (path.Contains(".xml"))
                {
                    choice = 2;

                    XmlSerializer xml = new XmlSerializer(typeof(List<Diary>));

                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        result = (List<Diary>)xml.Deserialize(fs);
                    }
                }

                else if (path.Contains(".json"))
                {
                    choice = 3;

                    string json = File.ReadAllText(path);

                    result = JsonConvert.DeserializeObject<List<Diary>>(json);
                }

                foreach (Diary diary in result)
                {
                    Console.WriteLine("Автор: " + diary.author + "\r\n");

                    for (int j = 0; j < diary.date.Length; j++)
                    {
                        Console.WriteLine(diary.date[j] + diary.content[j]);
                    }
                }
            }

            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Save(path, choice, result);
                }

                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            while (true);
        }

        static void Save(string path, int choice, List<Diary> result) // сохранение файлов, сериализация
        {

        }
    }
}
