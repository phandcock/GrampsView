namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;

    public class BookMarkListViewModel : ViewModelBase
    {
        public BookMarkListViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "BookMark List";
            BaseTitleIcon = CommonConstants.IconBookMark;
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