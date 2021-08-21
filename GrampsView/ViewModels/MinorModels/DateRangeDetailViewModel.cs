namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    /// <summary>
    /// ViewModel for the Address Detail page.
    /// </summary>
    public class DateRangeDetailViewModel : ViewModelBase
    {
        public DateRangeDetailViewModel(ICommonLogging iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitleIcon = CommonConstants.IconDDefault;
        }

        /// <summary>
        /// Gets or sets the View Current Person.
        /// </summary>
        /// <value>
        /// The current person ViewModel.
        /// </value>
        public DateObjectModel DateObject
        {
            get; set;
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void BaseHandleLoadEvent()
        {
            BaseCL.RoutineEntry("DateDetailViewModel");

            HLinkDateModelRange HLinkObject = CommonRoutines.GetHLinkParameter<HLinkDateModelRange>(BaseParamsHLink);

            BaseTitle = HLinkObject.Title;

            DateObject = HLinkObject.DeRef;

            if (DateObject.Valid)
            {
                BaseModelBase = DateObject;

                /*
                 * General Details
                 */

                // Get the Date Details
                BaseDetail.Add(new CardListLineCollection("Date")
                {
                    new CardListLine("Short Date:", DateObject.ShortDate),
                    new CardListLine("Long Date:", DateObject.LongDate),
                    new CardListLine("Age:", $"{DateObject.GetAge} years ago"),
                    new CardListLine("Valid:", DateObject.Valid),
                });

                BaseDetail.Add(new CardListLineCollection("Date Detail")
                {
                    new CardListLine("Month Day:", $"{DateObject.GetMonthDay:MM dd}"),
                    new CardListLine("Decade:", DateObject.GetDecade),
                    new CardListLine("Year:", DateObject.GetYear),
                });

                BaseDetail.Add(new CardListLineCollection("Date Internal")
                {
                    new CardListLine("Default Date:", DateObject.DefaultText),
                    new CardListLine("Notional Date:", DateObject.NotionalDate.ToString()),
                    new CardListLine("Single Date:", DateObject.SingleDate.ToString()),
                    new CardListLine("Sort Date:", DateObject.SortDate.ToString()),
                });

                BaseDetail.Add((DateObject as IDateObjectModelRange).AsCardListLine());
            }

            return;
        }
    }
}