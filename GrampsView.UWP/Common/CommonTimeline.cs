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