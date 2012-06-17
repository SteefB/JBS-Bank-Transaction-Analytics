using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankTransactions.Controllers.Adapters;

namespace BankTransactions.Controllers.Util
{
    public class ConfigurationManager
    {
        public const string FileNameKey = "resourceFileName";
        public const string FileTypeKey = "resourceFileType";

        private const string baseLocation = @"C:/BankTransactions/Resources/";
        private const string settingsLocation = "Settings/";
        private const string settingsName = "config.xml";

        private static HashSet<KeyValuePair<string, object>> settings;

        #region Properties

        public static string BaseLocation
        {
            get
            {
                return baseLocation;
            }
        }

        public static string ResourceSettingsLocation
        {
            get
            {
                return baseLocation + settingsLocation + settingsName;
            }
        }

        public static string FileName
        {
            get
            {
                return GetSettingValue<string>(FileNameKey);
            }
        }

        public static TransactionAdapterType FileType
        {
            get
            {
                return GetSettingValue<TransactionAdapterType>(FileTypeKey);
            }
        }

        #endregion Properties

        public static void Initialize()
        {
            // None of this stuff actually works
            settings = XMLSerializationService.Read<HashSet<KeyValuePair<string, object>>>(ResourceSettingsLocation);

            // For now, just hard code the needed values in here, effectively avoiding the problem and postponing a fix ;)
            AddSettingValue(FileNameKey, "rabo_jorn.csv");
            AddSettingValue(FileTypeKey, TransactionAdapterType.RABOBANK);
        }

        public static string GetSettingValue(string key)
        {
            return GetSettingValue<string>(key);
        }

        public static T GetSettingValue<T>(string key)
        {
            try
            {
                int count = settings.Count(r => key.Equals(r.Key));
                return (T)settings.SingleOrDefault(r => key.Equals(r.Key)).Value;
            }
            catch (Exception ex)
            {
                throw new NoSuchSettingException(ex);
            }
        }

        public static void UpdateSettingValue(string key, object value)
        {
            AddSettingValue(key, value);
        }

        public static void AddSettingValue(string key, object value) {
            settings.Add(new KeyValuePair<string, object>(key, value));

            Save();
        }

        private static void Save()
        {
            XMLSerializationService.Write<HashSet<KeyValuePair<string, object>>>(settings, ResourceSettingsLocation);
        }
    }
}