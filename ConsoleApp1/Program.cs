using BitacorasNET;
using BitacorasNET.Configuracion.ValorTk14;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ValorTk14Config valorTk14Config = new ValorTk14Config();
            string value = valorTk14Config.GetParameter("TKCONSECUTIVO");
            Console.WriteLine(value);
            Console.ReadKey();

        }
    }
}
