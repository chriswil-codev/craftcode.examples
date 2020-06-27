//-----------------------------------------------------------------------------
// <copyright file="Program.cs" company="Codev Software, LLC">
// Copyright © 2020
// </copyright>
//-----------------------------------------------------------------------------
namespace SerializedSettings
{
    using System;
    using System.Diagnostics;
    using SerializedSettings.Source;

    ///--------------------------------------------------------------------
    /// <summary>
    /// This is the main driver to run the example code.
    /// </summary>
    ///--------------------------------------------------------------------
    class Program
    {
        #region (Entry Point)
        ///----------------------------------------------------------------
        /// <summary>
        /// This is the main entry point for the application.
        /// </summary>
        ///----------------------------------------------------------------
        static void Main(
            String[] args)
        {
            Console.WriteLine("Enter the example to try:");
            Console.WriteLine("  (1) Serialized Settings");

            ConsoleKeyInfo idx = Console.ReadKey();
            Console.WriteLine("");

            switch (idx.KeyChar)
            {
                case '1':
                    SerializedSettingsExample();

                    Console.WriteLine("Success");
                    break;

                default:
                    Console.WriteLine("Usage: Incorrect seection");
                    break;
            }
        }
        #endregion

        #region Methods (Private)
        ///----------------------------------------------------------------
        /// <summary>
        /// This will demonstrate a simple means of doing application 
        /// settings to a file on the users file-system.
        /// </summary>
        ///----------------------------------------------------------------
        private static void SerializedSettingsExample()
        {
            // When instantiating an example settings object, we provide
            // the name of the application which will be used to delineate
            // the location where the file is stored.
            //
            SerializedSettings example = new SerializedSettings("MyTestApp");

            // Do some settings touching.
            //
            example.Settings.StringValue  = "Test";
            example.Settings.IntegerValue = 42;
            example.Settings.ComplexValue.Items.Add("Item 1");
            example.Settings.ComplexValue.Items.Add("Item 2");

            // Save the settings to disk.
            //
            example.Save();

            // This will reload the settings from disk.
            //
            example.Reload();

            // Validate.
            //
            Debug.Assert(example.Settings.StringValue  == "Test");
            Debug.Assert(example.Settings.IntegerValue == 42);
            Debug.Assert(example.Settings.ComplexValue.Items[0] == "Item 1");
            Debug.Assert(example.Settings.ComplexValue.Items[1] == "Item 2");

            // Instantiate the settings from disk.
            //
            example = new SerializedSettings("MyTestApp");

            // Validate.
            //
            Debug.Assert(example.Settings.StringValue == "Test");
            Debug.Assert(example.Settings.IntegerValue == 42);
            Debug.Assert(example.Settings.ComplexValue.Items[0] == "Item 1");
            Debug.Assert(example.Settings.ComplexValue.Items[1] == "Item 2");
        }
        #endregion
    }
}
