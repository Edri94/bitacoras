using System.Configuration;

namespace Prueba.Configuracion.Headerme
{
    public class HeadermeConfig : ConfigurationSection
    {
        [ConfigurationProperty("instances")]
        [ConfigurationCollection(typeof(HeadermeInstanceCollection))]
        public HeadermeInstanceCollection HeadermeInstances
        {
            get
            {
                return (HeadermeInstanceCollection)this["instances"];
            }
        }
    }
}
