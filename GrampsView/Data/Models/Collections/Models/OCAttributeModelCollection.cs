﻿// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Runtime.Serialization;

    /// <summary>
    /// Attribute model collection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.AttributeModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(CardGroupBase<AttributeModel>))]
    public class OCAttributeModelCollection : CardGroupBase<AttributeModel>
    {
        public OCAttributeModelCollection()
        {
            Title = "Attribute Collection";
        }
    }
}