// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Model;

namespace GrampsView.ViewModels.MinorModels
{
    /// <summary>
    /// ViewModel for the Attribute Detail page.
    /// </summary>
    public class AttributeDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>

        public AttributeDetailViewModel(ILog iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitleIcon = Constants.IconAttribute;
        }

        public AttributeModel AttributeObject
        {
            get; set;
        } = new AttributeModel();

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void HandleViewModelParameters()
        {
            BaseCL.RoutineEntry("AttributeDetailViewModel");

            if (base.NavigationParameter is not null && base.NavigationParameter.Valid)
            {
                HLinkAttributeModel HLinkObject = base.NavigationParameter as HLinkAttributeModel;

                if (HLinkObject.Valid)
                {
                    AttributeObject = HLinkObject.DeRef;

                    BaseModelBase = AttributeObject;

                    BaseDetail.Clear();

                    // Get the Attribute Details
                    BaseDetail.Add(new CardListLineCollection("Attribute Detail")
                {
                    new CardListLine("Type:", AttributeObject.GType),
                    new CardListLine("Value:", AttributeObject.GValue),
                });

                    // Add Standard details
                    BaseDetail.Add(CommonRoutines.GetModelInfoFormatted(AttributeObject));
                }

                return;
            }
        }
    }
}