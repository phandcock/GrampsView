// <copyright file="CommonTimeline.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.Common
{
    using GrampsView.Data.Model;

    using System;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.UserActivities;

    using Xamarin.Essentials;

    /// <summary>
    /// Handles User Activity and Timeline related functions.
    /// </summary>
    internal class CommonTimeline
    {
        public static async Task<UserActivitySession> AddToTimeLine(string area, ModelBase theModel, string bodyText)
        {
            UserActivitySession returnedFromUIThread = await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                // Record in the TimeLine
                UserActivityChannel channel = UserActivityChannel.GetDefault();
                UserActivity _ModelUserActivity = await channel.GetOrCreateUserActivityAsync(theModel.HLinkKey);
                Uri _Uri;
                UserActivitySession activitySession = null;

                if (theModel.Valid)
                {
                    // Set deep-link and properties.
                    _Uri = new Uri("gramps://" + area + @"/handle/" + theModel.HLinkKey);

                    //// TODO Add Adapative card visuals once the API has settled down StorageFile // cardFile
                    // = await StorageFile.GetFileFromApplicationUriAsync(new //
                    // Uri("ms-appx:///Assets/Misc/UserActivityCard.json")); string cardText = await
                    // // FileIO.ReadTextAsync(cardFile); // theModel.ModelUserActivity.VisualElements.Content

                    // = AdaptiveCardBuilder.CreateAdaptiveCardFromJson(cardText);
                    // theModel.ModelUserActivity.VisualElements.DisplayText = headerText;

                    _ModelUserActivity.VisualElements.DisplayText = area;
                    _ModelUserActivity.VisualElements.Description = bodyText;
                    _ModelUserActivity.ActivationUri = new Uri("gramps://" + area + @"/handle/" + theModel.HLinkKey);

                    //Save
                    await _ModelUserActivity.SaveAsync(); //save the new metadata

                    if (_ModelUserActivity != null)
                    {
                        activitySession = _ModelUserActivity.CreateSession();
                    }
                }

                return activitySession;
            }).ConfigureAwait(false);

            return returnedFromUIThread;
        }

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