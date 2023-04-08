// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Date;

namespace GrampsView.ViewModels.MinorModels
{
    /// <summary>
    /// ViewModel for the Address Detail page.
    /// </summary>
    public class DateValDetailViewModel : ViewModelBase
    {
        [Obsolete]
        public DateValDetailViewModel(ILog iocCommonLogging)
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
        public DateObjectModelBase DateObject
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
            BaseCL.RoutineEntry("DateValDetailViewModel");

            if (base.NavigationParameter is not null && base.NavigationParameter.Valid)
            {
                HLinkDateModelVal HLinkObject = base.NavigationParameter as HLinkDateModelVal;

                BaseTitle = HLinkObject.Title;

                DateObject = HLinkObject.DeRef;

                if (DateObject.Valid)
                {
                    BaseModelBase = DateObject;

                    /*
                     * General Details
                     */

                    BaseDetail.Clear();

                    // Get the Base Date Details
                    BaseDetail.Add(DateObject.AsCardListLineBaseDate());
                    BaseDetail.Add(DateObject.AsCardListLineBaseDateDetail());
                    BaseDetail.Add(DateObject.AsCardListLineBaseDateInternal());

                    BaseDetail.Add((DateObject as IDateObjectModelVal).AsCardListLine());
                }

                return;
            }
        }
    }
}