using GrampsView.Common;
using GrampsView.Data.Model;

namespace GrampsView.ViewModels.MinorModels
{
    /// <summary>
    /// ViewModel for the Address Detail page.
    /// </summary>
    public class DateRangeDetailViewModel : ViewModelBase
    {
        [Obsolete]
        public DateRangeDetailViewModel(ILog iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitleIcon = Constants.IconDDefault;
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
        public override void HandleViewModelParameters()
        {
            BaseCL.RoutineEntry("DateDetailViewModel");

            HLinkDateModelRange HLinkObject = CommonRoutines.GetHLinkParameter<HLinkDateModelRange>(BasePassedArguments);

            BaseTitle = HLinkObject.Title;

            DateObject = HLinkObject.DeRef;

            if (DateObject.Valid)
            {
                BaseModelBase = DateObject;

                /*
                 * General Details
                 */

                // Get the Base Date Details
                BaseDetail.Add(DateObject.AsCardListLineBaseDate());
                BaseDetail.Add(DateObject.AsCardListLineBaseDateDetail());
                BaseDetail.Add(DateObject.AsCardListLineBaseDateInternal());

                BaseDetail.Add((DateObject as IDateObjectModelRange).AsCardListLine());
            }

            return;
        }
    }
}