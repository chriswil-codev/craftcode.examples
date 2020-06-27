//-----------------------------------------------------------------------------
// <copyright file="ComplexValue.cs" company="Codev Software, LLC">
// Copyright © 2020
// </copyright>
//-----------------------------------------------------------------------------
namespace CraftCode.Examples
{
    using System;
    using System.Collections.Generic;

    ///--------------------------------------------------------------------
    /// <summary>
    /// This is the class for a complex value.
    /// </summary>
    ///--------------------------------------------------------------------
    public class ComplexValue
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public ComplexValue()
        {
            this.Items = new List<String>();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set this list of items.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<String> Items { get; private set; }
        #endregion
    }
}
