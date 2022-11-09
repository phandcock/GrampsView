namespace GrampsView.ViewModels
{
    using CommunityToolkit.Mvvm.Messaging;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Models.HLinks;

    using SharedSharp.Logging;

    public class BookMarkListViewModel : ViewModelBase
    {
        public BookMarkListViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "BookMark List";
            BaseTitleIcon = Constants.IconBookMark;
        }

        public CardGroupHLink<HLinkBase> BookMarkSource
        {
            get
            {
                return DV.BookMarkCollection.AsCardGroup;
            }
        }
    }
}