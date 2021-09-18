﻿namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.ComponentModel;

    using Xamarin.CommunityToolkit.UI.Views;

    /// <summary>
    /// ViewModel for the Person Detail page.
    /// </summary>
    public class PersonDetailViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IPlatformSpecific _PlatformSpecific;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        /// <param name="iocPlatformSpecific">
        /// platform specific routines
        /// </param>
        public PersonDetailViewModel(ICommonLogging iocCommonLogging, IPlatformSpecific iocPlatformSpecific)
            : base(iocCommonLogging)
        {
            BaseTitleIcon = CommonConstants.IconPeople;

            _PlatformSpecific = iocPlatformSpecific;
        }

        public HLinkNoteModel BioNote
        {
            get; set;
        } = new HLinkNoteModel();

        /// <summary>
        /// Gets the person's events and those of any families they were in.
        /// </summary>
        /// <returns>
        /// CardGroup
        /// </returns>
        public HLinkEventModelCollection EventsIncFamily
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

        public ItemGlyph MediaCard
        {
            get; set;
        }

        = new ItemGlyph();

        public HLinkNoteModelCollection NotesWithoutHighlight
        {
            get; set;
        } = new HLinkNoteModelCollection();

        public HLinkPersonNameModelCollection PersonNameMultipleDetails
        {
            get
            {
                // If only one name then its already displayed in the detail section
                if (PersonObject.GPersonNamesCollection.Count == 1)
                {
                    return new HLinkPersonNameModelCollection();
                }

                return PersonObject.GPersonNamesCollection;
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
            get; set;
        }

        = new PersonModel();

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void BaseHandleLoadEvent()
        {
            BaseCL.RoutineEntry("PersonDetailViewModel");

            // TODO try again to set this up when the toolkit is a little more mature or I have an
            // idea where the bug is coming from
            BaseCurrentLayoutState = LayoutState.Loading;

            HLinkPersonModel HLinkPerson = CommonRoutines.GetHLinkParameter<HLinkPersonModel>(BaseParamsHLink);

            PersonObject = HLinkPerson.DeRef;

            if (!(PersonObject is null))
            {
                BaseModelBase = PersonObject;

                // Get media image
                MediaCard = PersonObject.ModelItemGlyph;

                // Get the Name Details
                BaseDetail.Add(PersonObject.GPersonNamesCollection.GetPrimaryName);

                // Get the Person Details
                CardListLineCollection nameDetails = GetExtraPersonDetails();
                nameDetails.Title = "Person Detail";
                BaseDetail.Add(nameDetails);

                // Get date card
                BaseDetail.Add(PersonObject.BirthDate.AsHLink("Birth Date"));

                // Get parent details
                BaseDetail.Add(
                    new HLinkParentLinkModel
                    {
                        DeRef = PersonObject.GChildOf.DeRef,
                    });

                // Add Standard details
                BaseDetail.Add(DV.PersonDV.GetModelInfoFormatted(PersonObject));

                // If Bio note, display it while showing the full list further below.
                BioNote = PersonObject.GNoteRefCollection.GetBio;

                NotesWithoutHighlight = PersonObject.GNoteRefCollection.GetCollectionWithoutOne(BioNote);

                // Add PersonRefDetails - TODO
                //if (BaseNavParamsHLink is HLinkPersonRefModel)
                //{
                //    HLinkPersonRefModel personRef = (BaseNavParamsHLink as HLinkPersonRefModel);

                // Contract.Assert(personRef != null);

                //    BaseDetail.Add(personRef.GCitationCollection.GetCardGroup("PersonRef Citations"));
                //    BaseDetail.Add(personRef.GNoteCollection.GetCardGroup("PersonRef Notes"));
                //}

                _PlatformSpecific.ActivityTimeLineAdd(PersonObject);
            }

            // TODO fix this
            BaseCurrentLayoutState = LayoutState.None;
            return;
        }

        private CardListLineCollection GetExtraPersonDetails()
        {
            // Get extra details
            CardListLineCollection extraDetailsCard = new CardListLineCollection
                {
                        new CardListLine("Gender:", PersonObject.GGender.ToString()),
                };

            if (PersonObject.BirthDate.Valid)
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