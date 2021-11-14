namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using SharedSharp.Logging;

    /// <summary>
    /// ViewModel for the Address Detail page.
    /// </summary>
    public class DateSpanDetailViewModel : ViewModelBase
    {
        public DateSpanDetailViewModel(ISharedLogging iocCommonLogging)
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
        public override void HandleViewDataLoadEvent()
        {
            BaseCL.RoutineEntry("DateDetailViewModel");

            HLinkDateModelSpan HLinkObject = CommonRoutines.GetHLinkParameter<HLinkDateModelSpan>(BaseParamsHLink);

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

                BaseDetail.Add((DateObject as IDateObjectModelSpan).AsCardListLine());
            }

            return;
        }
    }
}