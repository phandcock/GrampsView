﻿// XML 171 - All fields defined

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Models.DataModels;
    using GrampsView.Models.HLinks;
    using GrampsView.Views;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to the Family Model.
    /// </summary>

    public class HLinkFamilyModel : HLinkBase, IHLinkFamilyModel
    {
        // NOTE: This cannot default to a FamilyModel as there is a recursive relationship with PersonModel
        private FamilyModel _Deref = null;

        private bool DeRefCached = false;

        public HLinkFamilyModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconFamilies;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundFamily");
        }

        //public Group<object> ChildrenLinks
        //{
        //    get
        //    {
        //        Group<object> returnValue = new Group<object>();
        //        foreach (HLinkFamilyModel currentFamily in DeRef.GParentInRefCollection)
        //        {
        //            currentFamily.DisplayAs = CommonEnums.DisplayFormat.SingleCard;

        //            // Add Family
        //            returnValue.Add(currentFamily);

        //            // Add children
        //            foreach (HLinkChildRefModel currentChild in currentFamily.DeRef.GChildRefCollection)
        //            {
        //                currentChild.DisplayAs = CommonEnums.DisplayFormat.SingleCard;

        //                returnValue.Add(currentChild);
        //            }
        //        }

        //        return returnValue;
        //    }
        //}

        /// <summary>
        /// Gets.
        /// </summary>
        [JsonIgnore]
        public FamilyModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.FamilyDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                if (_Deref is null)
                {
                    _Deref = new FamilyModel();
                }

                return _Deref;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(FamilyDetailPage));
            return;
        }
    }
}