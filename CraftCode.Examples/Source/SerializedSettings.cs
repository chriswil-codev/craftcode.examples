//-----------------------------------------------------------------------------
// <copyright file="SerializedSettings.cs" company="Codev Software, LLC">
// Copyright © 2020
// </copyright>
//-----------------------------------------------------------------------------
namespace CraftCode.Examples
{
    using System;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;

    ///--------------------------------------------------------------------
    /// <summary>
    /// This is the class that will provide serialized settings to a file
    /// for application persistence.
    /// </summary>
    ///--------------------------------------------------------------------
    public class SerializedSettings
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// This represents the name of the file to contain the settings
        /// information.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String SettingFileName = "Settings.json";
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.  Make sure that any reference properties
        /// are properly initialized (avoid null references).  The application 
        /// name is used as the directory name off of a root so that it can
        /// isolate from other applications.
        /// </summary>
        ///--------------------------------------------------------------------
        public SerializedSettings(
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
        /// Get or set the settings for the application.
        /// </summary>
        ///--------------------------------------------------------------------
        public Settings Settings { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Persist the settings to file.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Save()
        {
            String contents = JsonConvert.SerializeObject(this.Settings);

            String fileName = this.GetFileName();

            File.WriteAllText(fileName, contents, Encoding.UTF8);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Relaod the settings from the file.
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
            String fileName = this.GetFileName();

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
        private String GetFileName()
        {
            String directory = this.GetApplicationDirectory();

            return Path.Combine(directory, SettingFileName);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the diretory where the settings file resides.
        /// </summary>
        ///--------------------------------------------------------------------
        private String GetApplicationDirectory()
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
