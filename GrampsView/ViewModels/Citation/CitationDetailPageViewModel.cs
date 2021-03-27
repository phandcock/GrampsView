namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    using System;
    using System.Globalization;

    /// <summary>
    /// Defines the Citation Detail Page View ViewModel.
    /// </summary>
    public class CitationDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// Navigation Service
        /// </param>
        public CitationDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
        }

        /// <summary>
        /// Gets or sets the citation object.
        /// </summary>
        /// <value>
        /// The citation object.
        /// </value>

        public CitationModel CitationObject
        {
            get; set;
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
        public override void BaseHandleAppearingEvent()
        {
            // Handle HLinkKeys
            HLinkCitationModel HLinkCitation = CommonRoutines.DeserialiseObject<HLinkCitationModel>(Uri.UnescapeDataString(BaseParamsHLink));

            CitationObject = HLinkCitation.DeRef;

            //// Trigger refresh of View fields via INotifyPropertyChanged
            //RaisePropertyChanged(string.Empty);

            if (CitationObject != null)
            {
                BaseTitle = CitationObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconCitation;

                BaseDetail.Add(new CardListLineCollection("Citation Detail")
                {
                    new CardListLine("Page:", CitationObject.GPage),
                    new CardListLine("Confidence:", CitationObject.GConfidence.ToString(CultureInfo.CurrentCulture))
                });

                // Get date card
                BaseDetail.Add(CitationObject.GDateContent.AsCardListLine());

                BaseDetail.Add(DV.CitationDV.GetModelInfoFormatted(CitationObject));

                //BaseDetail.Add(t);

                // If only one note (the most common case) just display it in a large format,
                // otherwise setup a list of them.
                if (CitationObject.GNoteRefCollection.Count > 0)
                {
                    // TODO Fix this NoteObject = CitationObject.GNoteRefCollection[0].DeRef;
                }

                // TODO BaseDetail.Add(CitationObject.GSourceAttributeCollection);

                RaisePropertyChanged(nameof(BaseDetail));
            }
        }
    }
}