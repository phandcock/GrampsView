﻿namespace GrampsView.UWP
{
    using GrampsView.Common.CustomClasses;

    /// <summary>
    /// UWP Platform specific code
    /// </summary>

    internal partial class PlatformSpecific : IPlatformSpecific
    {
        //private IErrorNotifications _IErrorNotifications;

        //private IMessenger _IMessenger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformSpecific"/> class.
        /// </summary>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public PlatformSpecific() //IMessenger iocEventAggregator, IErrorNotifications iocErrorNotification)
        {
            // iocEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(UpdateTile, ThreadOption.UIThread);

            //_IMessenger = iocEventAggregator;
            //_IErrorNotifications = iocErrorNotification;
        }

        ///// <summary>
        ///// Add PersonModel to the activities time line.
        ///// </summary>
        ///// <param name="argPersonModel">
        ///// The person model.
        ///// </param>
        //public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
        //{
        //    try
        //    {
        //        await CommonTimeline.AddToTimeLine(Constants.ModelNamePerson, argPersonModel, argPersonModel.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        App.Current.Services.GetService<IErrorNotifications>().NotifyException("Exception when trying to add PersonDetailView to Windows Timneline", ex);

        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Add FamilyModel to the activities time line.
        ///// </summary>
        ///// <param name="argFamilyModel">
        ///// The argument family model.
        ///// </param>
        //public async Task ActivityTimeLineAdd(FamilyModel argFamilyModel)
        //{
        //    try
        //    {
        //        await CommonTimeline.AddToTimeLine(Constants.ModelNameFamily, argFamilyModel, argFamilyModel.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        App.Current.Services.GetService<IErrorNotifications>().NotifyException("Exception when trying to add FamilyDetailView to Windows Timneline", ex);

        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Updates the Windows Live Tile.
        ///// </summary>
        ///// <param name="obj">
        ///// The object.
        ///// </param>
        //private void UpdateTile()
        //{
        //    CommonTileUpdate.UpdateTile();
        //}
    }
}