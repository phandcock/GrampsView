// <copyright file="SurnameModelCollection.cs" company="PlaceholderCompany">
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
    [KnownType(typeof(ObservableCollection<SurnameModel>))]
    public class SurnameModelCollection : CardGroupBase<SurnameModel>
    {
        public SurnameModelCollection()
        {
            Title = "Surname Model Collection";
        }

        public string GetPrimarySurname
        {
            get
            {
                // TODO Handle multiple surnames

                if (Items.Count > 0)
                {
                    return (Items[0] as SurnameModel).GText;
                }

                return string.Empty;
            }
        }
    }
}