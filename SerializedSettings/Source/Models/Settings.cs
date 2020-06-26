//-----------------------------------------------------------------------------
// <copyright file="Settings.cs" company="Codev Software, LLC">
// Copyright © 2020
// </copyright>
//-----------------------------------------------------------------------------
namespace SerializedSettings.Source
{
    using System;

    ///--------------------------------------------------------------------
    /// <summary>
    /// This is the class that represents the settings to be serialized.
    /// </summary>
    ///--------------------------------------------------------------------
    public class Settings
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the settings object.
        /// </summary>
        ///--------------------------------------------------------------------
        public Settings()
        {
            this.ComplexValue = new ComplexValue();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the the string value.
        /// </summary>
        ///--------------------------------------------------------------------
        public String StringValue { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the the integer value.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 IntegerValue { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the the complex value.
        /// </summary>
        ///--------------------------------------------------------------------
        public ComplexValue ComplexValue { get; set; }
        #endregion
    }
}
