//-----------------------------------------------------------------------------
// <copyright file="Example.cs" company="Codev Software, LLC">
// Copyright © 2019
// </copyright>
//-----------------------------------------------------------------------------
namespace SerializedSettings.Source
{
    using System;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;

    ///--------------------------------------------------------------------
    /// <summary>
    /// This is the class that performs persistence settings.
    /// </summary>
    ///--------------------------------------------------------------------
    public class Example
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// This represents the name of the file that will reside in the 
        /// application directory.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String SettingFileName = "Setting.json";
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the settings object.
        /// </summary>
        ///--------------------------------------------------------------------
        public Example(
            String applicationName)
        {
            this.ApplicationName = applicationName;
            this.Settings        = new Settings();

            this.InitializeSettings();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the application name.
        /// </summary>
        ///--------------------------------------------------------------------
        private String ApplicationName { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the settings.
        /// </summary>
        ///--------------------------------------------------------------------
        public Settings Settings { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Save the settings to file.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Save()
        {
            String contents = JsonConvert.SerializeObject(this.Settings);

            String fileName = this.GetFile();

            File.WriteAllText(fileName, contents, Encoding.UTF8);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Relaod the settings file.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Reload()
        {
            this.InitializeSettings();
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the settings object from disk.
        /// </summary>
        ///--------------------------------------------------------------------
        private void InitializeSettings()
        {
            String directory = this.GetDirectory();

            String fileName = this.GetFile();

            if (File.Exists(fileName))
            {
                String contents = File.ReadAllText(fileName);

                this.Settings = JsonConvert.DeserializeObject<Settings>(contents);
            }
            else
            {
                String serializedData = JsonConvert.SerializeObject(this.Settings);

                File.WriteAllText(fileName, serializedData);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the absolute filename for the settings file.
        /// </summary>
        ///--------------------------------------------------------------------
        private String GetFile()
        {
            String directory = this.GetDirectory();

            return Path.Combine(directory, SettingFileName);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the diretory where the settings file resides.
        /// </summary>
        ///--------------------------------------------------------------------
        private String GetDirectory()
        {
            String myDocFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            String applicationPath = Path.Combine(myDocFolder, this.ApplicationName);

            if (Directory.Exists(applicationPath) == false)
            {
                Directory.CreateDirectory(applicationPath);
            }

            return applicationPath;
        }
        #endregion
    }
}
