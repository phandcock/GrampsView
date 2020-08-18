//-----------------------------------------------------------------------
//
// View model for the fly-out page view
//
// <copyright file="AboutViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    using System.Threading.Tasks;

    public class TwoPanePageViewModel : ViewModelBase
    {
    

        public TwoPanePageViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
                                            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "TwoPanePageViewModel";
            BaseTitleIcon = CommonConstants.IconAbout;
        }

        /// <summary>
        /// Raises the <see cref="NavigatedTo"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="NavigatedToEventArgs"/> instance containing the event data.
        /// </param>
        /// <param name="viewModelState">
        /// State of the view ViewModel.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public override void PopulateViewModel()
        {
            //var t = BaseNavParams;


        }
    }
}