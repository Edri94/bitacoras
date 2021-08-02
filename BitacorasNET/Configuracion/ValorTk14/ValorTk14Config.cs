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

        public string ObtenerParametro(string key)
        {
            string value = "";
            var config = (ValorTk14Config)ConfigurationManager.GetSection("mqSeries");

            foreach (ValorTk14InstanceElement instance in config.ValorTk14Instances)
            {
                if (instance.Name == key)
                {
                    value = instance.Value;
                }
            }

            return value;
        }
    }
}
