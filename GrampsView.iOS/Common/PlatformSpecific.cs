namespace GrampsView.iOS.Common
{
    using GrampsView.Common.CustomClasses;

    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Interfaces;

    internal partial class PlatformSpecific : IPlatformSpecific
    {
        //public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
        //{
        //    // No Timeline functionality so ignore this

        //    return;
        //}

        //public async Task ActivityTimeLineAdd(FamilyModel argFamilyModel)
        //{
        //    // No Timeline functionality so ignore this
        //}

        private IErrorNotifications _IErrorNotifications;

        private IMessenger _IMessenger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformSpecific"/> class.
        /// </summary>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public PlatformSpecific(IMessenger iocEventAggregator, IErrorNotifications iocErrorNotification)
        {
            // iocEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(UpdateTile, ThreadOption.UIThread);

            _IMessenger = iocEventAggregator;
            _IErrorNotifications = iocErrorNotification;
        }
    }
}