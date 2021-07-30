using BitacorasNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("hola");
            Bitacora bitacora = new Bitacora();
            bitacora.ProcesarBitacora("QMDCEDTK");
            Console.ReadLine();
        }
    }
}
