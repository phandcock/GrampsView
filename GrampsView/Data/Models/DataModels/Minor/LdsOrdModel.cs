//-----------------------------------------------------------------------
//
// Handles GRAMPS Alt fields
//
// <copyright file="LdsOrdModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// GRAMPS Alt element class.
    /// </summary>
    public class LdsOrdModel : ModelBase, ILdsOrdModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [g priv].
        /// </summary>
        /// <value>
        /// <c>true</c> if [g priv]; otherwise, <c>false</c>.
        /// </value>
        public bool GPriv
        {
            get;
            set;
        }

            = false;

        /// <summary>
        /// Gets or sets the g text.
        /// </summary>
        /// <value>
        /// The g text.
        /// </value>
        public string GType
        {
            get;
            set;
        }

            = null;

        /// <summary>
        /// Gets or sets the g value.
        /// </summary>
        /// <value>
        /// The g value.
        /// </value>
        public string GValue
        {
            get;
            set;
        }

            = null;
    }
}