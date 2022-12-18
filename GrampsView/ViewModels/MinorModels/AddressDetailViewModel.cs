using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Minor;
using GrampsView.Models.HLinks.Interfaces;


using SharedSharp.Models;

namespace GrampsView.ViewModels
{
    /// <summary>
    /// ViewModel for the Address Detail page.
    /// </summary>
    public class AddressDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        public AddressDetailViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitleIcon = Constants.IconAddress;
        }

        /// <summary>
        /// Gets or sets the View Current Person.
        /// </summary>
        /// <value>
        /// The current person ViewModel.
        /// </value>
        public AddressModel AddressObject
        {
            get; set;
        }

        public IHLinkMediaModel MediaCard
        {
            get; set;
        }

        /// <summary>
        /// Populates the view ViewModel by handling the Load Event.
        /// </summary>
        /// <returns>
        /// <br/>
        /// </returns>
        public override void HandleViewModelParameters()
        {
            BaseCL.RoutineEntry("AddressDetailViewModel");

            HLinkAdressModel HLinkObject = CommonRoutines.GetHLinkParameter<HLinkAdressModel>(BasePassedArguments);

            AddressObject = HLinkObject.DeRef;

            if (AddressObject.Valid)
            {
                BaseModelBase = AddressObject;

                // Get media image
                MediaCard = AddressObject.ModelItemGlyph.ImageHLinkMediaModel;

                // Get the Name Details
                BaseDetail.Add(new CardListLineCollection("Address Detail")
                {
                    new CardListLine("Street:", AddressObject.GStreet),
                    new CardListLine("City:", AddressObject.GCity),
                    new CardListLine("Locality:", AddressObject.GLocality),
                    new CardListLine("County:", AddressObject.GCounty),
                    new CardListLine("State:", AddressObject.GState),
                    new CardListLine("Country:", AddressObject.GCountry),

                    new CardListLine("Date:", AddressObject.GDate.ShortDate),
                    new CardListLine("Postal:", AddressObject.GPostal),
                    new CardListLine("Phone:", AddressObject.GPhone),
                });

                // Add date card
                BaseDetail.Add(AddressObject.GDate.AsHLink("Address Date"));

                // Add Map card
                BaseDetail.Add(AddressObject.ToMapModel().HLink);

                // Add Standard details
                BaseDetail.Add(DV.AddressDV.GetModelInfoFormatted(AddressObject));
            }

            return;
        }
    }
}