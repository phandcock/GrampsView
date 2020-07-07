namespace GrampsView.UWP
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Events;
    using GrampsView.UWP.Common;

    using Prism.Events;

    internal class PlatformSpecific : IPlatformSpecific
    {
        ///// <summary>
        ///// The local model user activity.
        ///// </summary>
        //[NonSerialized]
        //private UserActivity localModelUserActivity = null;

        public PlatformSpecific(IEventAggregator iocEventAggregator)
        {
            iocEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(UpdateTile, ThreadOption.UIThread);
        }

        public void ActivityTimeLineAdd(PersonModel argPersonModel)
        {
            //// TODO localActivitySession = await CommonTimeline.AddToTimeLine("Person",
            //// PersonObject, PersonObject.HomeImageHLink.DeRef.MediaStorageFilePath, "Person: "
            //// + PersonObject.BirthName.FullName, "Born: " + PersonObject.BirthDate.GetShortDateAsString).ConfigureAwait(false);

            //UserActivitySession returnedFromUIThread = await Device.BeginInvokeOnMainThread(async () =>
            //{
            //    // Record in the TimeLine
            //    UserActivityChannel channel = UserActivityChannel.GetDefault();

            // if (theModel.Valid && (theModel.ModelUserActivity is null)) {
            // theModel.ModelUserActivity = await channel.GetOrCreateUserActivityAsync(theModel.HLinkKey);

            // // Set deep-link and properties. theModel.ModelUserActivity.ActivationUri = new
            // Uri("gramps://" + area + @"/handle/" + theModel.HLinkKey);

            // // TODO Add Adapative card visuals once the API has settled down StorageFile //
            // cardFile = await StorageFile.GetFileFromApplicationUriAsync(new //
            // Uri("ms-appx:///Assets/Misc/UserActivityCard.json")); string cardText = await //
            // FileIO.ReadTextAsync(cardFile); // theModel.ModelUserActivity.VisualElements.Content
            // = AdaptiveCardBuilder.CreateAdaptiveCardFromJson(cardText);
            // theModel.ModelUserActivity.VisualElements.DisplayText = headerText;

            // theModel.ModelUserActivity.VisualElements.Description = bodyText;

            // // Save to activity feed. await theModel.ModelUserActivity.SaveAsync(); }

            // // Create a session, which indicates that the user is engaged in the activity.
            // UserActivitySession activitySession = null;

            // if (theModel.ModelUserActivity != null) { activitySession =
            // theModel.ModelUserActivity.CreateSession(); }

            //    return activitySession;
            //}).ConfigureAwait(false);

            //return returnedFromUIThread;
        }

        private void UpdateTile(object obj)

        {
            CommonTileUpdate.UpdateTile();
        }
    }
}