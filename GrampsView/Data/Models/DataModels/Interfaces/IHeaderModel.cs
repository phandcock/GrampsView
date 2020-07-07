//-----------------------------------------------------------------------
//
// Public interfaces for the external storage metadata elements
//
// <copyright file="IHeaderModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the external storage metadata elements.
    /// </summary>
    public interface IHeaderModel
    {
        /// <summary>
        /// Gets the database version.
        /// </summary>
        /// <value>The database version.</value>
        int DatabaseVersion
        {
            get;
        }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        string GCreatedDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the created version.
        /// </summary>
        /// <value>The created version.</value>
        string GCreatedVersion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the absolute path to the start of the media file file structure.
        /// </summary>
        /// <value>The media path.</value>
        string GMediaPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the researcher address.
        /// </summary>
        /// <value>The researcher address.</value>
        string GResearcherAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the researcher city.
        /// </summary>
        /// <value>The researcher city.</value>
        string GResearcherCity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the researcher country.
        /// </summary>
        /// <value>The researcher country.</value>
        string GResearcherCountry
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the researcher email.
        /// </summary>
        /// <value>The researcher email.</value>
        string GResearcherEmail
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the researcher locality.
        /// </summary>
        /// <value>The researcher locality.</value>
        string GResearcherLocality
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the researcher.
        /// </summary>
        /// <value>The name of the researcher.</value>
        string GResearcherName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the researcher phone.
        /// </summary>
        /// <value>The researcher phone.</value>
        string GResearcherPhone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the researcher postal.
        /// </summary>
        /// <value>The researcher postal.</value>
        string GResearcherPostal
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the state of the researcher.
        /// </summary>
        /// <value>The state of the researcher.</value>
        string GResearcherState
        {
            get;
            set;
        }
    }
}