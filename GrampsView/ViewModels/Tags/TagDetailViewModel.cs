using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;

namespace GrampsView.ViewModels.Tags
{
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
        [Obsolete]
        public TagDetailViewModel(ILog iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
        }

        public TagModel TagObject
        {
            get; set;
        }

        /// <summary>Gets or sets the tag object.</summary>
        /// <value>The tag object.</value>
        public override void HandleViewModelParameters()
        {
            HLinkTagModel HLinkObject = CommonRoutines.GetHLinkParameter<HLinkTagModel>(BasePassedArguments);

            TagObject = HLinkObject.DeRef;

            if (TagObject is not null)
            {
                BaseModelBase = TagObject;
                BaseTitleIcon = Constants.IconTag;

                BaseDetail.Clear();

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