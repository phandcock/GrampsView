// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Minor;

namespace GrampsView.ViewModels.MinorModels
{
    public class PersonNameDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        [Obsolete]
        public PersonNameDetailViewModel(ILog iocCommonLogging)
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
        public override void HandleViewModelParameters()
        {
            BaseCL.RoutineEntry("NameDetailViewModel");

            if (base.NavigationParameter is not null && base.NavigationParameter.Valid)
            {
                HLinkPersonNameModel HLinkObject = base.NavigationParameter as HLinkPersonNameModel;

                PersonNameObject = HLinkObject.DeRef;

                if (PersonNameObject.Valid)
                {
                    BaseModelBase = PersonNameObject;

                    BaseDetail.Clear();

                    // Get Header Details
                    CardListLineCollection headerCardGroup = new()
                    { Title = "Person Name Details" };
                    headerCardGroup.Add(new CardListLine("Full Name:", PersonNameObject.FullName));
                    BaseDetail.Add(headerCardGroup);

                    // TODO Show All Surnames
                    CardListLineCollection PersonNameCards = new("Name")
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
                        CardListLineCollection SurnameCard = new("Surnames")
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
}