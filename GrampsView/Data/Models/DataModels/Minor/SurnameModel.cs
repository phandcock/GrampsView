//-----------------------------------------------------------------------
//
// Handles GRAMPS URL fields
//
// <copyright file="URLModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS Surname element class.
    ///
    /// XML 1.71 all done
    /// </summary>
    public class SurnameModel : ModelBase, IURLModel
    {
        public SurnameModel()
        {
            HomeImageHLink.HomeSymbol = Common.CommonConstants.IconSurname;
        }

        public string DefaultText
        {
            get
            {
                return GText;
            }
        }

        [DataMember]
        public string GConnector
        {
            get;
            set;
        }

        [DataMember]
        public string GDerivation
        {
            get;
            set;
        }

        [DataMember]
        public string GPrefix
        {
            get;
            set;
        }

        [DataMember]
        public bool GPrim
        {
            get;
            set;
        }

        [DataMember]
        public string GText
        {
            get;
            set;
        }

        public override bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(DefaultText);
            }
        }
    }
}