using System.Configuration;

namespace BitacorasNET.Configuracion.DefaultValues
{
    public class DefaultValuesConfig : ConfigurationSection
    {
        [ConfigurationProperty("instances")]
        [ConfigurationCollection(typeof(DefaultValuesInstanceCollection))]
        public DefaultValuesInstanceCollection DefaultValuesInstances
        {
            get
            {
                // Get the collection and parse it
                return (DefaultValuesInstanceCollection)this["instances"];
            }
        }

        public string ObtenerParametro(string key)
        {
            string value = "";
            var config = (DefaultValuesConfig)ConfigurationManager.GetSection("defaultValues");

            foreach (DefaultValuesInstanceElement instance in config.DefaultValuesInstances)
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
