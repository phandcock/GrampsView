//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="CardGroupCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// </summary>
namespace GrampsView.Common
{
    using GrampsView.Data.Model;

    /// <summary>
    /// </summary>
    public class CardGroupModelBase<T> : CardGroupBase<T>
        where T : ModelBase, new()
    {
        //    public CardGroupBase<T> GetCardGroup()
        //    {
        //        return this;
        //    }
        }
    }