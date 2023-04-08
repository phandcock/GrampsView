// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;
using GrampsView.Views;

using System.Runtime.Serialization;

namespace GrampsView.Data.Model
{
    /// TODO Update fields as per Schema XML 1.71
    [DataContract]
    public class ParentLinkModel : ModelBase, IParentLinkModel
    {
        private FamilyModel _Parents = new();

        public ParentLinkModel()
        {
        }

        public FamilyModel Parents
        {
            get => _Parents;

            set
            {
                _ = SetProperty(ref _Parents, value);

                // Set HLinkKey to the family model so Valid is true. TODO Why?
                HLinkKey = Parents.HLinkKey;
                ModelItemGlyph = Parents.ModelItemGlyph;
            }
        }

        public override async Task UCNavigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new FamilyDetailPage(Parents.HLink));
            return;
        }
    }
}