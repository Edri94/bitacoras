using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitacorasNET;
using Prueba.Configuracion.EscribeArchivoLOG;

namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            //Bitacora bitacora = new Bitacora();

            //bitacora.ProcesarBitacora();

            var escribeLogConfig = (EscribeArchivoLOGConfig)ConfigurationManager.GetSection("escribeArchivoLOG");

            foreach (EscribeArchivoLOGInstanceElement instance in escribeLogConfig.EscribeArchivoLOGInstances)
            {
                Console.WriteLine(instance.Name);
                Console.WriteLine(instance.Value);
                Console.WriteLine("--------------------------");
            }

        }
    }
}
