﻿namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

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
        public AttributeDetailViewModel(ICommonLogging iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitle = "Attribute Detail";
            BaseTitleIcon = CommonConstants.IconAttribute;
        }

        public AttributeModel AttributeObject
        {
            get; set;
        }

        public HLinkAttributeModel HLinkAttributeObject
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
            BaseCL.RoutineEntry("AttributeDetailViewModel");

            HLinkAttributeObject = CommonRoutines.GetHLinkParameter<HLinkAttributeModel>(BaseParamsHLink);

            if (HLinkAttributeObject.Valid)
            {
                AttributeObject = HLinkAttributeObject.DeRef;

                BaseTitle = AttributeObject.GetDefaultText;

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