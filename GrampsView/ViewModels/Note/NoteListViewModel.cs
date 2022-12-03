namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using CommunityToolkit.Mvvm.Messaging;

    using SharedSharp.Logging;
    using GrampsView.Models.Collections.HLinks;

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
        public NoteListViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "Note List";
            BaseTitleIcon = Constants.IconNotes;
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