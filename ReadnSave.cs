using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp3
{
    internal class ReadnSave
    {
        public static void Read(string path) // чтение файлов, десериализация, вывод на консоль
        {
            Console.Clear();
            Console.WriteLine("Сохранить файл в одном из трёх форматов (txt, json, xml) - F1. Закрыть программу - Escape\r\n-----------------------------------------------------------------------------------------");

            List<Diary> result = new List<Diary>();

            if (File.Exists(path))
            {
                if (path.Contains(".txt"))
                {
                    string[] txt = File.ReadAllLines(path);

                    Diary newDiary = new Diary();

                    string authorMarkerWord = "Автор: ";

                    for (int i = 0; i < txt.Length; i++)
                    {
                        if (txt[i].Contains(authorMarkerWord))
                        {
                            newDiary.author = txt[i].Replace("Автор: ", "");
                        }
                    }

                    for (int j = 0; j < txt.Length / 3; j++)
                    {
                        txt[2 + (j * 3)] += "\r\n";
                        newDiary.date[j] = txt[2 + (j * 3)];
                    }

                    for (int k = 0; k < txt.Length / 3; k++)
                    {
                        txt[3 + (k * 3)] += "\r\n";
                        newDiary.content[k] = txt[3 + (k * 3)];
                    }

                    result.Add(newDiary);

                    foreach (Diary diary in result)
                    {
                        Console.WriteLine("Автор: " + diary.author + "\r\n");

                        for (int j = 0; j < diary.date.Length; j++)
                        {
                            Console.WriteLine(diary.date[j] + diary.content[j]);
                        }
                    }

                }

                else if ((path.Contains(".xml")) || (path.Contains(".json")))
                {
                    if (path.Contains(".xml"))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(List<Diary>));

                        using (FileStream fs = new FileStream(path, FileMode.Open))
                        {
                            result = (List<Diary>)xml.Deserialize(fs);
                        }
                    }

                    else if (path.Contains(".json"))
                    {
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

                    if (key.Key == ConsoleKey.F1)
                    {
                        Save(path, result);
                    }

                    else if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }

                    else if ((key.Key == ConsoleKey.UpArrow) || (key.Key == ConsoleKey.DownArrow) || (key.Key == ConsoleKey.LeftArrow) || (key.Key == ConsoleKey.RightArrow))
                    {
                        CursorMove(key);
                    }

                } while (true);
            }

            else
            {
                Console.WriteLine("Файл не существует! Проверьте правильность введённого пути.");
            }
        }

        static void Save(string path, List<Diary> result) // сохранение файлов, сериализация
        {
            Console.Clear();
            Console.WriteLine("Сохранить файл в одном из трёх форматов (txt, json, xml) - F1. Закрыть программу - Escape\r\n-----------------------------------------------------------------------------------------\r\n");

            path = SavePathEdit(path);
            Console.SetCursorPosition(0, 3);

            if (File.Exists(path))
            {
                Console.WriteLine("Файл с таким названием уже существует. Программа перезапишет его. Нажмите любую клавишу, чтобы продолжить.");
                ConsoleKeyInfo key = Console.ReadKey(true);
            }

            if (path.Contains(".txt"))
            {
                string txt = "";

                foreach (Diary diary in result)
                {
                    txt += "Автор: " + diary.author + "\r\n";

                    for (int i = 0; i < diary.date.Length; i++)
                    {
                        txt += "\r\n" + diary.date[i] + diary.content[i] + "\r\n";
                    }
                }

                File.WriteAllText(path, txt);
            }

            else if (path.Contains(".xml"))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Diary>));
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    xml.Serialize(fs, result);
                }
            }

            else if (path.Contains(".json"))
            {
                string json = JsonConvert.SerializeObject(result);
                File.WriteAllText(path, json);
            }

            Console.WriteLine("Успешно!");
        }

        static void CursorMove(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.UpArrow)
            {
                if (Console.CursorTop == 2)
                {
                    Console.CursorTop = 2;
                }

                else
                {
                    Console.CursorTop--;
                }
            }

            else if (key.Key == ConsoleKey.DownArrow)
            {
                Console.CursorTop++;
            }

            else if (key.Key == ConsoleKey.LeftArrow)
            {
                if (Console.CursorLeft == 0)
                {
                    Console.CursorLeft = 0;
                }

                else
                {
                    Console.CursorLeft--;
                }
            }

            else if (key.Key == ConsoleKey.RightArrow)
            {
                Console.CursorLeft++;
            }
        }

        static string SavePathEdit(string path)
        {
            ConsoleKeyInfo key;

            do
            {
                Console.SetCursorPosition(0, 2);

                for (int i = 0; i < path.Length + 1; i++)
                {
                    Console.Write(" ");
                }

                Console.SetCursorPosition(0, 2);

                Console.Write(path);


                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace)
                {
                    path = path.Remove(path.Length - 1, 1);
                }

                else if (key.Key == ConsoleKey.Enter)
                {

                }

                else
                {
                    path += key.KeyChar;
                }

            } while (key.Key != ConsoleKey.Enter);

            return path;
        }

        static void FileEdit(List<Diary> result)
        {

        }
    }
}
