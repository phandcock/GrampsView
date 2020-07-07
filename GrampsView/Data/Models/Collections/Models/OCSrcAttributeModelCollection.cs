// <copyright file="OCSrcAttributeModelCollection.cs" company="PlaceholderCompany">
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
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.SrcAttributeModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<SrcAttributeModel>))]
    public class OCSrcAttributeModelCollection : CardGroupBase<SrcAttributeModel>
    {
        public OCSrcAttributeModelCollection()
        {
            Title = "Attribute Collection";
        }
    }
}