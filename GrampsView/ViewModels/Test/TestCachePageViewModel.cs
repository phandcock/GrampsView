// <copyright file="TestCachePageViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

/// <summary>
/// Defines the Citation Detail Page View Model
/// </summary>
namespace GrampsView.ViewModels
{
    using FFImageLoading.Transformations;
    using FFImageLoading.Work;

    using GrampsView.Data.DataView;
    using GrampsView.Data.ExternalStorageNS;
    using GrampsView.Data.Model;

    using System;
    using System.Collections.Generic;

    public class CropTransformationViewModel : ViewModelBase
    {
        public PersonModel pm;

        public MediaModel tt;

        public CropTransformationViewModel()
        {
            // Alys - _c47a6bd13bb4288c272 Ann - _c47a1a91aec7e625124 Damon -
            // _dc84f6a1f0b6400c49bf3370bbc Elliot - _c47ecf788120563f2b2

            // do a
            pm = DV.PersonDV.GetModelFromHLink(new HLinkMediaModel { HLinkKey = "_c47a6bd13bb4288c272" });

            doCrop();

            aTransformations.Add(
                 new CropTransformation(CurrentZoomFactor, CurrentXOffset, CurrentYOffset, CropWidthRatio, CropHeightRatio)
                 );

            aSource = tt.MediaStorageFilePath;

            // do b
            pm = DV.PersonDV.GetModelFromHLink(new HLinkMediaModel { HLinkKey = "_dc84f6a1f0b6400c49bf3370bbc" });

            doCrop();

            bTransformations.Add(
                 new CropTransformation(CurrentZoomFactor, CurrentXOffset, CurrentYOffset, CropWidthRatio, CropHeightRatio)
                 );

            bSource = tt.MediaStorageFilePath;

            // do c
            pm = DV.PersonDV.GetModelFromHLinkString("_c47a1a9640c7f46c6fb");

            doCrop();

            cTransformations.Add(
                 new CropTransformation(CurrentZoomFactor, CurrentXOffset, CurrentYOffset, CropWidthRatio, CropHeightRatio)
                 );

            cSource = tt.MediaStorageFilePath;
        }

        public string aSource { get; set; } = string.Empty;

        public List<ITransformation> aTransformations { get; set; } = new List<ITransformation>();

        public string bSource { get; set; } = string.Empty;

        public List<ITransformation> bTransformations { get; set; } = new List<ITransformation>();

        public double CropHeightRatio { get; set; }

        public double CropWidthRatio { get; set; }

        public string cSource { get; set; } = string.Empty;

        public List<ITransformation> cTransformations { get; set; } = new List<ITransformation>();

        public double CurrentXOffset { get; set; }

        public double CurrentYOffset { get; set; }

        public double CurrentZoomFactor { get; set; }

        public void doCrop()
        {
            HLinkLoadImageModel hlmm = new HLinkLoadImageModel(); // TODO Fix this  pm.HomeImageHLink;

            tt = hlmm.DeRef;

            // my code
            double t = Math.Max((hlmm.GCorner2X - hlmm.GCorner1X), (hlmm.GCorner2Y - hlmm.GCorner1Y));

            double CropWidth = hlmm.GCorner2X - hlmm.GCorner1X;
            double CropHeight = hlmm.GCorner2Y - hlmm.GCorner1Y;

            CropWidthRatio = CropWidth / 100d;
            CropHeightRatio = CropHeight / 100d;

            CurrentZoomFactor = 100d / Math.Min((CropWidth), CropHeight);

            CurrentXOffset = (-50 + hlmm.GCorner1X + (CropWidth / 2)) / 100d;

            //CurrentXOffset = -1 * (50 - (hlmm.GCorner1X - (CropWidth / 2)));      // Convert to zero at 50 / 100 and edge of crop box

            // CurrentXOffset = CurrentXOffset * (CropWidth / CropHeight); // Scale to width/height

            CurrentXOffset = (-50 + hlmm.GCorner1X + (CropWidth / 2));

            CurrentXOffset = CurrentXOffset * (tt.MetaDataWidth / tt.MetaDataHeight) * (CropHeight / CropWidth) / 100d;                              // Convert to percentage

            // CurrentXOffset = (-50 + hlmm.GCorner1X + (CropWidth / 2)) * (CropWidth / CropHeight)
            // / 100d;

            CurrentYOffset = (-50 + hlmm.GCorner1Y + (CropHeight / 2)) / 100d;

            CurrentYOffset = (-50 + hlmm.GCorner1Y + (CropHeight / 2));

            CurrentYOffset = CurrentYOffset * (tt.MetaDataHeight / tt.MetaDataWidth) * (CropWidth / CropHeight) / 100d;

            //CurrentYOffset = (-50 + hlmm.GCorner1Y + (CropHeight / 2)) * (CropHeight / CropWidth) / 100d;

            // API code
            double cropWidthRatio = CropWidthRatio;
            double cropHeightRatio = CropHeightRatio;

            double xOffset = CurrentXOffset;
            double yOffset = CurrentYOffset;

            double zoomFactor = CurrentZoomFactor;

            double sourceWidth = tt.MetaDataWidth;
            double sourceHeight = tt.MetaDataHeight;

            double desiredWidth = sourceWidth;
            double desiredHeight = sourceHeight;

            double desiredRatio = cropWidthRatio / cropHeightRatio;
            double currentRatio = sourceWidth / sourceHeight;

            if (currentRatio > desiredRatio)
                desiredWidth = (cropWidthRatio * sourceHeight / cropHeightRatio);
            else if (currentRatio < desiredRatio)
                desiredHeight = (cropHeightRatio * sourceWidth / cropWidthRatio);

            xOffset = xOffset * desiredWidth;
            yOffset = yOffset * desiredHeight;

            desiredWidth = desiredWidth / zoomFactor;
            desiredHeight = desiredHeight / zoomFactor;

            float cropX = (float)(((sourceWidth - desiredWidth) / 2) + xOffset);
            float cropY = (float)(((sourceHeight - desiredHeight) / 2) + yOffset);

            if (cropX < 0)
                cropX = 0;

            if (cropY < 0)
                cropY = 0;

            if (cropX + desiredWidth > sourceWidth)
                cropX = (float)(sourceWidth - desiredWidth);

            if (cropY + desiredHeight > sourceHeight)
                cropY = (float)(sourceHeight - desiredHeight);

            //Transformations.Add(
            //    new CropTransformation(CurrentZoomFactor, CurrentXOffset, CurrentYOffset, CropWidthRatio, CropHeightRatio)
            //);
        }
    }
}