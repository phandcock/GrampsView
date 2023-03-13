using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;

using System.ComponentModel;

namespace GrampsView.ViewModels.Person
{
    /// <summary>
    /// ViewModel for the Person Detail page.
    /// </summary>
    public class ChildRefDetailViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        [Obsolete]
        public ChildRefDetailViewModel(ILog iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitle = "Child of Person Detail";
            BaseTitleIcon = Constants.IconPeople;
        }

        public HLinkNoteModel BioNote
        {
            get; set;
        } = new HLinkNoteModel();

        public HLinkChildRefModel ChildRefHLink
        {
            get; set;
        }
                     = new HLinkChildRefModel();

        /// <summary>
        /// Gets the persons events and those of any families they were in.
        /// </summary>
        /// <returns>
        /// CardGroup
        /// </returns>
        public CardGroupHLink<HLinkEventModel> EventsIncFamily
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

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void HandleViewModelParameters()
        {
            BaseCL.RoutineEntry("ChildRefDetailViewModel");

            ChildRefHLink = CommonRoutines.GetHLinkParameter<HLinkChildRefModel>(HLinkSerial);

            PersonObject = ChildRefHLink.DeRef;

            if (PersonObject is not null)
            {
                BaseModelBase = PersonObject;

                // Get media image
                MediaCard = PersonObject.ModelItemGlyph;

                BaseDetail.Clear();

                // Get the Name Details
                BaseDetail.Add(PersonObject.GPersonNamesCollection.GetPrimaryName);

                // Get the Person Details
                CardListLineCollection nameDetails = GetExtraPersonDetails();
                nameDetails.Title = "Child Reference Person Detail";

                // handle frel and mrel defaults (according to the gramps source code)
                nameDetails.Add(new CardListLine("Father Relationship", ChildRefHLink.GFatherRel));
                if (string.IsNullOrEmpty(ChildRefHLink.GFatherRel))
                {
                    nameDetails.Add(new CardListLine("Father Relationship", "Birth"));
                }

                nameDetails.Add(new CardListLine("Mother Relationship", ChildRefHLink.GMotherRel));
                if (string.IsNullOrEmpty(ChildRefHLink.GMotherRel))
                {
                    nameDetails.Add(new CardListLine("Mother Relationship", "Birth"));
                }

                BaseDetail.Add(nameDetails);

                // Get date card
                BaseDetail.Add(PersonObject.BirthDate.AsHLink("Birth Date"));

                // Get parent details
                BaseDetail.Add(
                    new HLinkFamilyGraphModel
                    {
                        DeRef = PersonObject
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

                // _PlatformSpecific.ActivityTimeLineAdd(PersonObject);

                //// Trigger refresh of View fields via INotifyPropertyChanged
                //OnPropertyChanged(string.Empty);
            }

            return;
        }

        private CardListLineCollection GetExtraPersonDetails()
        {
            // Get extra details
            CardListLineCollection extraDetailsCard = new()
            {
                        new CardListLine("Gender:", PersonObject.GGender.ToString()),
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