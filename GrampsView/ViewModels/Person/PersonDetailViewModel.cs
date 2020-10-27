namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Navigation;

    using System.Diagnostics.Contracts;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// ViewModel for the Person Detail page.
    /// </summary>
    public class PersonDetailViewModel : ViewModelBase
    {
        private readonly IPlatformSpecific _PlatformSpecific;

        private HLinkMediaModel _MediaCard = new HLinkMediaModel();

        /// <summary>
        /// The current person.
        /// </summary>
        private PersonModel _PersonObject = new PersonModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        public PersonDetailViewModel(ICommonLogging iocCommonLogging, IPlatformSpecific iocPlatformSpecific)
            : base(iocCommonLogging)
        {
            BaseTitle = "Person Detail";
            BaseTitleIcon = CommonConstants.IconPeople;

            _PlatformSpecific = iocPlatformSpecific;
        }

        /// <summary>
        /// Gets the persons events and those of any families they were in.
        /// </summary>
        /// <returns>
        /// CardGroup
        /// </returns>
        public CardGroupBase<HLinkEventModel> EventsIncFamily
        {
            get
            {
                // Get the personal events
                HLinkEventModelCollection t = new HLinkEventModelCollection();

                t.AddRange(PersonObject.GEventRefCollection);

                // Get Family events
                foreach (HLinkFamilyModel families in PersonObject.GParentInRefCollection)
                {
                    foreach (HLinkEventModel familyEvent in families.DeRef.GEventRefCollection)
                    {
                        t.Add(familyEvent);
                    }
                }

                t.Sort(x => x.DeRef.GDate.SortDate);

                return t;
            }
        }

        public HLinkMediaModel MediaCard
        {
            get
            {
                return _MediaCard;
            }
            set
            {
                SetProperty(ref _MediaCard, value);
            }
        }

        /// <summary>
        /// Gets or sets the Person to be shown on the page.
        /// </summary>
        /// <value>
        /// The current person ViewModel.
        /// </value>
        public PersonModel PersonObject
        {
            get
            {
                return _PersonObject;
            }

            set
            {
                SetProperty(ref _PersonObject, value);
            }
        }

        /// <summary>
        /// Called when [navigating from].
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void OnNavigatingFrom(INavigationParameters parameters)
        {
            OnNavigatedFrom(parameters);

            // TODO CommonTimeline.FinishActivitySessionAsync(localActivitySession);
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void PopulateViewModel()
        {
            BaseCL.LogRoutineEntry("PersonDetailViewModel");

            PersonObject = DV.PersonDV.GetModelFromHLink(BaseNavParamsHLink);

            // Trigger refresh of View fields via INotifyPropertyChanged
            RaisePropertyChanged(string.Empty);

            if (!(PersonObject is null))
            {
                BaseTitle = PersonObject.GPersonNamesCollection.GetPrimaryName.DeRef.GetDefaultText;

                // Get media image
                MediaCard = PersonObject.HomeImageHLink.ConvertToHLinkMediaModel;

                // Get the Name Details
                BaseDetail.Add(PersonObject.GPersonNamesCollection.GetPrimaryName.Copy(), argDisplayFormat: DisplayFormat.PersonNameCardSingle);

                // Get the Person Details
                CardListLineCollection nameDetails = GetExtraPersonDetails();

                nameDetails.Title = "Person Detail";

                BaseDetail.Add(nameDetails);

                // Get date card
                BaseDetail.Add(PersonObject.BirthDate.AsCardListLine("Birth Date"));

                // Get parent details
                BaseDetail.Add(
                    new ParentLinkModel
                    {
                        Parents = PersonObject.GChildOf.DeRef,
                    });

                // Add Standard details
                BaseDetail.Add(DV.PersonDV.GetModelInfoFormatted(PersonObject));

                // Add PersonRefDetails
                if (BaseNavParamsHLink is HLinkPersonRefModel)
                {
                    HLinkPersonRefModel personRef = (BaseNavParamsHLink as HLinkPersonRefModel);

                    Contract.Assert(personRef != null);

                    BaseDetail.Add(personRef.GCitationCollection.GetCardGroup("PersonRef Citations"));
                    BaseDetail.Add(personRef.GNoteCollection.GetCardGroup("PersonRef Notes"));
                }

                _PlatformSpecific.ActivityTimeLineAdd(PersonObject);
            }

            return;
        }

        private CardListLineCollection GetExtraPersonDetails()
        {
            // Get extra details
            CardListLineCollection extraDetailsCard = new CardListLineCollection
                {
                        new CardListLine("Gender:", PersonObject.GGenderAsString),
                };

            if (PersonObject.BirthDate != null)
            {
                if (PersonObject.IsLiving)
                {
                    extraDetailsCard.Add(new CardListLine("Age:", PersonObject.BirthDate.GetAge));
                }
                else
                {
                    extraDetailsCard.Add(new CardListLine("Years Since Birth:", PersonObject.BirthDate.GetAge));

                    EventModel ageAtDeath = DV.EventDV.GetEventType(PersonObject.GEventRefCollection, "Death");
                    if (ageAtDeath.Valid)
                    {
                        extraDetailsCard.Add(new CardListLine("Age at Death:", ageAtDeath.GDate.DateDifferenceDecoded(PersonObject.BirthDate)));
                    }
                }
            }
            else
            {
                extraDetailsCard.Add(new CardListLine("Birth Date:", "Unknown"));
            }

            extraDetailsCard.Add(new CardListLine("Is Living:", PersonObject.IsLivingAsString));

            return extraDetailsCard;
        }
    }
}