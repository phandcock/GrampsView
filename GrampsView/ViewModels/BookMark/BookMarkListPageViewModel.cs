namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using CommunityToolkit.Mvvm.Messaging;

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
                return DV.BookMarkCollection.CardGroupAsProperty;
            }
        }
    }
}