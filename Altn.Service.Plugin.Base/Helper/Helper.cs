using System;
using System.IO;
using System.Xml.Serialization;

namespace Altn.Service.Plugin.Base
{
    public static class Helper<T>
    {

        public static T LoadSettings(string settingsfile)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var fileStream = new FileStream("Plugins\\"+ settingsfile + "", FileMode.Open))
            {
                var settings = (T)serializer.Deserialize(fileStream);

                if (settings == null)
                    throw new Exception("Failed to load settings file: "+ settingsfile + "!");

                return settings;
            }
        }
    }
}
