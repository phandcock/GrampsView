namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using Prism.Events;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class NoteListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public NoteListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Note List";
            BaseTitleIcon = CommonConstants.IconNotes;
        }

        public Group<HLinkNoteModelCollection> NoteSource
        {
            get
            {
                return DV.NoteDV.GetAllAsGroupedCardGroup();
            }
        }
    }
}