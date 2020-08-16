// <copyright file="CommonTimeline.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.Common
{
    using System;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.UserActivities;

    /// <summary>
    /// Handles User Activity and Timeline related functions.
    /// </summary>
    internal class CommonTimeline
    {
        //public static async Task<UserActivitySession> AddToTimeLine(string area, ModelBase theModel, string backgroundImage, string headerText, string bodyText)
        //{
        //    // TODO finish this

        //UserActivitySession returnedFromUIThread = await MainThread.BeginInvokeOnMainThread(async () =>
        //{
        //    // Record in the TimeLine
        //    UserActivityChannel channel = UserActivityChannel.GetDefault();

        // if (theModel.Valid && (theModel.ModelUserActivity is null)) { theModel.ModelUserActivity
        // = await channel.GetOrCreateUserActivityAsync(theModel.HLinkKey);

        // // Set deep-link and properties. theModel.ModelUserActivity.ActivationUri = new
        // Uri("gramps://" + area + @"/handle/" + theModel.HLinkKey);

        // // TODO Add Adapative card visuals once the API has settled down StorageFile // cardFile
        // = await StorageFile.GetFileFromApplicationUriAsync(new //
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

        // return activitySession;

        //}).ConfigureAwait(false);

        // return returnedFromUIThread;

        //}

        /// <summary>
        /// Finishes the activity session asynchronous.
        /// </summary>
        /// <param name="theActivitySession">
        /// The activity session.
        /// </param>
        public static void FinishActivitySessionAsync(UserActivitySession theActivitySession)
        {
            if (!(theActivitySession is null))
            {
                theActivitySession.Dispose();
            }
        }

        /// <summary>
        /// Generates the activity asynchronous.
        /// </summary>
        /// <param name="argActivity">
        /// The argument activity.
        /// </param>
        /// <returns>
        /// </returns>
        internal static async Task<UserActivitySession> GenerateActivityAsync(UserActivity argActivity)
        {
            // TODO Finish this code

            // Get channel and create activity.
            UserActivityChannel channel = UserActivityChannel.GetDefault();

            // Save to activity feed.
            await argActivity.SaveAsync();

            // Create a session, which indicates that the user is engaged in the activity.
            return argActivity.CreateSession();
        }
    }
}