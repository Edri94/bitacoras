using System.Configuration;

namespace BitacorasNET.Configuracion.ValorTk14
{
    public class ValorTk14Config : ConfigurationSection
    {
        [ConfigurationProperty("instances")]
        [ConfigurationCollection(typeof(ValorTk14InstanceCollection))]
        public ValorTk14InstanceCollection ValorTk14Instances
        {
            get
            {
                return (ValorTk14InstanceCollection)this["instances"];
            }
        }
    }
}
