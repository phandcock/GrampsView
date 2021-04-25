namespace GrampsView.Common
{
    using AdaptiveCards;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

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
                UserActivitySession activitySession = null;

                try
                {
                    // Record in the TimeLine
                    UserActivityChannel channel = UserActivityChannel.GetDefault();

                    UserActivity _ModelUserActivity = await channel.GetOrCreateUserActivityAsync(theModel.HLinkKey.Value);

                    if (theModel.Valid)
                    {
                        _ModelUserActivity.VisualElements.DisplayText = area.ToUpper();
                        _ModelUserActivity.VisualElements.Description = bodyText;
                        _ModelUserActivity.VisualElements.BackgroundColor = ColorExtensions.ToPlatformColor(theModel.ModelItemGlyph.SymbolColour);

                        // _ModelUserActivity.VisualElements.Content =
                        // AdaptiveCardBuilder.CreateAdaptiveCardFromJson(CreateAdaptiveCardForTimeline(area,
                        // theModel, bodyText).ToJson());

                        _ModelUserActivity.ActivationUri = new Uri("gramps://" + area + @"/handle/" + theModel.HLinkKey);

                        //Save
                        await _ModelUserActivity.SaveAsync();

                        if (_ModelUserActivity != null)
                        {
                            activitySession = _ModelUserActivity.CreateSession();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataStore.Instance.CN.NotifyException("Timeline Add", ex);
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

        internal static AdaptiveCard CreateAdaptiveCardForTimeline(string argArea, ModelBase argTheModel, string argBodyText)
        {
            // _ModelUserActivity.VisualElements.BackgroundColor = ColorExtensions.ToPlatformColor(theModel.ModelItemGlyph.Symbol);

            // Create an adaptive card specifically to reference this app in Windows 10 Timeline.
            AdaptiveCard apodTimelineCard = new AdaptiveCard("1.0");

            // Add a heading to the card, which allows the heading to wrap to the next line if necessary.
            var apodHeading = new AdaptiveTextBlock
            {
                Text = argArea,
                Size = AdaptiveTextSize.Large,
                Weight = AdaptiveTextWeight.Bolder,
                Wrap = true,
                MaxLines = 2
            };
            apodTimelineCard.Body.Add(apodHeading);

            //// Add Column set
            //var apodColumnSet = new AdaptiveColumnSet();

            //// Column 1
            //var apodColumn1 = new AdaptiveColumn();

            // Add a description to the card, and note that it can wrap for several lines.
            var apodDesc = new AdaptiveTextBlock
            {
                Text = argBodyText,
                Size = AdaptiveTextSize.Default,
                Weight = AdaptiveTextWeight.Lighter,
                Wrap = true,
                MaxLines = 3,
                Separator = true
            };

            apodTimelineCard.Body.Add(apodDesc);

            // Add column1
            //apodColumn1.Items.Add(apodDesc);
            //apodColumnSet.Columns.Add(apodColumn1);

            // Column 2

            //// Add a background image to the card.
            //if (argTheModel.HLinkGlyph.IsImageType)
            //{
            //    var apodColumn2 = new AdaptiveColumn();

            // var apodImage = new AdaptiveImage { Url = new
            // Uri(argTheModel.HLinkGlyph.ConvertToHLinkMediaModel.DeRef.MediaStorageFilePath), };

            // // Add column1 apodColumn2.Items.Add(apodImage);

            //    apodColumnSet.Columns.Add(apodColumn2);
            //}

            //apodTimelineCard.Body.Add(apodColumnSet);

            return apodTimelineCard;
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
            //UserActivityChannel channel = UserActivityChannel.GetDefault();

            // Save to activity feed.
            await argActivity.SaveAsync();

            // Create a session, which indicates that the user is engaged in the activity.
            return argActivity.CreateSession();
        }
    }
}