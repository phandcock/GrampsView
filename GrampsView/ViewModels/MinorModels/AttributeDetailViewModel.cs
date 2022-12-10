using GrampsView.Common;
using GrampsView.Data.Model;

using SharedSharp.Model;

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

        public HLinkAttributeModel HLinkAttributeObject
        {
            get; set;
        } = new HLinkAttributeModel();

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void HandleViewModelParameters()
        {
            BaseCL.RoutineEntry("AttributeDetailViewModel");

            HLinkAttributeObject = CommonRoutines.GetHLinkParameter<HLinkAttributeModel>(BasePassedArguments);

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