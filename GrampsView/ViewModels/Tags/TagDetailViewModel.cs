//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="TagDetailViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// Defines the Tag Detail Page View ViewModel.
    /// </summary>
    public class TagDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The local book mark object.
        /// </summary>
        private TagModel localTagObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocCommonModelGridBuilder">
        /// The ioc common model grid builder.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public TagDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
        }

        /// <summary>
        /// Gets or sets the tag object.
        /// </summary>
        /// <value>
        /// The tag object.
        /// </value>

        public TagModel TagObject
        {
            get
            {
                return localTagObject;
            }

            set
            {
                SetProperty(ref localTagObject, value);
            }
        }

        /// <summary>
        /// Handles navigation in wards and sets up the event model parameter.
        /// </summary>
        /// <param name="e">
        /// The <see cref="NavigatedToEventArgs"/> instance containing the event data.
        /// </param>
        /// <param name="viewModelState">
        /// The parameter is not used.
        /// </param>
        public override void PopulateViewModel()
        {
            TagObject = DV.TagDV.GetModelFromHLinkString(BaseNavParamsHLink.HLinkKey);

            // Trigger refresh of View fields via INotifyPropertyChanged
            RaisePropertyChanged(string.Empty);

            if (!(TagObject is null))
            {
                BaseTitle = "Tag Detail";
                BaseTitleIcon = CommonConstants.IconTag;

                // Get Headers
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Add(new CardListLineCollection
                {
                        new CardListLine("Card Type:", "Tag Detail"),
                        new CardListLine("Name:", TagObject.GName),
                        new CardListLine("Priority:", TagObject.GPriority.ToString(System.Globalization.CultureInfo.CurrentCulture)),
                        new CardListLine("Private:", TagObject.PrivAsString),
                });

                t.Add(DV.TagDV.GetModelInfoFormatted(TagObject));

                BaseDetail.Add(t);
            }

            return;
        }
    }
}