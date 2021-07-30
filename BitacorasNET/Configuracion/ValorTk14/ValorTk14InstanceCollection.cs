using System.Configuration;

namespace BitacorasNET.Configuracion.ValorTk14
{
    public class ValorTk14InstanceCollection : ConfigurationElementCollection
    {
        public ValorTk14InstanceElement this[int index]
        {
            get
            {
                return (ValorTk14InstanceElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        public new ValorTk14InstanceElement this[string key]
        {
            get
            {
                return (ValorTk14InstanceElement)BaseGet(key);
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
            return new ValorTk14InstanceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ValorTk14InstanceElement)element).Name;
        }
    }
}
