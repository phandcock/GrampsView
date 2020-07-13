// <copyright file="OCAttributeModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Attribute model collection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.AttributeModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<PlaceNameModel>))]
    public class PlaceNameModelCollection : CardGroupBase<PlaceNameModel>
    {
        public PlaceNameModelCollection()
        {
            Title = "Place Name Collection";
        }

        public string GetDefaultText
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[0].GValue;
                }

                return "Unknown Place Name";
            }
        }
    }
}