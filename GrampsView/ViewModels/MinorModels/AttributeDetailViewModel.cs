namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System;

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
            BaseTitleIcon = CommonConstants.IconAddress;
        }

        public AttributeModel AttributeObject
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

            AttributeObject = CommonRoutines.DeserialiseObject<AttributeModel>(Uri.UnescapeDataString(BaseParamsModel));

            if (AttributeObject.Valid)
            {
                BaseTitle = AttributeObject.GetDefaultText;

                // Get the Name Details
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