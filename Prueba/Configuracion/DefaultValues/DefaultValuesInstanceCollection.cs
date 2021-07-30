using System.Configuration;

namespace Prueba.Configuracion.DefaultValues
{
    public class DefaultValuesInstanceCollection : ConfigurationElementCollection
    {
        public DefaultValuesInstanceElement this[int index]
        {
            get
            {
                return (DefaultValuesInstanceElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        public new DefaultValuesInstanceElement this[string key]
        {
            get
            {
                return (DefaultValuesInstanceElement)BaseGet(key);
            }
            set
            {
                if (BaseGet(key) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));

                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DefaultValuesInstanceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DefaultValuesInstanceElement)element).Name;
        }

    }
}
