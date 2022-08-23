namespace GrampsView.ViewModels
{
    using CommunityToolkit.Mvvm.Messaging;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using SharedSharp.Logging;

    public class BookMarkListViewModel : ViewModelBase
    {
        public BookMarkListViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
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