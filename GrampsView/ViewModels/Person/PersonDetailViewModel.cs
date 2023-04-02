// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks;

using System.ComponentModel;

namespace GrampsView.ViewModels.Person
{
    /// <summary>
    /// ViewModel for the Person Detail page.
    /// </summary>
    public class PersonDetailViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        [Obsolete]
        public PersonDetailViewModel(ILog iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitleIcon = Constants.IconPeople;
        }

        public HLinkNoteModel BioNote
        {
            get; set;
        } = new HLinkNoteModel();

        public HLinkBase BirthDate { get; set; } = new HLinkDateModelVal();

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
                HLinkEventModelCollection t = new();

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

        public HLinkFamilyGraphModel FamilyGraphModel { get; set; }

        public CardListLineCollection GetExtraPersonDetails
        {
            get
            {
                // Get extra details
                CardListLineCollection extraDetailsCard = new("Person Detail")
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

        public ItemGlyph MediaCard
        {
            get; set;
        }

                = new ItemGlyph();

        public HLinkNoteModelCollection NotesWithoutHighlight
        {
            get; set;
        } = new HLinkNoteModelCollection();

        public HLinkPersonNameModelCollection PersonNameMultipleDetails =>
                // If only one name then its already displayed in the detail section
                PersonObject.GPersonNamesCollection.Count == 1 ? new HLinkPersonNameModelCollection() : PersonObject.GPersonNamesCollection;

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

        public CardListLineCollection StandardDetails { get; set; } = new CardListLineCollection();


        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void HandleViewModelParameters()
        {
            BaseCL.RoutineEntry("PersonDetailViewModel");

            HLinkPersonModel HLinkPerson = CommonRoutines.GetHLinkParameter<HLinkPersonModel>(HLinkSerial);

            PersonObject = HLinkPerson.DeRef;

            if (PersonObject is not null)
            {
                BaseModelBase = PersonObject;

                // Get media image
                MediaCard = PersonObject.ModelItemGlyph;

                // Get date card
                if (PersonObject.BirthDate.Valid)
                {
                    BirthDate = PersonObject.BirthDate.AsHLink("Birth Date");
                }

                // Add Standard details
                StandardDetails = DV.PersonDV.GetModelInfoFormatted(PersonObject);

                FamilyGraphModel = new HLinkFamilyGraphModel
                {
                    DeRef = PersonObject,
                };

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
            }

            return;
        }
    }
}