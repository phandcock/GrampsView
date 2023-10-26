// Copyright (c) phandcock. All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels.Interfaces;

namespace GrampsView.Models.DBModels
{
    public class FamilyDBModel : DBModel<FamilyModel, HLinkFamilyModel>, IDBModel<FamilyModel, HLinkFamilyModel>
    {
        public FamilyDBModel()
        {
        }

        public FamilyDBModel(FamilyModel argFamilyModel)
        {
            Serialise(argFamilyModel);
        }
    }
}