using System.IO;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args) // чтение пути, обращение к Read
        {

            Console.WriteLine("Введите путь до файла (с названием), который вы хотите открыть\r\n--------------------------------------------------------------");

            string path = Console.ReadLine();

            ReadnSave.Read(path);

        }
    }
}