// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.DBModels;
using GrampsView.Models.HLinks;
using GrampsView.ModelsDB.HLinks.Interfaces;
using GrampsView.Views;

namespace GrampsView.ModelsDB.HLinks.Models
{
    /// <summary>
    /// HLink to the Family Model.
    /// </summary>

    public class HLinkFamilyDBModel : HLinkDBBase, IHLinkFamilyDBModel
    {
        // NOTE: This cannot default to a FamilyModel as there is a recursive relationship with PersonModel
        private FamilyDBModel _Deref = null;

        private bool DeRefCached = false;

        /// <summary>
        /// Gets.
        /// </summary>
        public FamilyDBModel DeRef
        {
            get
            {
                if (Valid && !DeRefCached)
                {
                    _Deref = DL.FamilyDL.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                if (_Deref is null)
                {
                    _Deref = new FamilyDBModel();
                }

                return _Deref;
            }
        }

        public HLinkFamilyDBModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconFamilies;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundFamily");
        }

        public override Page NavigationPage()
        {
            return new FamilyDetailPage(this);
        }

        //public Group<object> ChildrenLinks
        //{
        //    get
        //    {
        //        Group<object> returnValue = new Group<object>();
        //        foreach (HLinkFamilyModel currentFamily in DeRef.GParentInRefCollection)
        //        {
        //            currentFamily.DisplayAs = CommonEnums.DisplayFormat.SingleCard;

        // // Add Family returnValue.Add(currentFamily);

        // // Add children foreach (HLinkChildRefModel currentChild in
        // currentFamily.DeRef.GChildRefCollection) { currentChild.DisplayAs = CommonEnums.DisplayFormat.SingleCard;

        // returnValue.Add(currentChild); } }

        //        return returnValue;
        //    }
        //}
    }
}