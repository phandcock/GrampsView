namespace GrampsView.Droid.Common
{
    using GrampsView.Common.CustomClasses;

    internal partial class PlatformSpecific : IPlatformSpecific
    {
        //#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        //        public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
        //#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        // { // No Timeline functionality so ignore this }
        //#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        //        public async Task ActivityTimeLineAdd(FamilyModel argFamilyModel)
        //#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        //        {
        //            // No Timeline functionality so ignore this
        //        }

        //private IErrorNotifications _IErrorNotifications;

        //private IMessenger _IMessenger;

        public PlatformSpecific() //IMessenger iocEventAggregator, IErrorNotifications iocErrorNotification)
        {
            // iocEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(UpdateTile, ThreadOption.UIThread);

            //_IMessenger = iocEventAggregator;
            //_IErrorNotifications = iocErrorNotification;
        }
    }
}