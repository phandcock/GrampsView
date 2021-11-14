namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using SharedSharp.Logging;

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
        public AttributeDetailViewModel(ISharedLogging iocCommonLogging)
            : base(iocCommonLogging)
        {
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
        public override void HandleViewDataLoadEvent()
        {
            BaseCL.RoutineEntry("AttributeDetailViewModel");

            HLinkAttributeObject = CommonRoutines.GetHLinkParameter<HLinkAttributeModel>(BaseParamsHLink);

            if (HLinkAttributeObject.Valid)
            {
                AttributeObject = HLinkAttributeObject.DeRef;

                BaseModelBase = AttributeObject;

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