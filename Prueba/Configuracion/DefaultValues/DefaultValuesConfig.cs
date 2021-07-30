﻿using System.Configuration;

namespace Prueba.Configuracion.DefaultValues
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
    }
}
