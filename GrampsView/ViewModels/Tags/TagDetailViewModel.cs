namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    /// <summary>
    /// Defines the Tag Detail Page View ViewModel.
    /// </summary>
    public class TagDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public TagDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
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
        public override void BaseHandleLoadEvent()
        {
            HLinkTagModel HLinkObject = CommonRoutines.GetHLinkParameter<HLinkTagModel>((BaseParamsHLink));

            TagObject = HLinkObject.DeRef;

            if (!(TagObject is null))
            {
                BaseModelBase = TagObject;
                BaseTitleIcon = CommonConstants.IconTag;

                BaseDetail.Add(new CardListLineCollection("Tag Detail")
                {
                        new CardListLine("Name:", TagObject.GName),
                        new CardListLine("Priority:", TagObject.GPriority.ToString(System.Globalization.CultureInfo.CurrentCulture)),
                        new CardListLine("Private:", TagObject.Priv.ToString()),
                });

                BaseDetail.Add(DV.TagDV.GetModelInfoFormatted(TagObject));
            }

            return;
        }
    }
}