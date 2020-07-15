// <copyright file="OCAttributeModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Attribute model collection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.AttributeModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<PlaceLocation>))]
    public class PlaceLocationCollection : CardGroupBase<PlaceLocation>
    {
        public PlaceLocationCollection()
        {
            Title = "Place Location Collection";
        }
    }
}