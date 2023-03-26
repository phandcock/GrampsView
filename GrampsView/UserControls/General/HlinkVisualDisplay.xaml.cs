// Copyright (c) phandcock.  All rights reserved.

using CommunityToolkit.Maui.Views;

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Interfaces;
using GrampsView.Models.HLinks.Interfaces;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

namespace GrampsView.UserControls
{
    public partial class HLinkVisualDisplay : Grid
    {
        public static readonly BindableProperty FsctShowMediaProperty
      = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(HLinkVisualDisplay), propertyName: nameof(FsctShowMedia), defaultValue: false);

        public static readonly BindableProperty FsctShowSymbolsProperty
          = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(HLinkVisualDisplay), propertyName: nameof(FsctShowSymbols), defaultValue: true);

        private ItemGlyph newItemGlyph = new();

        public HLinkVisualDisplay()
        {
            InitializeComponent();
        }

        public bool FsctShowMedia
        {
            get => (bool)GetValue(FsctShowMediaProperty);
            set => SetValue(FsctShowMediaProperty, value);
        }

        public bool FsctShowSymbols
        {
            get => (bool)GetValue(FsctShowSymbolsProperty);
            set => SetValue(FsctShowSymbolsProperty, value);
        }

        private ItemGlyph WorkHLMediaModel
        {
            get; set;
        } = new ItemGlyph();

        private void HLinkVisualDisplay_BindingContextChanged(object sender, EventArgs e)
        {
            if (BindingContext == null)
            {
                return;
            }

            try
            {
                newItemGlyph = new ItemGlyph();

                switch (BindingContext.GetType().Name)
                {
                    case nameof(IHLinkMediaModel):
                        {
                            if ((BindingContext as IHLinkMediaModel).Valid)
                            {
                                newItemGlyph = (BindingContext as IHLinkMediaModel).DeRef.ModelItemGlyph;
                            }
                            break;
                        }

                    case nameof(HLinkMediaModel):
                        {
                            if ((BindingContext as IHLinkMediaModel).Valid)
                            {
                                newItemGlyph = (BindingContext as IHLinkMediaModel).DeRef.ModelItemGlyph;
                            }
                            break;
                        }

                    case nameof(ItemGlyph):
                        {
                            newItemGlyph = BindingContext as ItemGlyph;
                            break;
                        }

                    default:
                        {
                            Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("HLinkVisualDisplay Binding Context is not a ItemGlyph but a" + BindingContext.GetType().ToString()));
                            return;
                        }
                }

                // Don't display anything if the glyph is invalid
                if (!newItemGlyph.Valid)
                {
                    return;
                }

                if (newItemGlyph.ImageHLink.Value == "_c4c5aaa038602727de3~zipimage")
                {
                }

                ShowSomething(newItemGlyph);
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("HLinkVisualDisplay", ex);
            }
        }

        //private void NewMediaControl_Error(object sender, CachedImageEvents.ErrorEventArgs e)
        //{
        //    ErrorInfo t = new("Error in HLinkVisualDisplay.")
        //            {
        //                { "Error is ", e.Exception.Message },
        //            };

        //    // Component not found exception
        //    if (e.Exception.HResult == -2003292336)
        //    {
        //        t.Add("Ideas", "Showing bad file, perhaps an internalmediafile or the file type can not be displayed?");
        //    }

        //    // TODO
        //    // t.Add("File", (sender as CachedImage).Source.ToString());

        //    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);

        //    // TODO
        //    //(sender as CachedImage).Cancel();
        //    //(sender as CachedImage).Source = null;
        //}

        private void ShowImage(IMediaModel argMediaModel)
        {
            if (argMediaModel.CurrentStorageFile.Valid)
            {
                try
                {
                    if (string.IsNullOrEmpty(argMediaModel.CurrentStorageFile.GetAbsoluteFilePath))
                    {
                        ErrorInfo t = new("The image file path is null")
                        {
                            { "Id", argMediaModel.Id }
                        };

                        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);
                        return;
                    }
                    // Input valid so start work
                    // TODO fix
                    //CachedImage newMediaControl = new()
                    //{
                    //    Source = argMediaModel.HLink.DeRef.MediaStorageFilePath,
                    //    Margin = 3,
                    //    // TODO
                    //    //Aspect = Aspect.AspectFit,
                    //    //BackgroundColor = Color.Transparent,
                    //    CacheType = FFImageLoading.Cache.CacheType.All,
                    //    DownsampleToViewSize = true,

                    //    // HeightRequest = 100, // "{Binding MediaDetailImageHeight,
                    //    // Source={x:Static common:CardSizes.Current}, Mode=OneWay}"
                    //    HorizontalOptions = LayoutOptions.FillAndExpand,
                    //    IsVisible = true,
                    //    ErrorPlaceholder = "ic_launcher.png",
                    //    LoadingPlaceholder = "ic_launcher.png",
                    //    RetryCount = 3,
                    //    RetryDelay = 1000
                    //};

                    Image newMediaControl = new()
                    {
                        Source = argMediaModel.HLink.DeRef.CurrentStorageFile.GetAbsoluteFilePath,
                        BackgroundColor = new Color(),
                        Aspect = Aspect.AspectFit,
                    };

                    //newMediaControl.Error += NewMediaControl_Error;

                    HLinkVisualDisplayRoot.Children.Clear();
                    HLinkVisualDisplayRoot.Children.Add(newMediaControl);
                }
                catch (Exception ex)
                {
                    ErrorInfo argDetail = new("HLinkVisualDisplay")
                    {
                    { "Type", "Image" },
                    { "Media Model Id", argMediaModel.Id },
                    { "Media Model Path", argMediaModel.CurrentStorageFile.GetAbsoluteFilePath },
                };

                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, argExtraItems: argDetail);
                }
            }
        }

        private void ShowMedia(IMediaModel argMediaModel)
        {
            if (argMediaModel.CurrentStorageFile.Valid)
            {
                try
                {
                    if (string.IsNullOrEmpty(argMediaModel.CurrentStorageFile.GetAbsoluteFilePath))
                    {
                        ErrorInfo t = new("The media file path is null")
                        {
                            { "Id", argMediaModel.Id }
                        };

                        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);
                        return;
                    }

                    // Input valid so start work
                    MediaElement newMediaControl = new MediaElement
                    {
                        Aspect = Aspect.AspectFit,
                        ShouldAutoPlay = false,
                        ShouldShowPlaybackControls = true,
                        BackgroundColor = Colors.Transparent,
                        IsVisible = true,
                        Margin = 3,
                        Source = argMediaModel.HLink.DeRef.CurrentStorageFile.GetAbsoluteFilePath,
                    };

                    //MediaElement newMediaControl = new()
                    //{
                    //    Source = argMediaModel.HLink.DeRef.CurrentStorageFile.GetAbsoluteFilePath
                    //};

                    //newMediaControl.Error += NewMediaControl_Error;

                    HLinkVisualDisplayRoot.Children.Clear();
                    HLinkVisualDisplayRoot.Children.Add(newMediaControl);
                }
                catch (Exception ex)
                {
                    ErrorInfo argDetail = new("HLinkVisualDisplay")
                    {
                    { "Type", "Image" },
                    { "Media Model Id", argMediaModel.Id },
                    { "Media Model Path", argMediaModel.CurrentStorageFile.GetAbsoluteFilePath },
                };

                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, argDetail);
                }
            }
        }

        private void ShowSomething(ItemGlyph argItemGlyph)
        {
            try
            {
                if (!argItemGlyph.Valid)
                {
                    //DataStore.Instance.CN.NotifyError("Invalid HlinkMediaModel (" + HLinkMedia.HLinkKey + ") passed to MediaImage");
                    return;
                }

                // Save the HLink so can check for duplicate changes later
                WorkHLMediaModel = argItemGlyph;

                //if (argHHomeMedia.HLinkKey == "_e1823d5e765308999f4c16addb5")
                //{
                //}

                if (argItemGlyph.Valid)
                {
                    switch (argItemGlyph.ImageType)
                    {
                        case CommonEnums.HLinkGlyphType.Image:
                            {
                                ShowImage(argItemGlyph.ImageHLinkMediaModel.DeRef);
                                break;
                            }

                        case CommonEnums.HLinkGlyphType.Media:
                            {
                                if (FsctShowMedia)
                                {
                                    ShowMedia(argItemGlyph.MediaHLinkMediaModel.DeRef);
                                }
                                else
                                {
                                    ShowImage(argItemGlyph.ImageHLinkMediaModel.DeRef);
                                }
                                break;
                            }

                        case CommonEnums.HLinkGlyphType.Symbol:
                            {
                                if (FsctShowSymbols)
                                {
                                    ShowSymbol(argItemGlyph);
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("HLinkVisualDisplay", ex);
            }
        }

        private void ShowSymbol(ItemGlyph argItemGlyph)
        {
            try
            {
                // create symbol control

                Image newImageControl = new()
                {
                    Aspect = Aspect.AspectFit,
                    BackgroundColor = Colors.Transparent,
                    IsVisible = true,
                    Margin = 10,
                    Source = new FontImageSource
                    {
                    },
                };

                // Input valid so start work

                // Set symbol
                FontImageSource fontGlyph = new()
                {
                    Glyph = argItemGlyph.Symbol,
                    Color = Color.FromRgb(255, 255, 255),
                    FontFamily = "IconFont",
                    Size = 48,
                };

                if (fontGlyph.Glyph == null)
                {
                    ErrorInfo t = new("HLinkVisualDisplay", "Null Glyph")
                        {
                            { "HLinkKey", argItemGlyph.ToString() }
                        };

                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);
                }

                if (fontGlyph.Color == null)
                {
                    ErrorInfo t = new("HLinkVisualDisplay", "Null Glyph Colour")
                        {
                            { "HLinkKey", argItemGlyph.ImageHLink.Value }
                        };

                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);
                }

                newImageControl.Source = fontGlyph;

                HLinkVisualDisplayRoot.Children.Clear();
                HLinkVisualDisplayRoot.Children.Add(newImageControl);
            }
            catch (Exception ex)
            {
                ErrorInfo argDetail = new("HLinkVisualDisplay")
                {
                    { "Type", "Symbol" },
                    { "Media Model HLinkKey", argItemGlyph.ImageHLink.Value  },
                    { "Media Model Symbol", argItemGlyph.Symbol },
                };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, argDetail);
            }
        }
    }
}