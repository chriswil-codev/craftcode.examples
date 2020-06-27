//-----------------------------------------------------------------------------
// <copyright file="SerializedSettingsTest.cs" company="Codev Software, LLC">
// Copyright © 2020
// </copyright>
//-----------------------------------------------------------------------------
namespace CraftCode.Examples.Test
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    ///--------------------------------------------------------------------
    /// <summary>
    /// This will verify the SerializedSettings functionality.
    /// </summary>
    ///--------------------------------------------------------------------
    [TestClass]
    public class SerializedSettingsTest
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Define the name of the directory which will identify as the
        /// application we are running the test.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String TestDirectory = "CraftCode.Test.6c5c4ea6-340f-4279-a844-a5044575d614";
        #endregion

        #region Setup/Cleanup
        ///--------------------------------------------------------------------
        /// <summary>
        /// Clean up any residual files we may have.
        /// </summary>
        ///--------------------------------------------------------------------
        [TestInitialize]
        public void Initialize()
        {
            this.Clean();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Clean up any residual files we may have.
        /// </summary>
        ///--------------------------------------------------------------------
        [TestCleanup]
        public void Shutdown()
        {
            this.Clean();
        }
        #endregion

        #region Tests
        ///--------------------------------------------------------------------
        /// <summary>
        /// Test the scenario where the directory and settings file does not
        /// exist prior to the first use of the class.  We should expect the
        /// directory and settings file to be created, and that it should 
        /// contain default values.
        /// </summary>
        ///--------------------------------------------------------------------
        [TestMethod]
        public void DoesNotExist()
        {
            SerializedSettings example = new SerializedSettings(TestDirectory);

            Debug.Assert(File.Exists(this.GetFileName()), "Settings File does not exist");

            Debug.Assert(String.IsNullOrWhiteSpace(example.Settings.StringValue));
            Debug.Assert(example.Settings.IntegerValue == 0);
            Debug.Assert(example.Settings.ComplexValue.Items.Count == 0);
            Debug.Assert(example.Settings.DictionaryValue.Count == 0);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Test the scenario where the directory and settings file already
        /// exist.  Once instantiation of the class, the object should contain
        /// our expected settings values.
        /// </summary>
        ///--------------------------------------------------------------------
        [TestMethod]
        public void AlreadyExists()
        {
            // Create a settings file that is populated with data we should
            // expect during this test.
            //
            this.CreateSettingsFile();

            // Create a new instance.  This will load from the file and as
            // such should contain the settings we persisted.
            //
            SerializedSettings example = new SerializedSettings(TestDirectory);

            Debug.Assert(example.Settings.StringValue  == "Test");
            Debug.Assert(example.Settings.IntegerValue == 42);

            Debug.Assert(example.Settings.ComplexValue.Items[0] == "Item 1");
            Debug.Assert(example.Settings.ComplexValue.Items[1] == "Item 2");

            Debug.Assert(example.Settings.DictionaryValue["A"] == 0);
            Debug.Assert(example.Settings.DictionaryValue["B"] == 1);
            Debug.Assert(example.Settings.DictionaryValue["C"] == 2);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Test the scenario where we perform a reload of the settings file
        /// after we have set some data.  We should expect that the object
        /// contains that which we have persisted and not set.
        /// </summary>
        ///--------------------------------------------------------------------
        [TestMethod]
        public void Reload()
        {
            this.CreateSettingsFile();

            SerializedSettings example = new SerializedSettings(TestDirectory);

            example.Settings.IntegerValue = Int32.MaxValue;
            example.Settings.StringValue  = Int32.MaxValue.ToString();

            example.Settings.ComplexValue.Items.Clear();

            // Reload the file.  It should not contain any of the data we set
            // above (since we didn't save).
            //
            example.Reload();

            Debug.Assert(example.Settings.StringValue  == "Test");
            Debug.Assert(example.Settings.IntegerValue == 42);

            Debug.Assert(example.Settings.ComplexValue.Items[0] == "Item 1");
            Debug.Assert(example.Settings.ComplexValue.Items[1] == "Item 2");

            Debug.Assert(example.Settings.DictionaryValue["A"] == 0);
            Debug.Assert(example.Settings.DictionaryValue["B"] == 1);
            Debug.Assert(example.Settings.DictionaryValue["C"] == 2);
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the absolute filename for the settings file.
        /// </summary>
        ///--------------------------------------------------------------------
        private String GetFileName()
        {
            String directory = this.GetApplicationDirectory();

            return Path.Combine(directory, "Settings.json");
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the diretory where the settings file resides.
        /// </summary>
        ///--------------------------------------------------------------------
        private String GetApplicationDirectory()
        {
            String directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            return Path.Combine(directory, TestDirectory);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the existence of the test directory.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Clean()
        {
            String directory = GetApplicationDirectory();

            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a dummy file of expected settings.
        /// </summary>
        ///--------------------------------------------------------------------
        private void CreateSettingsFile()
        {
            SerializedSettings example = new SerializedSettings(TestDirectory);

            example.Settings.StringValue  = "Test";
            example.Settings.IntegerValue = 42;
            example.Settings.ComplexValue.Items.Add("Item 1");
            example.Settings.ComplexValue.Items.Add("Item 2");

            example.Settings.DictionaryValue.Add("A", 0);
            example.Settings.DictionaryValue.Add("B", 1);
            example.Settings.DictionaryValue.Add("C", 2);

            example.Save();
        }
        #endregion
    }
}
