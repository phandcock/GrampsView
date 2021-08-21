namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    /// <summary>
    /// ViewModel for the Address Detail page.
    /// </summary>
    public class DateStrDetailViewModel : ViewModelBase
    {
        public DateStrDetailViewModel(ICommonLogging iocCommonLogging)
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

            HLinkDateModelStr HLinkObject = CommonRoutines.GetHLinkParameter<HLinkDateModelStr>(BaseParamsHLink);

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

                BaseDetail.Add((DateObject as IDateObjectModelStr).AsCardListLine());
            }
        }
    }
}