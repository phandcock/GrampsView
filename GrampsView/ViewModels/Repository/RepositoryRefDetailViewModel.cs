namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;
    using SharedSharp.Model;

    /// <summary>
    /// Defines the EVent Detail Page View ViewModel.
    /// </summary>
    public class RepositoryRefDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public RepositoryRefDetailViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
        }

        public HLinkRepositoryRefModel RepositoryHLink
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the repository object.
        /// </summary>
        /// <value>
        /// The repository object.
        /// </value>
        public RepositoryModel RepositoryObject
        {
            get; set;
        }

        /// <summary>
        /// Handles navigation inwards and sets up the repository model parameter.
        /// </summary>
        public override void HandleViewDataLoadEvent()
        {
            RepositoryHLink = CommonRoutines.GetHLinkParameter<HLinkRepositoryRefModel>(BaseParamsHLink);

            RepositoryObject = RepositoryHLink.DeRef;

            if (!(RepositoryObject == null))
            {
                BaseTitle = RepositoryObject.ToString();
                BaseTitleIcon = CommonConstants.IconRepository;

                BaseDetail.Add(new CardListLineCollection("Repostiory Ref Detail")
                    {
                        new CardListLine("Type:", "Repostiory Ref"),
                    });

                BaseDetail.Add(new CardListLineCollection("Call Details")
                    {
                        new CardListLine("Call No:", RepositoryHLink.GCallNo),
                        new CardListLine("Medium:", RepositoryHLink.GMedium),
                    });

                BaseDetail.Add(RepositoryHLink.DeRef.HLink);

                BaseDetail.Add(DV.RepositoryDV.GetModelInfoFormatted(RepositoryObject));
            }
        }
    }
}