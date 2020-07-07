//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IAltMarkModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    public interface IAltModel : IModelBase
    {
        bool GAlt { get; set; }

        /// <summary>
        /// turn the string 0 or 1 into true or false.
        /// </summary>
        /// <param name="altString">
        /// returns the string version of the Alt value.
        /// </param>
        void SetAlt(string altString);
    }
}