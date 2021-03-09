﻿// <copyright file="AddressDetailViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System;

    /// <summary>
    /// ViewModel for the Address Detail page.
    /// </summary>
    public class PersonNameDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The current address.
        /// </summary>
        private PersonNameModel _AddressObject = new PersonNameModel();

        private ItemGlyph _MediaCard = new ItemGlyph();

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        public PersonNameDetailViewModel(ICommonLogging iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitle = "Name Detail";
            BaseTitleIcon = CommonConstants.IconPersonName;
        }

        public ItemGlyph MediaCard
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
        /// Gets or sets the View Current Person.
        /// </summary>
        /// <value>
        /// The current person ViewModel.
        /// </value>
        public PersonNameModel PersonNameObject
        {
            get
            {
                return _AddressObject;
            }

            set
            {
                SetProperty(ref _AddressObject, value);
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        ///
        /// XML 1.71 all done
        /// </summary>
        /// <returns>
        /// </returns>
        public override void BaseHandleAppearingEvent()
        {
            BaseCL.RoutineEntry("NameDetailViewModel");

            HLinkPersonNameModel HLinkObject = CommonRoutines.DeserialiseObject<HLinkPersonNameModel>(Uri.UnescapeDataString(BaseParamsHLink));

            PersonNameObject = HLinkObject.DeRef;

            if (PersonNameObject.Valid)
            {
                BaseTitle = PersonNameObject.GetDefaultText;

                // Trigger refresh of View fields via INotifyPropertyChanged
                RaisePropertyChanged(string.Empty);

                // Get media image
                MediaCard = PersonNameObject.ModelItemGlyph;

                // Get Header Details
                CardGroup headerCardGroup = new CardGroup { Title = "Person Name Details" };
                BaseDetail.Add(headerCardGroup);

                // TODO Show All Surnames
                CardListLineCollection PersonNameCards = new CardListLineCollection
                {
                    new CardListLine("Type:", PersonNameObject.GType),
                    new CardListLine("Full Name:", PersonNameObject.FullName),
                    new CardListLine("Title:", PersonNameObject.GTitle),
                    new CardListLine("FirstName:", PersonNameObject.GFirstName),
                    new CardListLine("SurName:", PersonNameObject.GSurName.GetPrimarySurname),
                    new CardListLine("Suffix:", PersonNameObject.GSuffix),

                    new CardListLine("Alt:", PersonNameObject.GAlt.GetDefaultText),
                    new CardListLine("Call:", PersonNameObject.GCall),
                    new CardListLine("Display:", PersonNameObject.GDisplay),
                    new CardListLine("Family Nick:", PersonNameObject.GFamilyNick),

                    new CardListLine("Group:", PersonNameObject.GGroup),
                    new CardListLine("Nick:", PersonNameObject.GNick),
                    new CardListLine("Priv:", PersonNameObject.GPriv,true),
                    new CardListLine("Sort:", PersonNameObject.GSort)
                };

                BaseDetail.Add(PersonNameCards);

                // Get date card
                BaseDetail.Add(PersonNameObject.GDate.AsCardListLine());

                foreach (SurnameModel item in PersonNameObject.GSurName)
                {
                    CardListLineCollection SurnameCard = new CardListLineCollection
                        {
                            new CardListLine("Prefix:", item.GPrefix),
                            new CardListLine("Primary:", item.GPrim),
                            new CardListLine("Derivation:", item.GDerivation),
                            new CardListLine("Connector:", item.GConnector),
                        };

                    BaseDetail.Add(SurnameCard);
                }
            }

            return;
        }
    }
}