using Prueba.Configuracion.EscribeArchivoLOG;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            var escribeLogConfig = (EscribeArchivoLOGConfig)ConfigurationManager.GetSection("escribeArchivoLOG");

            foreach(EscribeArchivoLOGInstanceElement instance in escribeLogConfig.EscribeArchivoLOGInstances)
            {
                Console.WriteLine("{0}  {1}", instance.Name, instance.Value);
            }

            Console.ReadLine();

        }
    }
}
