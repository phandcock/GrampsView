﻿namespace GrampsView.UWP.Common
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Microsoft.Toolkit.Uwp.Notifications;

    using Windows.ApplicationModel;
    using Windows.UI.Notifications;

    /// <summary>
    /// Update the Live tiles.
    /// </summary>
    public static class CommonTileUpdate
    {
        /// <summary>
        /// Updates the tiles.
        /// </summary>
        public static void UpdateTile()
        {
            TileUpdater theTile = TileUpdateManager.CreateTileUpdaterForApplication();

            theTile.EnableNotificationQueue(true);

            TileContent content = GenerateTileContent(DV.MediaDV.GetRandomFromCollection(null).DeRef);
            theTile.Update(new TileNotification(content.GetXml()));

            content = GenerateTileContent(DV.MediaDV.GetRandomFromCollection(null).DeRef);
            theTile.Update(new TileNotification(content.GetXml()));

            content = GenerateTileContent(DV.MediaDV.GetRandomFromCollection(null).DeRef);
            theTile.Update(new TileNotification(content.GetXml()));
        }

        /// <summary>
        /// Generates the tile binding large.
        /// </summary>
        /// <param name="media">
        /// The media.
        /// </param>
        /// <returns>
        /// </returns>
        private static TileBinding GenerateTileBindingLarge(IMediaModel media)
        {
            return new TileBinding()
            {
                Branding = TileBranding.NameAndLogo,

                Content = new TileBindingContentAdaptive()
                {
                    TextStacking = TileTextStacking.Bottom,

                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = media.GDescription,
                            HintStyle = AdaptiveTextStyle.CaptionSubtle,
                            HintWrap = true,
                            HintMaxLines = 6,
                        },

                        new AdaptiveText()
                        {
                            Text = media.GDateValue.ShortDateOrEmpty,
                            HintStyle = AdaptiveTextStyle.CaptionSubtle,
                            HintWrap = true,
                            HintMaxLines = 1,
                        },
                    },

                    BackgroundImage = new TileBackgroundImage()
                    {
                        Source = media.MediaStorageFilePath,
                        HintOverlay = 30,
                    },
                },

                ContentId = media.HLinkKey,

                DisplayName = Package.Current.DisplayName,
            };
        }

        /// <summary>
        /// Generates the tile binding.
        /// </summary>
        /// <param name="media">
        /// The media.
        /// </param>
        /// <returns>
        /// </returns>
        private static TileBinding GenerateTileBindingMedium(IMediaModel media)
        {
            return new TileBinding()
            {
                Branding = TileBranding.NameAndLogo,

                Content = new TileBindingContentAdaptive()
                {
                    Children =
                        {
                            new AdaptiveText()
                            {
                                Text = media.GDescription,
                                HintStyle = AdaptiveTextStyle.Caption,
                                HintWrap = true,
                                HintMaxLines = 3,
                            },
                        },

                    BackgroundImage = new TileBackgroundImage()
                    {
                        Source = media.MediaStorageFilePath,
                        HintOverlay = 60,
                    },
                },

                ContentId = media.HLinkKey,

                DisplayName = Package.Current.DisplayName,
            };
        }

        /// <summary>
        /// Generates the tile binding wide.
        /// </summary>
        /// <param name="media">
        /// The media.
        /// </param>
        /// <returns>
        /// </returns>
        private static TileBinding GenerateTileBindingWide(IMediaModel media)
        {
            return new TileBinding()
            {
                Branding = TileBranding.NameAndLogo,

                ContentId = media.HLinkKey,

                DisplayName = Package.Current.DisplayName,

                Content = new TileBindingContentAdaptive()
                {
                    Children =
                    {
                        new AdaptiveGroup()
                        {
                            Children =
                            {
                                new AdaptiveSubgroup()
                                {
                                    HintWeight = 1,

                                    Children =
                                    {
                                        new AdaptiveText()
                                        {
                                            Text = media.GDescription,
                                            HintStyle = AdaptiveTextStyle.Caption,
                                            HintWrap = true,
                                            HintMaxLines = 5,
                                        },

                                        new AdaptiveText()
                                        {
                                            Text = media.GDateValue.ShortDateOrEmpty,
                                            HintStyle = AdaptiveTextStyle.CaptionSubtle,
                                            HintWrap = true,
                                            HintMaxLines = 1,
                                        },
                                    },
                                },

                                new AdaptiveSubgroup()
                                {
                                    HintWeight = 2,

                                    Children =
                                    {
                                        new AdaptiveImage()
                                        {
                                            Source = media.MediaStorageFilePath,
                                        },
                                    },
                                },
                            },
                        },
                    },
                },
            };
        }

        /// <summary>
        /// Generates the content of the tile.
        /// </summary>
        /// <param name="argMedia">
        /// The media.
        /// </param>
        /// <returns>
        /// </returns>
        private static TileContent GenerateTileContent(IMediaModel argMedia)
        {
            if (argMedia != null)
            {
                return new TileContent()
                {
                    Visual = new TileVisual()
                    {
                        TileMedium = GenerateTileBindingMedium(argMedia),
                        TileWide = GenerateTileBindingWide(argMedia),
                        TileLarge = GenerateTileBindingLarge(argMedia),
                    },
                };
            }
            else
            {
                return new TileContent();
            }
        }
    }
}