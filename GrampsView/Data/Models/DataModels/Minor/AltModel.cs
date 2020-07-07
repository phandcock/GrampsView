//-----------------------------------------------------------------------
//
// Handles GRAMPS Alt fields
//
// <copyright file="AltModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// GRAMPS Alt element class.
    /// </summary>
    public class AltModel : ModelBase, IAltModel
    {
        /// <summary>
        /// Alt field.
        /// </summary>
        private bool localAlt = false;

        /// <summary>
        /// Gets or sets a value indicating whether Alt is set.
        /// </summary>
        public bool GAlt
        {
            get
            {
                return localAlt;
            }

            set
            {
                SetProperty(ref localAlt, value);
            }
        }

        /// <summary>
        /// turn the string 0 or 1 into true or false.
        /// </summary>
        /// <param name="altString">returns the string version of the Alt value.</param>
        public void SetAlt(string altString)
        {
            switch (altString)
            {
                case "0":
                    {
                        GAlt = false;
                        break;
                    }

                case "1":
                    {
                        GAlt = true;
                        break;
                    }

                default:
                    {
                        GAlt = false;
                        break;
                    }
            }
        }
    }
}