using System.Configuration;

namespace Prueba.Configuracion.EscribeArchivoLOG
{
    public class EscribeArchivoLOGConfig : ConfigurationSection
    {
        [ConfigurationProperty("instances")]
        [ConfigurationCollection(typeof(EscribeArchivoLOGInstanceCollection))]
        public EscribeArchivoLOGInstanceCollection EscribeArchivoLOGInstances
        {
            get
            {
                // Get the collection and parse it
                return (EscribeArchivoLOGInstanceCollection)this["instances"];
            }
        }
    }
}
