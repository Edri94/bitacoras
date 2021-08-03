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
            var config = (ValorTk14Config)ConfigurationManager.GetSection("valorTk14");
            
            foreach (ValorTk14InstanceElement instance in config.ValorTk14Instances)
            {
                if (instance.Name == key)
                {
                    value = instance.Value;
                }
            }

            return value;
        }

        public string GetParameter(string key)
        {
            return ConfigurationManager.AppSettings[$"valorTk14.{key}"];
        }

        public void SetParameter(string key, string value)
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string[] appPath_arr = appPath.Split('\\');

            appPath = "";
            for (int i = 0; i < (appPath_arr.Length - 2); i++)
            {
                appPath = appPath + "\\" + appPath_arr[i];
            }
            appPath = appPath.Substring(1, appPath.Length - 1);

            string configFile = System.IO.Path.Combine(appPath, "App.config");
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            config.AppSettings.Settings[$"valorTk14.{key}"].Value = value;

            config.Save();
        }
    }
}
