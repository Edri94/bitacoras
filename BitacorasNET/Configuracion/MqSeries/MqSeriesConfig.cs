using System.Configuration;

namespace BitacorasNET.Configuracion.MqSeries
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
