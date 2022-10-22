using GrampsView.Models.DataModels;

using System;
using System.Collections;
using System.ComponentModel;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// interface definitions.
    /// </summary>
    public interface IFamilyModel : IModelBase, IComparable<FamilyModel>, INotifyPropertyChanged, IComparable, IComparer
    {
        HLinkPersonModel GFather { get; set; }

        HLinkPersonModel GMother { get; set; }

        /// <summary>
        /// Gets the get hlink Family Model that points to this ViewModel.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        HLinkFamilyModel HLink
        {
            get;
        }
    }
}