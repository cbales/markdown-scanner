﻿namespace OneDrive.ApiDocumentation.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class SavedSettings
    {
        private const string AppName = "ApiTestTool";
        private const string DataFilename = "settings.json";

        [JsonProperty("path")]
        public string DocumentationPath { get; set; }
        [JsonProperty("access-token")]
        public string AccessToken { get; set; }
        [JsonProperty("url")]
        public string ServiceUrl { get; set; }

        private static SavedSettings DefaultInstance;
        public static SavedSettings Default
        {
            get
            {
                if (null == DefaultInstance)
                {
                    DefaultInstance = new SavedSettings();
                }
                return DefaultInstance;
            }
        }

        public SavedSettings()
        {
            Load();
        }

        public void Load()
        {
            var dataFile = PathToAppDataFile;
            if (!File.Exists(dataFile))
                return;

            try
            {
                using (var reader = File.OpenText(dataFile))
                {
                    var contents = reader.ReadToEnd();
                    JsonConvert.PopulateObject(contents, this);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to read saved settings file: {0}", ex.Message));
            }
        }

        public void Save()
        {
            var dataFile = PathToAppDataFile;
            try
            {
                var contents = JsonConvert.SerializeObject(this);
                using (var writer = new StreamWriter(dataFile, false))
                {
                    writer.Write(contents);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to write saved settings file: {0}", ex.Message));
            }
        }

        protected string PathToAppDataFile
        {
            get
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var appFolderPath = Path.Combine(appDataPath, AppName);
                Directory.CreateDirectory(appFolderPath);

                return Path.Combine(appFolderPath, DataFilename);
            }
        }

    }
}