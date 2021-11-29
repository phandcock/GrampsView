﻿// TODO Needs XML 1.71 check

namespace GrampsView.Data.Collections
{
    using GrampsView.Data.Model;

    /// <summary>
    /// Source Attribute model collection
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.SrcAttributeModel}"/>

    public class HLinkOCSrcAttributeCollection : HLinkBaseCollection<HLinkSrcAttributeModel>
    {
        public HLinkOCSrcAttributeCollection()
        {
            Title = "Source Attribute Collection";
        }
    }
}