using System.Configuration;

namespace BitacorasNET.Configuracion.Headerih
{
    public class HeaderihConfig : ConfigurationSection
    {
        [ConfigurationProperty("instances")]
        [ConfigurationCollection(typeof(HeaderihInstanceCollection))]
        public HeaderihInstanceCollection HeaderihInstances
        {
            get
            {
                return (HeaderihInstanceCollection)this["instances"];
            }
        }
    }
}
