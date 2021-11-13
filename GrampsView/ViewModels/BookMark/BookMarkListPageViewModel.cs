namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Microsoft.Toolkit.Mvvm.Messaging;

    public class BookMarkListViewModel : ViewModelBase
    {
        public BookMarkListViewModel(ICommonLogging iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
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