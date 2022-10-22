namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Models.DataModels.Minor;

    using SharedSharp.Logging;
    using SharedSharp.Model;

    public class PersonNameDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        public PersonNameDetailViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitleIcon = Constants.IconPersonName;
        }

        /// <summary>
        /// Gets or sets the View Current Person.
        /// </summary>
        /// <value>
        /// The current person ViewModel.
        /// </value>
        public PersonNameModel PersonNameObject
        {
            get; set;
        }

        /// <summary>
        /// Populates the view ViewModel.
        ///
        /// XML 1.71 all done
        /// </summary>
        /// <returns>
        /// </returns>
        public override void HandleViewDataLoadEvent()
        {
            BaseCL.RoutineEntry("NameDetailViewModel");

            HLinkPersonNameModel HLinkObject = CommonRoutines.GetHLinkParameter<HLinkPersonNameModel>((BaseParamsHLink));

            PersonNameObject = HLinkObject.DeRef;

            if (PersonNameObject.Valid)
            {
                BaseModelBase = PersonNameObject;

                // Get Header Details
                CardListLineCollection headerCardGroup = new CardListLineCollection { Title = "Person Name Details" };
                headerCardGroup.Add(new CardListLine("Full Name:", PersonNameObject.FullName));
                BaseDetail.Add(headerCardGroup);

                // TODO Show All Surnames
                CardListLineCollection PersonNameCards = new CardListLineCollection("Name")
                {
                    new CardListLine("Type:", PersonNameObject.GType),
                    new CardListLine("Full Name:", PersonNameObject.FullName),
                    new CardListLine("Title:", PersonNameObject.GTitle),
                    new CardListLine("FirstName:", PersonNameObject.GFirstName),
                    new CardListLine("Primary SurName:", PersonNameObject.GSurName.GetPrimarySurname),
                    new CardListLine("Suffix:", PersonNameObject.GSuffix),

                    new CardListLine("Alternative:", PersonNameObject.GAlt.ToString()),
                    new CardListLine("Call:", PersonNameObject.GCall),
                    new CardListLine("Display:", PersonNameObject.GDisplay),
                    new CardListLine("Family Nick:", PersonNameObject.GFamilyNick),

                    new CardListLine("Group:", PersonNameObject.GGroup),
                    new CardListLine("Nick:", PersonNameObject.GNick),
                    new CardListLine("Priv:", PersonNameObject.Priv,true),
                    new CardListLine("Sort:", PersonNameObject.GSort)
                };

                BaseDetail.Add(PersonNameCards);

                // Get date card
                BaseDetail.Add(PersonNameObject.GDate.AsHLink("Name Date"));

                foreach (HLinkSurnameModel item in PersonNameObject.GSurName)
                {
                    CardListLineCollection SurnameCard = new CardListLineCollection("Surnames")
                        {
                            new CardListLine("Surname:", item.ToString()),
                            new CardListLine("Prefix:", item.DeRef.GPrefix),
                            new CardListLine("Primary:", item.DeRef.GPrim),
                            new CardListLine("Derivation:", item.DeRef.GDerivation),
                            new CardListLine("Connector:", item.DeRef.GConnector),
                        };

                    BaseDetail.Add(SurnameCard);
                }
            }

            return;
        }
    }
}