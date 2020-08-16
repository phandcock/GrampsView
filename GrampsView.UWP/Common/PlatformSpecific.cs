namespace GrampsView.UWP
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.UWP.Common;

    using Prism.Events;

    using System;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.UserActivities;

    internal class PlatformSpecific : IPlatformSpecific
    {
        private UserActivitySession _currentActivity;

        ///// <summary>
        ///// The local model user activity.
        ///// </summary>
        [NonSerialized]
        private UserActivity localModelUserActivity = null;

        public PlatformSpecific(IEventAggregator iocEventAggregator)
        {
            iocEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(UpdateTile, ThreadOption.UIThread);
        }

        public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
        {
            try
            {
                // TODO Hack until we have time to fix

                //Get the default UserActivityChannel and query it for our UserActivity. If the activity doesn't exist, one is created.
                UserActivityChannel channel = UserActivityChannel.GetDefault();
                UserActivity userActivity = await channel.GetOrCreateUserActivityAsync(argPersonModel.GetDefaultText);

                //localModelUserActivity = await CommonTimeline.AddToTimeLine("Person",
                //PersonObject, PersonObject.HomeImageHLink.DeRef.MediaStorageFilePath, "Person: "
                //+ PersonObject.BirthName.FullName, "Born: " + PersonObject.BirthDate.ShortDate).ConfigureAwait(false);

                //Populate required properties
                userActivity.VisualElements.DisplayText = argPersonModel.GetDefaultText;
                userActivity.VisualElements.Description = argPersonModel.GPersonNamesCollection.GetPrimaryName.DeRef.FullName;
                userActivity.ActivationUri = new Uri("gramps://" + "Person" + @"/handle/" + argPersonModel.HLinkKey);

                //Save
                await userActivity.SaveAsync(); //save the new metadata

                //Dispose of any current UserActivitySession, and create a new one.
                _currentActivity?.Dispose();
                _currentActivity = userActivity.CreateSession();

                // Finish Hack

                //// TODO localActivitySession = await CommonTimeline.AddToTimeLine("Person",
                //// PersonObject, PersonObject.HomeImageHLink.DeRef.MediaStorageFilePath, "Person: "
                //// + PersonObject.BirthName.FullName, "Born: " + PersonObject.BirthDate.ShortDate).ConfigureAwait(false);

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
            catch (Exception ex)
            {
                DataStore.CN.NotifyException("Exception when trying to add PersonDetailView to Windows Timneline", ex);
            }
        }

        private void UpdateTile(object obj)

        {
            CommonTileUpdate.UpdateTile();
        }
    }
}