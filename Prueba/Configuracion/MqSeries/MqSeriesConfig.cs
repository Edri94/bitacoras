using System.Configuration;

namespace Prueba.Configuracion.MqSeries
{
    public class MqSeriesConfig : ConfigurationSection
    {
        [ConfigurationProperty("instances")]
        [ConfigurationCollection(typeof(MqSeriesInstanceCollection))]
        public MqSeriesInstanceCollection MqSeriesInstances
        {
            get
            {
                return (MqSeriesInstanceCollection)this["instances"];
            }
        }
    }
}
