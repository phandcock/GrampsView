// <copyright file="CardListLine.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using GrampsView.Common;

/// <summary>
/// Common routines
/// </summary>
namespace GrampsView.Data.Model
{
    /// <summary>
    /// </summary>

    public class MediaImageFullModel : CommonBindableBase
    {
        private HLinkHomeImageModel _ImageLink = new HLinkHomeImageModel();

        public MediaImageFullModel()
        {
        }

        public HLinkHomeImageModel BaseModel
        {
            get
            {
                return _ImageLink;
            }

            set
            {
                SetProperty(ref _ImageLink, value);
            }
        }
    }
}